using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRaceWithQueue
{
    class Record
    {
        public String HorseName { get; set; }
        public int Position { get; set; }

        public Record(String name, int position)
        {
            this.HorseName = name;
            this.Position = position;
        }
    }
}
