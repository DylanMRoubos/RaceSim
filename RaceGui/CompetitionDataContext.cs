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

    class CompetitionDataContext : INotifyPropertyChanged
    {
        public RaceDetails<DriverPoints> DriverPoints { get; set; }

        public string CompetitionName { get; set; }

        public string BestDriver { get; set; }

        public List<DriverPoints> DriverPointsList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNextRaceEvent(Object sender, RaceStartEventArgs e)
        {
            Data.CurrentRace.NextRace += OnNextRaceEvent;

            CompetitionName = Data.Competition.Name;
            DriverPoints = Data.Competition.DriverPoints;
            BestDriver = DriverPoints.GetHighest();

            DriverPointsList = DriverPoints.GetList().ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void OnDriversChanged(Object sender, DriversChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
