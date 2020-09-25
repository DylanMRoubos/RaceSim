using System;
using System.Collections.Generic;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_AddParticipantsToTrack
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

        //Test if a driver is placed on the first startgrid from the finishline
        [Test]
        public void Should_Add_Driver_To_First_StartGrid()
        {

            var startGrids = new Stack<Section>();
            var track = _competition.Tracks.Dequeue();

            var race = new Race(track, _competition.Participants);

            race.AddParticipantsToTrack(track, _competition.Participants);

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            var result = startGrids.Pop();

            Assert.IsTrue(race.GetSectionData(result).Left != null);
        }
        //Test if both drivers are placed on the first startgrid from the finishline
        [Test]
        public void Should_Add_Both_Driver_To_First_StartGrid()
        {

            var startGrids = new Stack<Section>();
            var track = _competition.Tracks.Dequeue();

            var race = new Race(track, _competition.Participants);

            race.AddParticipantsToTrack(track, _competition.Participants);

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            var result = startGrids.Pop();

            Assert.IsTrue(race.GetSectionData(result).Left != null && race.GetSectionData(result).Right != null);
        }

        //Test if all drivers are placed on the startgrid
        [Test]
        public void Should_Add_All_Drivers_To_StarGrid()
        {

            var startGrids = new Stack<Section>();
            var track = _competition.Tracks.Dequeue();
            var remainingDrivers = _competition.Participants.Count;

            var race = new Race(track, _competition.Participants);

            race.AddParticipantsToTrack(track, _competition.Participants);

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            //loop through start grids if found drivers found ++;

            while(startGrids.Count > 0)
            {
                var section = startGrids.Pop();

                if(race.GetSectionData(section).Left != null)
                {
                    remainingDrivers--;
                }
                if(race.GetSectionData(section).Right != null)
                {
                    remainingDrivers--;
                }

            }

            Assert.IsTrue(remainingDrivers == 0);
        }

        //Test if not more drivers are added than starting positions
        [Test]
        public void Should_Not_Add_Driver_To_First_StartGrid()
        {
            _competition.Participants.Add(new Driver("Ricciardo", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            _competition.Participants.Add(new Driver("Vettel", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            _competition.Participants.Add(new Driver("Sainz", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
            _competition.Participants.Add(new Driver("Norris", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            var startGrids = new Stack<Section>();
            var track = _competition.Tracks.Dequeue();
            var remainingDrivers = _competition.Participants.Count;

            var race = new Race(track, _competition.Participants);

            race.AddParticipantsToTrack(track, _competition.Participants);

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            //loop through start grids if found drivers found ++;
            while (startGrids.Count > 0)
            {
                var section = startGrids.Pop();

                if (race.GetSectionData(section).Left != null)
                {
                    remainingDrivers--;
                }
                if (race.GetSectionData(section).Right != null)
                {
                    remainingDrivers--;
                }

            }

            Assert.IsTrue(remainingDrivers > 0);
        }
    }
}
