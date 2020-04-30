using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Model
{
    class Product
    {
        public ProductInfo Info;
        public double UnitPrice;
        public int Quantity;
        

        public Product(Product p, int quantity)
        {
            this.Info = p.Info;
            this.UnitPrice = p.UnitPrice;
            this.Quantity = quantity;
        }

        public Product(ProductInfo pInfo, double unitPrice, int quantity)
        {
            this.Info = pInfo;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
        }
    }
}
