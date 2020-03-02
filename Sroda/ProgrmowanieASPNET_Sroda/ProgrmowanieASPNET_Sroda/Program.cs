using System;
using System.Collections.Generic;
using System.Threading;

namespace ProgrmowanieASPNET_Sroda
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();
            List<Thread> threads = new List<Thread>();
            for (int i=0; i<1000; i++)
            {
                SingleThreadAction sTA = new SingleThreadAction(counter);
                Thread t = new Thread(sTA.Do);
                t.Start();
                threads.Add(t);
                t = new Thread(() =>{
                    for (int i = 0; i < 40; i++)
                    {
                        lock (counter)
                        {
                            counter.Add();
                        }
                    }
               });
               t.Start();
               threads.Add(t);
            }
            //Thread.Sleep(3000);
            foreach(Thread t in threads){
                t.Join();
            }
            Console.WriteLine(counter.Value());
        }
    }
}
