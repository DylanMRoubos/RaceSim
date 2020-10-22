using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RaceGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window RaceDetails;
        private Window CompetitionDetails;

        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Initialize();
        }

        public void Initialize()
        {
            ImageCache.ClearCache();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            this.Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((MainDataContext)this.DataContext).OnDriversChanged;
            });
            
            Data.CurrentRace.NextRace += NextRace;
            //ImageCache.ClearCache();
        }

        public void OnDriversChanged(Object sender, DriversChangedEventArgs e)
        {
            TrackImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                TrackImage.Source = null;
                TrackImage.Source = GUITrackVisualisation.DrawTrack(Data.CurrentRace.Track);
            }));
        }

        public void NextRace(Object source, RaceStartEventArgs e)
        {
            Initialize();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Open_Race_Details(object sender, RoutedEventArgs e)
        {
            RaceDetails = new RaceStatisticsWindow();

            Data.CurrentRace.NextRace += ((RaceDataContext)RaceDetails.DataContext).OnNextRaceEvent;
            ((RaceDataContext)RaceDetails.DataContext).OnNextRaceEvent(null, new RaceStartEventArgs(Data.CurrentRace));

            RaceDetails.Show();
        }
        private void Open_Competition_Details(object sender, RoutedEventArgs e)
        {
            CompetitionDetails = new StatisticsWindow();
            Data.CurrentRace.NextRace += ((CompetitionDataContext)CompetitionDetails.DataContext).OnNextRaceEvent;

            ((CompetitionDataContext)CompetitionDetails.DataContext).OnNextRaceEvent(null, new RaceStartEventArgs(Data.CurrentRace));

            CompetitionDetails.Show();
        }
    }
}
