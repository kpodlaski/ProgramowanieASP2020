using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.OneClerkOneQueue
{
    class Clerk_OCOQ : Clerk
    {
        private volatile int queueSize = 0;
        private String queueMonitor = "QueueMonitor";

        public void AddToQueue()
        {
            lock (queueMonitor)
            {
                queueSize++;
            }
        }

        public int QueueSize()
        {
            lock (queueMonitor)
            {
                return queueSize;
            }
        }

        public Clerk_OCOQ(string name) : base(name){}

        public override void serve(Client c)
        {
            lock (this)
            {
                base.serve(c);
            }
        }
    }
}
