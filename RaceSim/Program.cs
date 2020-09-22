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
            //Console.WriteLine(Data.CurrentRace.Track.Name);

            TrackVisualisation.DrawTrack(Data.CurrentRace.Track);

            //for (; ; )
            //{
            //    Thread.Sleep(100);
            //}

        }
    }
}
