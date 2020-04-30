using InformationBroker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Messages
{
    class OfferAnswerMessage : Message
    {

        public readonly ProductInfo product;
        public readonly double price;
        public readonly int quantityAviable;
        public OfferAnswerMessage(String from, String to, ProductInfo product, double price, int quantityAviable) : base(from, to, MessageType.OfferAnswer)
        {
            this.product = product;
            this.price = price;
            this.quantityAviable = quantityAviable;
        }
    }
}
