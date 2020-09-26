using System;
using System.Collections.Generic;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_GetSectionData
    {
        Competition _competition;

        [SetUp]
        public void SetUp()
        {
            //Create Competition with 1 track & 2 drivers
            Competition comp = new Competition();
            comp.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            comp.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            comp.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            comp.Tracks.Enqueue(new Track("Monaco", sections1));

            _competition = comp;
        }
        //If sectionData does not exist return new object
        [Test]
        public void Should_Return_New_Object()
        {
            var track = _competition.Tracks.Dequeue();
            var section = track.Sections.First.Value;

            var race = new Race(track, _competition.Participants);
            var results = race.GetSectionData(section);

            Assert.IsNotNull(results);
        }

        //If sectionData exists return that object
        [Test]
        public void Should_Return_Existing_Object()
        {
            var track = _competition.Tracks.Dequeue();

            var section = track.Sections.First.Value;
            var race = new Race(track, _competition.Participants);

            var sectionData = race.GetSectionData(section);
            var results = race.GetSectionData(section);

            sectionData.DistanceLeft = 1;

            Assert.IsTrue(results.DistanceLeft == 1);
        }
    }
}
