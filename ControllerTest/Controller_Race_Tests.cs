using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tests
{
    [TestFixture]
    class Controller_Race_Tests
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

        //Check if you can add a brokendown driver to list
        [TestCase("Erik", 0)]
        [TestCase("Thomas", 1)]
        public void Controller_Race_AddDriverBrokenDown(string output, int location)
        {
            race.AddDriverBrokenDown(_competition.Participants[location].Name);

            Assert.AreEqual(output, race.brokenDownAmount.GetHighest());
        }

        //Check if brokentogler workd
        [TestCase(false)]
        public void Controller_Race_BrokenToggler(bool output)
        {
            race.BrokenToggler(_competition.Participants[0]);

            Assert.IsTrue(output == _competition.Participants[0].Equipment.IsBroken || output != _competition.Participants[0].Equipment.IsBroken);
        }
    }
}
