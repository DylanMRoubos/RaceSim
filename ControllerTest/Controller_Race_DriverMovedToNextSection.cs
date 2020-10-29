/*using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    class Controller_Race_DriverMovedToNextSection
    {
        Competition _competition;

        [SetUp]
        public void SetUp()
        {
            //Create Competition with 1 track & 2 drivers
            Competition comp = new Competition("W1");
            comp.Participants.Add(new Driver("Erik", 0, new Car(0, 0, 0, false), TeamColors.Grey));
            comp.Participants.Add(new Driver("Thomas", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            comp.Participants.Add(new Driver("Max", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            SectionTypes[] sections1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner };
            comp.Tracks.Enqueue(new Track("Monaco", sections1));

            _competition = comp;
        }
        [Test]
        public void Driver_Moved()
        {
            var track = _competition.Tracks.Dequeue();

            var race = new Race(track, null);
            race.Initialize();

            var result = race.DriverMovedToNextSection(race.Track.Sections.Last, race.Track.Sections.First, 1, DateTime.Now);

            Assert.AreEqual(result, false);
        }
    }
}
*/