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
                Message msg = mq.PeekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.Type)
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
                if (aviableProductsMap.ContainsKey(msg.ProductInfo)){
                    p = aviableProductsMap[msg.ProductInfo];
                }
                else return;
            }
            if (p == null) return;
            Message reply = messageFactory.BuildOfferAnswer(msg, p.UnitPrice, p.Quantity);
            sendMessage(reply);
        }

        private void newSellRequest(SellRequestMessage msg)
        {
            Product p = null;
            int quantity;
            lock (aviableProductsMap)
            {
                if (aviableProductsMap.ContainsKey(msg.ProductInfo)){
                    p = aviableProductsMap[msg.ProductInfo];
                    quantity = Math.Min(p.Quantity, msg.Quantity);
                    p.Quantity -= quantity;
                    if (p.Quantity == 0)
                    {
                        aviableProductsMap.Remove(p.Info);
                    }
                }
                else return;
            }
            if (p == null) return;

            Product product = new Product(p, quantity);
            Message reply = messageFactory.BuildSellConfirmation(msg, AgentId + "_" + tid, product.Quantity, product.UnitPrice);
            sendMessage(reply);
            tid++;
            reply = messageFactory.SendProduct((SellConfirmMessage) reply, product);
            sendMessage(reply);
            Console.WriteLine("Seller {0} sold {1}, for {2}$ and {3} copies", AgentId, product.Info, product.UnitPrice, product.Quantity);
        }

        public void AddNewProduct(ProductInfo product, double price, int quantity)
        {
            lock (aviableProductsMap)
            {
                if (!aviableProductsMap.ContainsKey(product))
                {
                    aviableProductsMap[product] = new Product(product, price, quantity);
                }
                else {
                    aviableProductsMap[product].UnitPrice = price;
                    aviableProductsMap[product].Quantity += quantity;
                }
            }
        }
    }
}
