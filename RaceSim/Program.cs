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
            //Data.NextRace();


            TrackVisualisation.Initialize();
            TrackVisualisation.DrawTrack(Data.CurrentRace.Track);

            for (; ; )
            {
                Thread.Sleep(15000);
            }

        }
    }
}
