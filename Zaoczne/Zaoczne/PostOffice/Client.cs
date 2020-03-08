using System;

namespace PostOffice
{
    public class Client
    {
        public int Id { get; }

        private PostOffice postOffice;

        public Client(int id, PostOffice po) {
            this.Id = id;
            this.postOffice = po;
        }
            
        public void VisitPost()
        {
            Clerk c = postOffice.ClientArrive();
            c.ServeClient(this);
            Console.WriteLine("Klient " + Id + " idzie do domu");
        }
    }
}