using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PostOffice
{
    abstract class Clerk
    {
        public string Name { get; set; }
        private Random rand = new Random();
       
        public Clerk(string name)
        {
            this.Name = name;
        }

        virtual public void serve(Client c)
        {
            //Możemy założyć lock, kto jest monitorem ?
            Console.WriteLine("Urzędnik " + Name + " obsługuje klienta " + c);
            // Różny czas czekania dla różnych spraw
            Thread.Sleep(rand.Next() % 500);
        }
    }
}
