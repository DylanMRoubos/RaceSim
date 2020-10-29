using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    class Model_SectionBuidlingDetails
    {
        SectionBuildingDetails sectionDetails;

        [SetUp]
        public void SetUp()
        {
            sectionDetails = new SectionBuildingDetails(new Section(SectionTypes.Finish), 1, 2, Direction.North);
        }

        [Test]
        public void Should_Get_X_SectionBuildingDetails()
        {
            Assert.AreEqual(sectionDetails.X, 1);
        }
        [Test]
        public void Should_Get_Y_SectionBuildingDetails()
        {
            Assert.AreEqual(sectionDetails.Y, 2);
            Assert.AreEqual(sectionDetails.Y, 2);
        }
        [Test]
        public void Should_Get_Direction_SectionBuildingDetails()
        {
            Assert.AreEqual(sectionDetails.Direction, Direction.North);
        }
        [Test]
        public void Should_Get_SectionType_SectionBuildingDetails()
        {
            Assert.AreEqual(sectionDetails.Section.SectionType, SectionTypes.Finish);
        }
    }
}
