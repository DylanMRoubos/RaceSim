using System;
using System.Collections.Generic;

namespace Model
{
    public class DriverBrokenDownAmount : IDriverName
    {
        public int BrokenDownAmount { get; set; }
        public string Name { get; set; }

        public DriverBrokenDownAmount()
        {
        }

        public DriverBrokenDownAmount(string name, int brokenDownAmount)
        {
            Name = name;
            BrokenDownAmount = brokenDownAmount;
        }

        public void Add(List<IDriverName> list)
        {
            foreach(var driver in list)
            {
                var currentDriver = (DriverBrokenDownAmount) driver;
                if(driver.Name == Name)
                {
                    currentDriver.BrokenDownAmount++;
                    return;
                }
            }            
            list.Add(this);            
        }
    }
}
