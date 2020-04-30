using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class ProductMessage : Message
    {
        public readonly Product ProductInfo;
        public readonly String Transaction_id;

        public ProductMessage(String from, String to, Product product, String transaction_id) : base(from, to, MessageType.Product)
        {
            this.ProductInfo = product;
            this.Transaction_id = transaction_id;
        }
    }
}
