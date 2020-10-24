using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;
using NUnit.Framework;


namespace ControllerTest
{
    [TestFixture]
    public class Model_DriverPoints
    {
        public RaceDetails<DriverPoints> DriverPoints { get; set; }

        [SetUp]
        public void SetUp()
        {
           
        }

        //Check if driverpoints are added
        [TestCase(2, "Max")]
        public void Model_DriverPoints_Create_DriverPoints(int PointAmount, string Driver)
        {
            var points = new DriverPoints(Driver, PointAmount);

            Assert.AreEqual(points.Name, Driver);
        }
        //Check if driverpoints are added
        [TestCase(2, "Max")]
        public void Model_DriverPoints_Add_To_List_DriverPoints(int PointAmount, string Driver)
        {
            var points = new DriverPoints(Driver, PointAmount);
            List<DriverPoints> list = new List<DriverPoints>();

            points.Add(list);

            Assert.AreEqual(Driver, points.GetBest(list));
        }

    }
}
