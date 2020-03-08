using System;
using System.Threading;

namespace SimpleThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starujemy");
            Counter counter = new Counter();

            for (int i=0; i<1000; i++)
            {
                MyTask task = new MyTask(counter);
                Thread t = new Thread(task.toDo);
                t.Start();
            }
            Thread.Sleep(3000);
            Console.WriteLine(counter.Value());
        }
    }
}
