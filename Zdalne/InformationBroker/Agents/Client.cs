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
        List<Product> buyed = new List<Product>();
        Dictionary<ProductInfo, int> toBuy = new Dictionary<ProductInfo, int>();
        //List<SellRequestMessage> sellreq = new List<SellRequestMessage>();
        Dictionary<ProductInfo, List<OfferAnswerMessage>> offers = new Dictionary<ProductInfo, List<OfferAnswerMessage>>();

        public Client(Broker broker) : base(broker, AgentType.Client) { }

        protected override void queueWorker()
        {
            while (!this.closing || mq.Count() > 0)
            {
                Message msg = mq.peekMessage();
                if (msg == null)
                {
                    Thread.Sleep(10);
                    continue;
                }
                switch (msg.type)
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
                Product p = msg.product;
                lock (buyed)
                {
                    buyed.Add(p);
                    Console.WriteLine("Client {0} bought {1}, for {2}$ and {3} copies", agentId, p.info, p.unitPrice, p.quantity);
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
            Console.WriteLine("Agent {0}, has offer {1}, {2}, {3}", agentId, msg.product, msg.price, msg.quantityAviable);
            lock (offers)
            {
                if (!offers.ContainsKey(msg.product))
                {
                    offers[msg.product] = new List<OfferAnswerMessage>();
                }
                offers[msg.product].Add(msg);
            }
        }

        public void addProductToBuy(ProductInfo p, int quantity)
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
                Message msg = messageFactory.buildOfferRequest(product);
                Console.WriteLine("Agent {0} send request for {1}", agentId, product);
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
                    _offers.Sort((o1, o2) => { return Math.Sign(o1.price - o2.price); } );
                    OfferAnswerMessage offer = _offers[0];
                    //We should check the quantity ;-)
                    Message sellReq = messageFactory.buildSellRequest(offer, quantity);
                    sendMessage(sellReq);
                });
                t.Start();
            }
        }

    }

}
