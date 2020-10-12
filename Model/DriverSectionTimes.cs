using System;
using System.Collections.Generic;
namespace Model
{
    public class DriverSectionTimes : IDriverName
    {
        public TimeSpan Time { get; set; }
        public Section Section { get; set; }
        public string Name { get; set; }

        public DriverSectionTimes()
        {
        }

        public DriverSectionTimes(string name, TimeSpan time, Section section)
        {
            Name = name;
            Time = time;
            Section = section;
        }

        public void Add<T>(List<T> list) where T : class, IDriverName
        {
            foreach (var driver in list)
            {
                var currentDriver = driver as DriverSectionTimes;
                if (currentDriver.Name == Name && currentDriver.Section == Section)
                {
                    currentDriver.Time = Time;
                    return;
                }
            }
            list.Add(this as T);
        }

        public string GetBest<T>(List<T> list) where T : class, IDriverName
        {
            TimeSpan FastestSector = this.Time;
            DriverSectionTimes DriverSectionTimes = this;

            foreach (var driver in list)
            {
                var currentDriver = driver as DriverSectionTimes;

                if (currentDriver.Time > Time)
                {
                    Time = currentDriver.Time;
                    DriverSectionTimes = currentDriver;
                }
            }

            return DriverSectionTimes.Name;
        }
    }
}
