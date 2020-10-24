using System;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Controller_RaceStartEventArgs
    {
        public static Race Race;
        private Competition Comp;
        [SetUp]
        public void SetUp()
        {
            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            //Add track to competition
            
            //Create Competition
            Comp = new Competition("W1 20/21");

            //Add track to competition
            Comp.Tracks.Enqueue(new Track("Monaco", sections1));

            //Add 2 drivers
            Comp.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            Comp.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            Race = new Race(Comp.NextTrack(), Comp.Participants);
        }

        //Check if RaceStartEventArgs data is initialized with correct values
        [TestCase("Monaco")]
        public void Controller_RaceStartEventArgs_Create(string TrackName)
        {
            RaceStartEventArgs RaceEventArgs= new RaceStartEventArgs(Race);

            Assert.AreEqual(RaceEventArgs.Race.Track.Name, TrackName);
        }
    }
}
