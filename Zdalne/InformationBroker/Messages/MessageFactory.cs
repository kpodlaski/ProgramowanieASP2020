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

        public Message buildOfferRequest(ProductInfo product)
        {
            return new OfferRequestMessage(myId, null, product);
        }

        public Message readressOfferRequest(OfferRequestMessage initialMessage, String to)
        {
            return new OfferRequestMessage(initialMessage.from, to, initialMessage.product);
        }

        public Message buildOfferAnswer(OfferRequestMessage initial, double price, int aviableQuantity)
        {
            return new OfferAnswerMessage(myId, initial.from, initial.product, price, aviableQuantity);
        }

        public Message buildSellRequest(OfferAnswerMessage initial, int requestedQuantity)
        {
            return new SellRequestMessage(myId, initial.from, initial.product, requestedQuantity);
        }

        public Message buildSellConfirmation(SellRequestMessage initial, String transaction_id, int quantity, double unitPrice )
        {
            return new SellConfirmMessage(myId, initial.from, initial.product, transaction_id, quantity, unitPrice);
        }

        public Message sendProduct(SellConfirmMessage confirm, Product product)
        {
            return new ProductMessage(myId, confirm.to, product, confirm.transaction_id);
        }
    }
}
