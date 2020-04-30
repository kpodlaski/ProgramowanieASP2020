
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

       
        public void ReceiveMessage(Message msg)
        {
            Console.WriteLine("Broker have msg {0}, from {1}, to {2}", msg.GetType(), msg.From, msg.To);
            mq.AddMessage(msg);

        }

        public string Register(CommunicationAgent communicationAgent, AgentType type)
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

        public void Unregister(CommunicationAgent communicationAgent)
        {
            Console.WriteLine("Unregister ", communicationAgent.AgentId);
            lock (agents)
            {
                agents.Remove(communicationAgent.AgentId);
            }
        }

        protected void queueWorker()
        {
            while (true)
            {
                Message msg = mq.PeekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.Type)
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
                    if (cA.Type != AgentType.Seller) continue;
                    _msg = messageFactory.ReadressOfferRequest(msg, cA.AgentId);
                    cA.ReceiveMessage(_msg);
                }
            }
        }

        private void sendMessage(Message msg)
        {

            lock (agents)
            {
                CommunicationAgent a = agents[msg.To];
                a.ReceiveMessage(msg);
            }
        }
    }
}
