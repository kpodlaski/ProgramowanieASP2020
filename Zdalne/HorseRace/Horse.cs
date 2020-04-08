using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseRace
{
    public class Horse : IComparable<Horse>
    {
        private static Random rand = new Random();
        public static int MaxVelocity = 19;
        public static int SleepInterval = 0;
        public static int scaleFactor = 10;

        public String Name {get; private set;}
        //Zakładam distance w Race to decymetry
        private int distance;
        private volatile int X = 0;
        public int Position { get { return X / scaleFactor; } }
        //Zakłądam prędkość to metry na sekundę
        private int V;
        private Barrier startBarrier, finishBarrier;
        public double finalRaceTime;
        private Thread thread;
        private IUI ui;

        public Horse(String name, int distance, Barrier startBarrier, Barrier finishBarrier, IUI ui)
        {
            this.Name = name;
            this.V = rand.Next() % MaxVelocity + 1;
            this.startBarrier = startBarrier;
            this.finishBarrier = finishBarrier;
            this.distance = scaleFactor*distance;
            this.thread = new Thread(this.Race);
            this.ui = ui;
        }

        public void Run()
        {
            thread.Start();
        }

        private void Race()
        {
            Console.WriteLine("Horse {0}, ready", Name);
            startBarrier.SignalAndWait();
            int time =0;
            //Jedna iteracja == 0.1  sekundy dla scaleFactor =10
            while (X < distance)
            {
                X +=  V;
                time++;
                //Możemy dodać kod przyspieszania/zwalniania jeśli koń startuje lub jest zmęczony
                //Spowalnianie pętli, Sleep niestety jak by synchronizował wątki co widać w wynikach
                //Dlaczego ??
                if (SleepInterval>0) Thread.Sleep(SleepInterval);
                ui.updateHorse(this);
            }
            finalRaceTime = time/scaleFactor;
            ui.message("Horse {0}, finished with result {1}, v={2}", Name, finalRaceTime, V);
            finishBarrier.SignalAndWait();
            ui.message("Horse {0}, leave the race trace", Name);
        }

        public int CompareTo([AllowNull] Horse other)
        {
            return this.finalRaceTime.CompareTo(other.finalRaceTime);
        }
    }
}
