using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice
{
    public class PostOffice
    {
        List<Clerk> clerks = new List<Clerk>();

        public PostOffice()
        {
            clerks.Add(new Clerk('A'));
            clerks.Add(new Clerk('B'));
            clerks.Add(new Clerk('C'));
        }

        public Clerk ClientArrive()
        {
            //TODO
            //Return appropriate Clerk to Client
            return null;
        }
    }
}
