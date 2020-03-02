using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrmowanieASPNET_Sroda
{
    class Counter
    {
        volatile private int value;

        public void Add()
        {
            //lock (this) {
                value++;
            //}
        }

        public int Value()
        {
            return value;
        }
    }
}
