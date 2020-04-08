using System;

namespace HorseRace
{
    public interface IUI
    {
        public void updateHorse(Horse horse);
        public void message(string v, params Object[] p);
    }
}