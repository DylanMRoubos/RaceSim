using System;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data_AddParticipants
    {

        //Check if participants are added to the CurrentRace
        [Test]
        public void Should_Add_Participants_To_CurrentRace()
        {
            Data.Competition = new Competition();

            var participantCount = Data.Competition.Participants.Count;

            Data.AddParticipants();

            Assert.IsTrue(participantCount < Data.Competition.Participants.Count);
        }

    }
}
