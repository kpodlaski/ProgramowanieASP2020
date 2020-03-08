using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PostOffice
{
    public class Clerk
    {
        public char Id { get; }
        private Random rand = new Random();

        public Clerk(char id) {
            this.Id = id;
        }

        public void ServeClient(Client c)
        {
            //ToDO SOmething
            Console.WriteLine("Urzędnik " + Id + " obsługuje klienta " + c.Id);
            Thread.Sleep(rand.Next() % 200); 
        }
    }
}
