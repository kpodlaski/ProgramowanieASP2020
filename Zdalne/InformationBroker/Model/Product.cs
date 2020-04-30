using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Model
{
    class Product
    {
        public ProductInfo info;
        public double unitPrice;
        public int quantity;
        

        public Product(Product p, int quantity)
        {
            this.info = p.info;
            this.unitPrice = p.unitPrice;
            this.quantity = quantity;
        }

        public Product(ProductInfo pInfo, double unitPrice, int quantity)
        {
            this.info = pInfo;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
        }
    }
}
