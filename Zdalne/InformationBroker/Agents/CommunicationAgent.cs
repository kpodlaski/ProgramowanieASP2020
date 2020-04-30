using InformationBroker.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InformationBroker.Agents
{
    abstract class CommunicationAgent
    {
        public readonly String AgentId;
        public readonly AgentType Type;
        protected readonly Broker broker;
        protected MessageFactory messageFactory;
        protected MessageQueue mq = new MessageQueue();
        protected volatile bool closing = false;
        protected Thread workingThread;

        public CommunicationAgent(Broker broker, AgentType type)
        {
            this.broker = broker;
            this.Type = type;
            this.AgentId = broker.Register(this, type);
            messageFactory = new MessageFactory(this.AgentId);
            workingThread = new Thread(this.queueWorker);
            workingThread.Start();
        }

        protected abstract void queueWorker();
        protected void sendMessage(Message msg)
        {
            broker.ReceiveMessage(msg);
        }


        public void Stop()
        {
            broker.Unregister(this);
            closing = true;
        }

        public void ReceiveMessage(Message msg)
        {
            //Console.WriteLine("Agent {0} have message fom {1}", agentId, msg.from);
            mq.AddMessage(msg);
        }

    }
}
