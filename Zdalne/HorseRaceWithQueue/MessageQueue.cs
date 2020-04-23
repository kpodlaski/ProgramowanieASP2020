using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseRaceWithQueue
{
    class MessageQueue
    {
        private Queue<Record> queue = new Queue<Record>();

        public void addRecord(Record record)
        {
            lock (this)
            {
                queue.Enqueue(record);
            }
        }

        public Record peekRecord()
        {
            Record record = null;
            lock (this)
            {
                if (queue.Count > 0)
                {
                    record = queue.Dequeue();
                }
            }
            return record;
        }

        public int Count()
        {
            lock (this)
            {
                return queue.Count();
            }
        }
    }
}
