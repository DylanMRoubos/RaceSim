using System;
namespace Model
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }
        public Competition Comp { get; set; }

        public DriversChangedEventArgs(Track track, Competition comp)
        {
            Track = track;
            Comp = comp;
        }
    }
}
