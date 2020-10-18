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
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.NextRace += NextRace;
        }

        public void Initialize()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
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

        public void NextRace(Object source, EventArgs e)
        {
            Initialize();
        }
    }
}
