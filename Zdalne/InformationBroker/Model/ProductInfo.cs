using System;
using System.Collections.Generic;
using System.Text;

namespace InformationBroker.Model
{
    class ProductInfo
    {
        public String Author;
        public String Title;

        public override bool Equals(object obj)
        {
            if (obj is ProductInfo p) {
                return Author.Equals(p.Author) && Title.Equals(p.Title);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode()+21*Author.GetHashCode();
        }

        public override string ToString()
        {
            return Author + ", "+Title;
        }

    }
}
