using System;

namespace HorseRace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Race !!!");
            Horse.SleepInterval = 10;
            Race race = new Race(3000, 
                            new string[] { "Mornig Star", "Sprakle", "Black Storm", "Froggie", "Flopper" }, 
                            new ConsoleUI());
            race.StartRace();
        }
    }

    class ConsoleUI : IUI
    {
        public void message(string v, params object[] p)
        {
            Console.WriteLine(v, p);
        }

        public void updateHorse(Horse horse)
        {
            //DO NOTHING
        }
    }
}
