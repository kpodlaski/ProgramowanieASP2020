using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrmowanieASPNET_Sroda
{
    class SingleThreadAction
    {
        Counter counter;

        public SingleThreadAction(Counter counter)
        {
            this.counter = counter;
        }

        public void Do()
        {

        //lock (counter) { 
                for (int i=0; i<40; i++){
                lock (counter)
                {
                    counter.Add();
                }
        //    }
        }

    }
    }
}
