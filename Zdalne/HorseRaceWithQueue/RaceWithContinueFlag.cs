using HorseRace;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HorseRaceWithQueue
{
    internal class RaceWithContinueFlag : Race
    {
        public volatile Boolean RaceInProgress = false;
        public RaceWithContinueFlag(int distance, String[] horseNames, IUI ui) : base(distance, horseNames, ui)
        {
        }

        //The virtual atributte have been added to Race.StartRace method
        public override void StartRace()
        {
            RaceInProgress = true;
            base.StartRace();
        }

        //The virtual and protected atributtes have been added to Race.raceRace method
        protected override void raceEnded(Barrier obj)
        {
            base.raceEnded(obj);
            RaceInProgress = false;
        }

    }
}
