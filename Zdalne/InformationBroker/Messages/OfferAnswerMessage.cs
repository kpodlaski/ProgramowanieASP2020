using InformationBroker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Messages
{
    class OfferAnswerMessage : Message
    {

        public readonly ProductInfo ProductInfo;
        public readonly double Price;
        public readonly int QuantityAviable;
        public OfferAnswerMessage(String from, String to, ProductInfo product, double price, int quantityAviable) : base(from, to, MessageType.OfferAnswer)
        {
            this.ProductInfo = product;
            this.Price = price;
            this.QuantityAviable = quantityAviable;
        }
    }
}
