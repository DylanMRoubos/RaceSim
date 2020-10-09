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

        public void Add(List<IDriverName> list)
        {
            foreach (var driver in list)
            {
                var currentDriver = (DriverSectionTimes)driver;
                if (currentDriver.Name == Name && currentDriver.Section == Section)
                {
                    currentDriver.Time = Time;
                    return;
                }
            }
            list.Add(this);
        }
    }
}
