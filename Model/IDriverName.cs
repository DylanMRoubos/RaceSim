using System;
using System.Collections.Generic;

namespace Model
{
    public interface IDriverName
    {
        //Name of driver
        public string Name { get; set; }

        //Method to Add data to list
        public void Add<T>(List<T> list) where T : class, IDriverName;

        public string GetBest<T>(List<T> list) where T : class, IDriverName;
    }
}
