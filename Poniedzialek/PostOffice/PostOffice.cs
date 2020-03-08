using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice
{
    abstract class PostOffice
    {
        protected List<Clerk> allClerks = new List<Clerk>();

        virtual public void serveMe(Client client)
        {
            //Czekaj na swoją kolej
            //Wybor urzędnika odpowiedniego do klienta
            Clerk c = nextClerk();
            c.serve(client);
        }

        abstract protected Clerk nextClerk();
    }
}
