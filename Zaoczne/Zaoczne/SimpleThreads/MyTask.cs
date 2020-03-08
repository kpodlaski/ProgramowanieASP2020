using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleThreads
{
    class MyTask
    {
        private Counter counter;

        public MyTask(Counter counter)
        {
            this.counter = counter;
        }

        public void toDo()
        {
            for(int i=0; i<40; i++)
            {
                lock (counter)
                {
                    counter.Next();
                }
            }
        }
    }
}
