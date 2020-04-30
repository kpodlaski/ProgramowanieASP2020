using InformationBroker.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;


namespace InformationBroker.Messages
{
    class MessageFactory
    {
        private readonly String myId;

        public MessageFactory(string agentId)
        {
            this.myId = agentId;
        }

        public Message BuildOfferRequest(ProductInfo product)
        {
            return new OfferRequestMessage(myId, null, product);
        }

        public Message ReadressOfferRequest(OfferRequestMessage initialMessage, String to)
        {
            return new OfferRequestMessage(initialMessage.From, to, initialMessage.ProductInfo);
        }

        public Message BuildOfferAnswer(OfferRequestMessage initial, double price, int aviableQuantity)
        {
            return new OfferAnswerMessage(myId, initial.From, initial.ProductInfo, price, aviableQuantity);
        }

        public Message BuildSellRequest(OfferAnswerMessage initial, int requestedQuantity)
        {
            return new SellRequestMessage(myId, initial.From, initial.ProductInfo, requestedQuantity);
        }

        public Message BuildSellConfirmation(SellRequestMessage initial, String transaction_id, int quantity, double unitPrice )
        {
            return new SellConfirmMessage(myId, initial.From, initial.ProductInfo, transaction_id, quantity, unitPrice);
        }

        public Message SendProduct(SellConfirmMessage confirm, Product product)
        {
            return new ProductMessage(myId, confirm.To, product, confirm.Transaction_id);
        }
    }
}
