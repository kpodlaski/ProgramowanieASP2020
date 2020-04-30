using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class SellRequestMessage : Message
    {
        public readonly ProductInfo product;
        public readonly int quantity;

        public SellRequestMessage(String from, String to, ProductInfo product, int quantity) : base(from, to, MessageType.SellRequest)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}
