using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RaceGui
{

    class RaceDataContext : INotifyPropertyChanged
    {

        public Race CurrentRace;
        public RaceDetails<DriverSectionTimes> SectionTimes { get; set; }

        public RaceDetails<DriverDistanceDriven> DistanceDriven { get; set; }

        public RaceDetails<DriverBrokenDownAmount> BrokenDownAmount { get; set; }

        public List<DriverSectionTimes> SectionTimesList { get; set; }

        public List<DriverDistanceDriven> DistanceDrivenList { get; set; }

        public List<DriverBrokenDownAmount> BrokenDownAmountList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNextRaceEvent(Object sender, RaceStartEventArgs e)
        {
            CurrentRace = e.Race;

            SectionTimes = CurrentRace.sectionTimes;
            DistanceDriven = CurrentRace.distanceDriven;
            BrokenDownAmount = CurrentRace.brokenDownAmount;
            e.Race.DriversChanged += OnDriversChanged;
        }

        public void OnDriversChanged(Object sender, DriversChangedEventArgs e)
        {
            SectionTimesList = SectionTimes.GetList().ToList();
            DistanceDrivenList = DistanceDriven.GetList().ToList();
            BrokenDownAmountList = BrokenDownAmount.GetList().ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
