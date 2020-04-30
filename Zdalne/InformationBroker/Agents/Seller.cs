using InformationBroker.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InformationBroker.Model;

namespace InformationBroker.Agents
{
    class Seller : CommunicationAgent
    {
        private Dictionary<ProductInfo, Product> aviableProductsMap;
        private int tid = 0;
        
        public Seller(Broker broker): base(broker,AgentType.Seller) {
            aviableProductsMap = new Dictionary<ProductInfo, Product>();
        }

        protected override void queueWorker()
        {
            while(!this.closing || mq.Count() > 0)
            {
                Message msg = mq.peekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.type)
                {
                    case MessageType.OfferRequest: newOfferRequest( (OfferRequestMessage) msg); break;
                    case MessageType.SellRequest: newSellRequest((SellRequestMessage) msg); break;
                    default: break;
                }
            }
        }

        private void newOfferRequest(OfferRequestMessage msg)
        {
            Product p = null;
            lock (aviableProductsMap)
            {
                if (aviableProductsMap.ContainsKey(msg.product)){
                    p = aviableProductsMap[msg.product];
                }
                else return;
            }
            if (p == null) return;
            Message reply = messageFactory.buildOfferAnswer(msg, p.unitPrice, p.quantity);
            sendMessage(reply);
        }

        private void newSellRequest(SellRequestMessage msg)
        {
            Product p = null;
            int quantity;
            lock (aviableProductsMap)
            {
                if (aviableProductsMap.ContainsKey(msg.product)){
                    p = aviableProductsMap[msg.product];
                    quantity = Math.Min(p.quantity, msg.quantity);
                    p.quantity -= quantity;
                    if (p.quantity == 0)
                    {
                        aviableProductsMap.Remove(p.info);
                    }
                }
                else return;
            }
            if (p == null) return;

            Product product = new Product(p, quantity);
            Message reply = messageFactory.buildSellConfirmation(msg, agentId + "_" + tid, product.quantity, product.unitPrice);
            sendMessage(reply);
            tid++;
            reply = messageFactory.sendProduct((SellConfirmMessage) reply, product);
            sendMessage(reply);
            Console.WriteLine("Seller {0} sold {1}, for {2}$ and {3} copies", agentId, product.info, product.unitPrice, product.quantity);
        }

        public void addNewProduct(ProductInfo product, double price, int quantity)
        {
            lock (aviableProductsMap)
            {
                if (!aviableProductsMap.ContainsKey(product))
                {
                    aviableProductsMap[product] = new Product(product, price, quantity);
                }
                else {
                    aviableProductsMap[product].unitPrice = price;
                    aviableProductsMap[product].quantity += quantity;
                }
            }
        }
    }
}
