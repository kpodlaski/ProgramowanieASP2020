
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InformationBroker.Messages;

namespace InformationBroker.Agents
{
    class Broker : ICommunicationAgent
    {
        protected MessageFactory messageFactory;
        protected MessageQueue mq = new MessageQueue();
        protected volatile bool closing = false;
        protected Thread workingThread;
        protected Dictionary<String, CommunicationAgent> agents = new Dictionary<string, CommunicationAgent>();
        private int last_seller = 0;
        private int last_client = 0;

        public Broker()
        {
            messageFactory = new MessageFactory("broker");
            workingThread = new Thread(this.queueWorker);
            workingThread.Start();
        }

       
        public void receiveMessage(Message msg)
        {
            Console.WriteLine("Broker have msg {0}, from {1}, to {2}", msg.GetType(), msg.from, msg.to);
            mq.addMessage(msg);

        }

        public string register(CommunicationAgent communicationAgent, AgentType type)
        {
            Console.WriteLine("Register ", type.ToString());
            String agentId = null;
            lock (agents)
            {
                switch (type)
                {
                    case AgentType.Client: agentId = @"C" + last_client; last_client++; break;
                    case AgentType.Seller: agentId = @"S" + last_seller; last_seller++; break;
                }
                agents[agentId] = communicationAgent;
            }
            return agentId;
        }

        public void unregister(CommunicationAgent communicationAgent)
        {
            Console.WriteLine("Unregister ", communicationAgent.agentId);
            lock (agents)
            {
                agents.Remove(communicationAgent.agentId);
            }
        }

        protected void queueWorker()
        {
            while (true)
            {
                Message msg = mq.peekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.type)
                {
                    case MessageType.OfferRequest: sendOfferToSellers( (OfferRequestMessage) msg); break;
                    default: sendMessage(msg); break;
                }
            }
        }

        private void sendOfferToSellers(OfferRequestMessage msg)
        {
            Message _msg;
            //Console.WriteLine("Resend to all sellers req from {0} ", msg.from);
            lock (agents)
            {
                foreach (CommunicationAgent cA in agents.Values)
                {
                    if (cA.type != AgentType.Seller) continue;
                    _msg = messageFactory.readressOfferRequest(msg, cA.agentId);
                    cA.receiveMessage(_msg);
                }
            }
        }

        private void sendMessage(Message msg)
        {

            lock (agents)
            {
                CommunicationAgent a = agents[msg.to];
                a.receiveMessage(msg);
            }
        }
    }
}
