using System;
using System.Collections.Generic;

namespace Model
{
    public class DriverDistanceDriven : IDriverName
    {
        private int Distance { get; set; }
        public string Name { get; set; }

        public DriverDistanceDriven()
        {
        }

        public DriverDistanceDriven(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }

        public void Add(List<IDriverName> list)
        {
            foreach (var driver in list)
            {
                var currentDriver = (DriverDistanceDriven)driver;
                if (currentDriver.Name == Name)
                {
                    currentDriver.Distance += Distance;
                    return;
                }
            }
            list.Add(this);
        }

    }
}
