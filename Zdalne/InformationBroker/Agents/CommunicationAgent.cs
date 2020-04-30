using InformationBroker.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InformationBroker.Agents
{
    abstract class CommunicationAgent
    {
        public readonly String agentId;
        public readonly AgentType type;
        protected readonly Broker broker;
        protected MessageFactory messageFactory;
        protected MessageQueue mq = new MessageQueue();
        protected volatile bool closing = false;
        protected Thread workingThread;

        public CommunicationAgent(Broker broker, AgentType type)
        {
            this.broker = broker;
            this.type = type;
            this.agentId = broker.register(this, type);
            messageFactory = new MessageFactory(this.agentId);
            workingThread = new Thread(this.queueWorker);
            workingThread.Start();
        }

        protected abstract void queueWorker();
        protected void sendMessage(Message msg)
        {
            broker.receiveMessage(msg);
        }


        public void Stop()
        {
            broker.unregister(this);
            closing = true;
        }

        public void receiveMessage(Message msg)
        {
            //Console.WriteLine("Agent {0} have message fom {1}", agentId, msg.from);
            mq.addMessage(msg);
        }

    }
}
