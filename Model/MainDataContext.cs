using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class MainDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TrackName { get; set; }

        public void OnDriversChanged(Object sender, DriversChangedEventArgs e)
        {
            //DriverBrokenDown = e.CurrentRace.Driver;
            TrackName = e.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));            
        }
    }
}
