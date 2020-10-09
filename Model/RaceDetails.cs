using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class RaceDetails<T> where T : IDriverName
    {
        private List<IDriverName> _list = new List<IDriverName>();

        public RaceDetails()
        {
        }

        public void addItemToList(T item)
        {
            item.Add(_list);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (T val in _list)
            {
                yield return val;
            }
        }
    }
}
