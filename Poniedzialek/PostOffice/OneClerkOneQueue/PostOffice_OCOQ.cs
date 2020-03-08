using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.OneClerkOneQueue
{
    class PostOffice_OCOQ: PostOffice
    {
        public PostOffice_OCOQ() : base()
        {
            Console.WriteLine("Otwieramy Pocztę: One Client one Queue");
            allClerks.Add(new Clerk_OCOQ("A"));
            allClerks.Add(new Clerk_OCOQ("B"));
            allClerks.Add(new Clerk_OCOQ("C"));
        }

        protected override Clerk nextClerk()
        {
            lock (this) {
                Clerk_OCOQ c = (Clerk_OCOQ)allClerks[0];
                int last_size = c.QueueSize();
                for (int i = 1; i < allClerks.Count; i++)
                {
                    Clerk_OCOQ ct = (Clerk_OCOQ)allClerks[i];
                    int st = ct.QueueSize();
                    if (st < last_size)
                    {
                        last_size = st;
                        c = ct;
                    }
                }
                c.AddToQueue();
                return c;
            }        
        }

        public override void serveMe(Client client)
        {
            base.serveMe(client);
        }
    }
}
