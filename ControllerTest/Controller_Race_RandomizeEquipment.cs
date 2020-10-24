using System;
using System.Collections.Generic;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
	public class Controller
	{
		[TestFixture]
		public class Controller_Race_RandomizeEquipment
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

            //Tests if quality and performance in participants is changed
            //[Test]
            //public void Should_Change_Performance_And_Quality_Of_Driver()
            //{
            //    var track = _competition.Tracks.Dequeue();

            //    var race = new Race(track, _competition.Participants);
            //    var copyParticipants = new List<IParticipant>(_competition.Participants);

            //    race.RandomizeEquipment();

                
            //    for(int i = 0; i < _competition.Participants.Count; i++)
            //    {
            //        Assert.IsTrue(_competition.Participants[i].Equipment.Quality != copyParticipants[i].Equipment.Quality && _competition.Participants[i].Equipment.Performance != copyParticipants[i].Equipment.Performance);
            //    }


            //}

            //Is value added to drivers
            //[Test]
            //public void Should_Add_Performance_And_Quality_Of_Driver()
            //{
            //    var track = _competition.Tracks.Dequeue();
            //    var section = track.Sections.First.Value;

            //    var race = new Race(track, _competition.Participants);
            //    race._random = new Random(69);

            //    race.RandomizeEquipment();

            //    foreach (IParticipant participant in race.Participants)
            //    {
            //        Assert.IsTrue(participant.Equipment.Quality == 69 && participant.Equipment.Performance == 69);
            //    }


            //}
        }
	}
}
