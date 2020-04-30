using InformationBroker.Messages;
using InformationBroker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InformationBroker.Agents
{
    class Client : CommunicationAgent
    {
        private List<Product> buyed = new List<Product>();
        private Dictionary<ProductInfo, int> toBuy = new Dictionary<ProductInfo, int>();
        //List<SellRequestMessage> sellreq = new List<SellRequestMessage>();
        private Dictionary<ProductInfo, List<OfferAnswerMessage>> offers = new Dictionary<ProductInfo, List<OfferAnswerMessage>>();

        public Client(Broker broker) : base(broker, AgentType.Client) { }

        protected override void queueWorker()
        {
            while (!this.closing || mq.Count() > 0)
            {
                Message msg = mq.PeekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.Type)
                {
                    case MessageType.OfferAnswer: newOfferAnswer((OfferAnswerMessage)msg); break;
                    case MessageType.SellConfirm: newSellConfirm((SellConfirmMessage)msg); break;
                    case MessageType.Product: newProductArrived((ProductMessage)msg); break;
                    default: break;
                }
            }
        }

        private void newProductArrived(ProductMessage msg)
        {
            if (packageWasExpected(msg))
            {
                Product p = msg.ProductInfo;
                lock (buyed)
                {
                    buyed.Add(p);
                    Console.WriteLine("Client {0} bought {1}, for {2}$ and {3} copies", AgentId, p.Info, p.UnitPrice, p.Quantity);
                }
            }
        }

        private bool packageWasExpected(ProductMessage msg)
        {
            //TODO check if the packge was expected
            return true;
        }

        private void newSellConfirm(SellConfirmMessage msg)
        {
            //We can check if we make such an order sellreq
            //Remember the transaction_id for payment, check the packages
        }

        private void newOfferAnswer(OfferAnswerMessage msg)
        {
            Console.WriteLine("Agent {0}, has offer {1}, {2}, {3}", AgentId, msg.ProductInfo, msg.Price, msg.QuantityAviable);
            lock (offers)
            {
                if (!offers.ContainsKey(msg.ProductInfo))
                {
                    offers[msg.ProductInfo] = new List<OfferAnswerMessage>();
                }
                offers[msg.ProductInfo].Add(msg);
            }
        }

        public void AddProductToBuy(ProductInfo p, int quantity)
        {
            lock (toBuy)
            {
                if (!toBuy.ContainsKey(p))
                {
                    toBuy[p] = 0;
                }              
                toBuy[p]+= quantity;
            }
        }
        public void StartBuyOperation()
        {
            foreach(ProductInfo product in toBuy.Keys)
            {
                Message msg = messageFactory.BuildOfferRequest(product);
                Console.WriteLine("Agent {0} send request for {1}", AgentId, product);
                sendMessage(msg);
                //Lambda in loop cannot use loop variable like product, it have to be iteration scope variable.
                ProductInfo _product = product;
                int quantity = toBuy[product];
                Thread t = new Thread(() =>
                {
                    //Now wait fror answers and check offers
                    Thread.Sleep(3000);
                    Console.WriteLine("Check offers for product " + _product);
                    List<OfferAnswerMessage> _offers = null;
                    lock (offers)
                    {
                        _offers = offers[_product];
                        offers.Remove(_product);
                    }
                    if (_offers == null || _offers.Count == 0) return;
                    _offers.Sort((o1, o2) => { return Math.Sign(o1.Price - o2.Price); } );
                    OfferAnswerMessage offer = _offers[0];
                    //We should check the quantity ;-)
                    Message sellReq = messageFactory.BuildSellRequest(offer, quantity);
                    sendMessage(sellReq);
                });
                t.Start();
            }
        }

    }

}
