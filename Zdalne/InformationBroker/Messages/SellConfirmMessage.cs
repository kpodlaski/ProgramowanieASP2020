using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class SellConfirmMessage : Message
    {
        public readonly String Transaction_id;
        public readonly int Quantity;
        public readonly double UnitPrice;
        public readonly ProductInfo ProductInfo;

        public SellConfirmMessage(String from, String to, ProductInfo product, String transaction_id, int quantity, double unitPrice) : base(from, to, MessageType.SellConfirm)
        {
            this.Transaction_id = transaction_id;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.ProductInfo = product;
        }
    }
}
