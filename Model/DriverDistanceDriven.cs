using System;
using System.Collections.Generic;

namespace Model
{
    public class DriverDistanceDriven : IDriverName
    {
        public int Distance { get; set; }
        public string Name { get; set; }

        public DriverDistanceDriven()
        {
        }

        public DriverDistanceDriven(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }

        public void Add<T>(List<T> list) where T : class, IDriverName
        {
            foreach (var driver in list)
            {
                var currentDriver = driver as DriverDistanceDriven;
                if (currentDriver.Name == Name)
                {
                    currentDriver.Distance += Distance;
                    return;
                }
            }
            list.Add(this as T);
        }


        public string GetBest<T>(List<T> list) where T : class, IDriverName
        {
            int DistanceDriven = Distance;
            DriverDistanceDriven DistanceDrivenObj = this;

            foreach (var driver in list)
            {
                var currentDriver = driver as DriverDistanceDriven;

                if (currentDriver.Distance > Distance)
                {
                    Distance = currentDriver.Distance;
                    DistanceDrivenObj = currentDriver;
                }
            }

            return DistanceDrivenObj.Name;
        }
    }
}
