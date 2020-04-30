using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class SellConfirmMessage : Message
    {
        public readonly String transaction_id;
        public readonly int quantity;
        public readonly double unitPrice;
        public readonly ProductInfo product;

        public SellConfirmMessage(String from, String to, ProductInfo product, String transaction_id, int quantity, double unitPrice) : base(from, to, MessageType.SellConfirm)
        {
            this.transaction_id = transaction_id;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
            this.product = product;
        }
    }
}
