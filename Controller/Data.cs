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
            Competition = new Competition();
            addParticipants();
            addTracks();
        }
        public static void addParticipants()
        {
            Competition.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            Competition.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Herman", 0, new Car(0, 0, 0, false), TeamColors.Green));
            Competition.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Rob", 0, new Car(0, 0, 0, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Jordy", 0, new Car(0, 0, 0, false), TeamColors.Red));
            //Competition.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Red));
        }
        public static void addTracks()
        {
            //SectionTypes[] sect1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };

            SectionTypes[] sect1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner

            , SectionTypes.Straight, SectionTypes.Straight};
            //SectionTypes[] sect2 = { SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner };
            //SectionTypes[] sect3 = { SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Track track1 = new Track("Racetrack1", sect1);
            //Track track2 = new Track("Racetrack2", sect2);
            //Track track3 = new Track("Racetrack3", sect3);
            Competition.Tracks.Enqueue(track1);
            //Competition.Tracks.Enqueue(track2);
            //Competition.Tracks.Enqueue(track3);
        }
        //TODO: Check with teacher is .Nexttrack pops a track 2 times from queuee.
        public static void NextRace()
        {
            Track Race = Competition.NextTrack();

            if (Race != null)
            {
                CurrentRace = new Race(Race, Competition.Participants);
            }
        }
    }


}
