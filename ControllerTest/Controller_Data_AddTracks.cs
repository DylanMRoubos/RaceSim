using System;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data_AddTracks
    {
        //Check if Tracks are added to the CurrentRace
        [Test]
        public void Should_Add_Tracks_To_CurrentRace()
        {
            Data.Competition = new Competition("W1");

            var trackCount = Data.Competition.Tracks.Count;

            Data.AddTracks();

            Assert.IsTrue(trackCount < Data.Competition.Tracks.Count);
        }
    }
}
