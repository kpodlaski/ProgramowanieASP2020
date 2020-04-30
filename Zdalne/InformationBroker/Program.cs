using InformationBroker.Agents;
using InformationBroker.Model;
using System;
using System.Collections.Generic;

namespace InformationBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductInfo[] books = new ProductInfo[]{
                new ProductInfo { Author = "J.R.R Tolkien", Title = "Silmarillion" },
                new ProductInfo { Author = "J. Grzędowicz", Title = "Pan Lodowego Ogrodu" },
                new ProductInfo { Author = "L. Carrol", Title = "Alice in Wonderland" },
                new ProductInfo { Author = "A. Sapkowski", Title = "Krew Elfów" },
            };
            Broker broker = new Broker();
            Seller seller = new Seller(broker);
            seller.addNewProduct(books[1], 54.50, 100);
            seller.addNewProduct(books[2], 12.80, 100);
            seller.addNewProduct(books[3], 24.20, 100);
            seller = new Seller(broker);
            seller.addNewProduct(books[1], 84.30, 100);
            seller.addNewProduct(books[2], 12.20, 100);
            seller.addNewProduct(books[0], 35.30, 100);
            seller = new Seller(broker);
            seller.addNewProduct(books[0], 33.30, 100);
            seller.addNewProduct(books[2], 12.20, 100);
            seller.addNewProduct(books[3], 35.30, 100);
            Client[] client = new Client[] { new Client(broker), new Client(broker), new Client(broker) };
            client[0].addProductToBuy(books[2], 7);
            client[0].addProductToBuy(books[0], 2);
            client[0].addProductToBuy(books[2], 3);

            client[1].addProductToBuy(books[3], 7);
            client[1].addProductToBuy(books[2], 1);

            client[2].addProductToBuy(books[0], 15);
            client[2].addProductToBuy(books[1], 5);

            foreach(Client c in client)
            {
                c.StartBuyOperation();
            }
        }
    }
}
