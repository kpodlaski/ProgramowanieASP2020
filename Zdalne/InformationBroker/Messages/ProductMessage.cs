using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class ProductMessage : Message
    {
        public readonly Product product;
        public readonly String transaction_id;

        public ProductMessage(String from, String to, Product product, String transaction_id) : base(from, to, MessageType.Product)
        {
            this.product = product;
            this.transaction_id = transaction_id;
        }
    }
}
