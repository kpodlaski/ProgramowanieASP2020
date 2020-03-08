using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PostOffice.OneQueueToAllClerks
{
    class PostOffice_OQAC : PostOffice
    {
        private Semaphore _pool;

        public PostOffice_OQAC() : base()
        {
            Console.WriteLine("Otwieramy Pocztę: One Client one Queue");
            allClerks.Add(new Clerk_OQAC("A"));
            allClerks.Add(new Clerk_OQAC("B"));
            allClerks.Add(new Clerk_OQAC("C"));
            _pool = new Semaphore(0, 3);
        }

        protected override Clerk nextClerk()
        {
            Clerk c = null;
            while (c == null)
            {
                _pool.WaitOne();
                lock (this)
                {
                    c = allClerks[0];

                }
                _pool.Release();
            }

             return c;
        }
    }
}
