using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Data_Initialize
    {

        //Check if competition is initialized
        [Test]
        public void Should_Initialize_Competition()
        {
            if(Data.Competition == null)
            {
                Data.Initialize();
                Assert.IsTrue(Data.Competition != null);
            }
        }
    }
}
