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

        public void Add<T>(List<T> list) where T : class, IDriverName
        {
            foreach (var driver in list)
            {
                var currentDriver = driver as DriverBrokenDownAmount;

                if (driver.Name == Name)
                {
                    currentDriver.BrokenDownAmount++;
                    return;
                }
            }
            list.Add(this as T);
        }

        public string GetBest<T>(List<T> list) where T : class, IDriverName
        {
            int MostBrokenDown = BrokenDownAmount;
            DriverBrokenDownAmount BrokenDownAmountObj = this;

            foreach (var driver in list)
            {
                var currentDriver = driver as DriverBrokenDownAmount;

                if (currentDriver.BrokenDownAmount > BrokenDownAmount)
                {
                    BrokenDownAmount = currentDriver.BrokenDownAmount;
                    BrokenDownAmountObj = currentDriver;
                }
            }

            return BrokenDownAmountObj.Name;
        }
    }
}
