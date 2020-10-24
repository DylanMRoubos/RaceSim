using System;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data
    {
        Competition _competition;
        Race race;

        [SetUp]
        public void SetUp()
        {
            //Create Competition
            Competition comp = new Competition("W1 20/21");

            //Add 2 drivers
            comp.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            comp.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            //Create Sections
            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            
            //Add track to competition
            comp.Tracks.Enqueue(new Track("Monaco", sections1));

            //Make competition available in class
            _competition = comp;

            //Set the first Race in comp as race
            race = new Race(_competition.NextTrack(), _competition.Participants);
        }

        //Check if Comp data is initialized with correct values
        [TestCase("W1 20/21")]
        public void Controller_Data_Initialize(string CompName)
        {
            Data.Initialize();

            Assert.AreEqual(Data.Competition.Name, CompName);
        }

        //Check if Next race is done
        [TestCase("Zandvoord")]
        public void Controller_Data_NextRace(string TrackName)
        {
            Data.NextRace();

            Data.NextRace();

            Assert.AreEqual(TrackName, Data.CurrentRace.Track.Name);
        }
    }
}
