using InformationBroker.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InformationBroker.Agents
{

    interface ICommunicationAgent
    {
        public void ReceiveMessage(Message msg);
    }

}

