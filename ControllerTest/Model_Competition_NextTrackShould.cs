using System;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;


        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] sect1 = { SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Track track1 = new Track("Racetrack1", sect1);

            _competition.Tracks.Enqueue(track1);

            var result = _competition.NextTrack();

            Assert.AreEqual(track1, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] sect1 = { SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Track track1 = new Track("Racetrack1", sect1);

            _competition.Tracks.Enqueue(track1);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.IsNull(result);

        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] sect1 = { SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner };
            Track track1 = new Track("Racetrack1", sect1);
            Track track2 = new Track("Racetrack2", sect1);

            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.AreEqual(track2, result);

        }
    }
}
