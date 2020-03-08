using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleThreads
{
    class Counter
    {
        volatile private int value = 0;

        public void Next() {
            lock (this)
            {
                value++;
            }
        }

        public int Value()
        {
            lock (this)
            {
                return value;
            }
        }
    }
}
