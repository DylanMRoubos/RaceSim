using System;
using System.Collections.Generic;
namespace Model
{
    public class DriverPoints : IDriverName
    {
        private int Points { get; set; }
        public string Name { get; set; }

        public DriverPoints()
        {
        }

        public DriverPoints(string name, int points)
        {
            Name = name;
            Points = points;

        }

        public void Add(List<IDriverName> list)
        {
            foreach (var driver in list)
            {
                var currentDriver = (DriverPoints)driver;
                if (driver.Name == Name)
                {
                    currentDriver.Points += Points;
                    return;
                }
            }
            list.Add(this);
        }
    }
}
