using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Model_DriverSectionTImes
    {
        DriverSectionTimes points;
        List<DriverSectionTimes> list;

        [SetUp]
        public void SetUp()
        {
            points = new DriverSectionTimes("Max", new TimeSpan(1500), new Section(SectionTypes.Finish));
            list = new List<DriverSectionTimes>();
        }
        
        [Test]
        public void Model_DriverSectionTimes_Add_To_List()
        {

            points.Add(list);

            Assert.AreEqual(points.GetBest(list), "Max");
        }
        [Test]
        public void Model_DriverSectionTimes_Update_List()
        {
            DriverSectionTimes points2 = new DriverSectionTimes("Max", new TimeSpan(1400), new Section(SectionTypes.Finish));
            
            points.Add(list);
            points2.Add(list);

            Assert.AreEqual(points.GetBest(list), "Max");
        }
        [Test]
        public void Model_DriverPoints_Get_Name()
        {
            Assert.AreEqual(points.Name, "Max");
        }
        [Test]
        public void Model_DriverPoints_Get_Section()
        {
            Assert.AreEqual(points.Section.SectionType, SectionTypes.Finish);
        }
        [Test]
        public void Model_DriverPoints_Get_Time()
        {
            Assert.AreEqual(points.Time, new TimeSpan(1500));
        }

    }
}
