using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Messages
{
    class Message
    {
        public readonly String from;
        public readonly String to;
        public readonly MessageType type;

        protected Message(String from, String to, MessageType type)
        {
            this.from = from;
            this.to = to;
            this.type = type;
        }
    }
}
