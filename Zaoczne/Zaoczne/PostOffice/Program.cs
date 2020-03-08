using System;
using System.Collections.Generic;
using System.Threading;

namespace PostOffice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Otwieramy Pocztę");
            List<Thread> threads = new List<Thread>();
            PostOffice postOffice = new PostOffice();
            for (int i=0; i < 100; i++)
            {
                Client c = new Client(i + 1, postOffice);
                threads.Add(new Thread(c.VisitPost));
            }
            foreach(Thread t in threads)
            {
                t.Start();
            }
        }
    }
}
