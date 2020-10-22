using System;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_AddDrivenDistance
    {
        Competition _competition;
        Race race;

        [SetUp]
        public void SetUp()
        {
            //Create Competition with 1 track & 2 drivers
            Competition comp = new Competition("W1 20/21");
            comp.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            comp.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            comp.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            comp.Tracks.Enqueue(new Track("Monaco", sections1));

            _competition = comp;

            race = new Race(_competition.NextTrack(), _competition.Participants);
        }
        //Check if participants are added to the CurrentRace
        [Test]
        public void Should_Add_DrivenDistance_To_List()
        {
            race.AddDrivenDistance(race.Participants.First().Name, 100);
            Assert.AreEqual(race.distanceDriven.GetHighest(), "Erik");
        }

    }
}
