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
            Console.WriteLine("Otwieramy Pocztę: One Queue for all Clerks");
            _pool = new Semaphore(3, 3);
            allClerks.Add(new Clerk_OQAC("A"));
            allClerks.Add(new Clerk_OQAC("B"));
            allClerks.Add(new Clerk_OQAC("C"));
            
        }

        protected override Clerk nextClerk()
        {
            Clerk c = null;
            while (c == null)
            {
                _pool.WaitOne();
                lock (this)
                {
                    if (allClerks.Count > 0)
                    {
                        c = allClerks[0];
                        allClerks.Remove(c);
                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                    
                }
            }

             return c;
        }

        public override void serveMe(Client client)
        {
            Clerk c = nextClerk();
            c.serve(client);
            lock (this)
            {
                allClerks.Add(c);
            }
            _pool.Release();
        }

    }
}
