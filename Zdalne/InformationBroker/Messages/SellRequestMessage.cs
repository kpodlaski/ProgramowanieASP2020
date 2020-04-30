using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class SellRequestMessage : Message
    {
        public readonly ProductInfo ProductInfo;
        public readonly int Quantity;

        public SellRequestMessage(String from, String to, ProductInfo product, int quantity) : base(from, to, MessageType.SellRequest)
        {
            this.ProductInfo = product;
            this.Quantity = quantity;
        }
    }
}
