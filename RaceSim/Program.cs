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

            TrackVisualisation.Initialize(Data.CurrentRace.Track);
            TrackVisualisation.DrawTrack();

            Data.NextRace();
            TrackVisualisation.Initialize(Data.CurrentRace.Track);
            TrackVisualisation.DrawTrack();

            Data.NextRace();
            TrackVisualisation.Initialize(Data.CurrentRace.Track);
            TrackVisualisation.DrawTrack();

            //for (; ; )
            //{
            //    Thread.Sleep(100);
            //}

        }
    }
}
