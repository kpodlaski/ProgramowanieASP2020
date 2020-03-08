using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.OneQueueToAllClerks
{
    class Clerk_OQAC : Clerk
    {
        public volatile bool isAviable = true;
        public Clerk_OQAC(string name) : base(name) { }
        }
    }
}
