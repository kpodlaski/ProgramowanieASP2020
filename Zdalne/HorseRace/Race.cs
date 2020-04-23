using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HorseRace
{
    public class Race
    {
        //Zakładam distance w Race to metry
        private int distance;
        private int numberOfHorses;
        private List<Horse> horses = new List<Horse>();
        private Barrier startBarrier, finishBarrier;
        private IUI ui;

        
        public Race(int distance, String[] horseNames, IUI ui)
        {
            this.distance = distance;
            this.numberOfHorses = horseNames.Length;
            startBarrier = new Barrier(this.numberOfHorses, racePrpared);
            finishBarrier = new Barrier(this.numberOfHorses, raceEnded);
            this.ui = ui;
            for (int i=0; i<numberOfHorses; i++)
            {
                String horseName = horseNames[i];
                horses.Add(new Horse(horseName, this.distance, startBarrier, finishBarrier, ui));
            }
        }



        public virtual void StartRace()
        {
            foreach (Horse horse in horses)
            {
                horse.Run();
            }
        }

        protected virtual void raceEnded(Barrier obj)
        {
            Console.WriteLine("All Horses finished, Results:");
            horses.Sort();
            foreach( Horse h in horses)
            {
                //ui.message może być zastąpione Console.WriteLine
                ui.message("\t{0} \t....\t {1}s", h.Name, h.finalRaceTime);
            }
        }

        protected virtual void racePrpared(Barrier obj)
        {
            ui.message("Horses are prepared, Start");
        }

    }
}
