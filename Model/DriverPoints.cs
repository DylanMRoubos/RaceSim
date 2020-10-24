using System;
using System.Collections.Generic;
namespace Model
{
    public class DriverPoints : IDriverName
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public DriverPoints(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public void Add<T>(List<T> list) where  T : class, IDriverName
        {

            foreach (var driver in list)
            {
                var currentDriver = driver as DriverPoints;

                if (driver.Name == Name)
                {
                    currentDriver.Points += Points;
                    return;
                }
            }
            list.Add(this as T);
        }

        public string GetBest<T>(List<T> list) where T : class, IDriverName
        {
            int highestPoints = 0;
            DriverPoints DriverPoints = this;

            foreach (var driver in list)
            {
                var currentDriver = driver as DriverPoints;

                if (currentDriver.Points > highestPoints)
                {
                    highestPoints = currentDriver.Points;
                    DriverPoints = currentDriver;
                }
            }

            return DriverPoints.Name;
        }
    }
}
