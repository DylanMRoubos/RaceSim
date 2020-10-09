using System;
using System.Collections.Generic;

namespace Model
{
    public interface IDriverName
    {
        public string Name { get; set; }

        public void Add(List<IDriverName> list);
    }
}
