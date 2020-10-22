using Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceStartEventArgs : EventArgs
    {
        public Race Race { get; set; }

        public RaceStartEventArgs(Race race)
        {
            Race = race;
        }
    }
}
