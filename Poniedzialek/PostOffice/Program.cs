using PostOffice.OneClerkOneQueue;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PostOffice
{
    class Program
    {
        static void Main(string[] args)
        {
            //One Clerk One Queue
            PostOffice po = new PostOffice_OCOQ();
            List<Client> clients = new List<Client>();
            List<Thread> clientsTh = new List<Thread>();
            for ( int i= 0; i<100; i++)
            {
                Client c = new Client(i, po);
                clients.Add(c);
                clientsTh.Add(new Thread(c.Task));
            }

            Console.WriteLine("Start obsługi klientów");
            foreach(Thread t in clientsTh)
            {
                t.Start();
            }

        }
    }
}
