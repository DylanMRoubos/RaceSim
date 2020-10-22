using System;
using System.Collections.Generic;
using Model;

namespace Controller
{

    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition("W1 20/21");
            AddParticipants();
            AddTracks();

        }
        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 8, false), TeamColors.Grey));
            Competition.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 10, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Herman", 0, new Car(0, 0, 6, false), TeamColors.Green));
            Competition.Participants.Add(new Driver("Max", 0, new Car(0, 0, 8, true), TeamColors.Red));
            //Competition.Participants.Add(new Driver("Rob", 0, new Car(0, 0, 0, false), TeamColors.Red));
            //Competition.Participants.Add(new Driver("Jordy", 0, new Car(0, 0, 0, false), TeamColors.Red));
            //Competition.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Red));
        }
        public static void AddTracks()
        {
            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner
            , SectionTypes.Straight, SectionTypes.Straight};
            SectionTypes[] sections2 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner };
            SectionTypes[] sections3 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            SectionTypes[] sections4 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner };
            SectionTypes[] sections5 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid,SectionTypes.Finish, SectionTypes.Straight,
                SectionTypes.LeftCorner, SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight};
            SectionTypes[] sections6 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner };

            Track track1 = new Track("Monaco", sections1);
            Track track2 = new Track("Zandvoord", sections2);
            Track track3 = new Track("Nascar", sections3);
            Track track4 = new Track("Nascar1", sections4);
            Track track6 = new Track("Nascar3", sections6);
            Track oostendorp = new Track("Oostendorp", sections5);

            //Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
            Competition.Tracks.Enqueue(track6);
            //Competition.Tracks.Enqueue(oostendorp);
            Competition.Tracks.Enqueue(track3);
            
            Competition.Tracks.Enqueue(track4);
           
        }

        public static void NextRace()
        {
            //If a current race ended add points to the driver
            if (CurrentRace != null) AddPointsToDrivers();

            Track Race = Competition.NextTrack();

            if (Race != null)
            {
                CurrentRace = new Race(Race, Competition.Participants);
            }
        }

        public static void AddPointsToDrivers()
        {
            Competition.AddPointsToDirvers(CurrentRace.FinishPosition);
        }
    }


}
