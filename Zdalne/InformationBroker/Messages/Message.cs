using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Messages
{
    class Message
    {
        public readonly String From;
        public readonly String To;
        public readonly MessageType Type;

        protected Message(String from, String to, MessageType type)
        {
            this.From = from;
            this.To = to;
            this.Type = type;
        }
    }
}
