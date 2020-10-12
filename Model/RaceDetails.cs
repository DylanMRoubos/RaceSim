using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class RaceDetails<T> where T : class, IDriverName
    {
        //Generic list
        private List<T> _list = new List<T>();

        //Add item to the list using "Add method from implemted interface"
        public void AddItemToList(T item)
        {
            item.Add(_list);
        }
        
        //Get highest from the getbest method from the implementend interface
        public string GetHighest()
        {
            if (_list.Count > 0)
            {
                return _list[0].GetBest(_list);
            } else
            {
                return "";
            }
            
        }

        //Method to make list enumartable
        //public IEnumerator GetEnumerator()
        //{
        //    foreach (T val in _list)
        //    {
        //        yield return val;
        //    }
        //}
    }
}
