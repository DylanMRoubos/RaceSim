using System;
using System.Threading;
using Controller;

namespace RaceSim
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init the race
            Data.Initialize();
            Data.NextRace();
            //Visualize the race to the console
            TrackVisualisation.Initialize();
            TrackVisualisation.DrawTrack(Data.CurrentRace.Track);

            //Keep the terminal open while racing
            for (; ; )
            {
                Thread.Sleep(15000);
            }

        }
    }
}
