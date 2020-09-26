using System;
using System.Threading;
using Controller;

namespace RaceSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            TrackVisualisation.Initialize();

            for (; ; )
            {
                Thread.Sleep(15000);
            }

        }
    }
}
