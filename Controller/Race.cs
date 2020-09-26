using System;
using System.Collections.Generic;
using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        public Random _random { get; set; }
        private Timer timer;

        public delegate void onDriversChanged(object Sender, DriversChangedEventArgs dirversChangedEventArgs);

        public event onDriversChanged DriversChanged;

        private Dictionary<Section, SectionData> _positions;

        public Race(Track Track, List<IParticipant> Participants)
        {
            this.Track = Track;
            this.Participants = Participants;

            _random = new Random(DateTime.Now.Millisecond);
            RandomizeEquipment();

            timer = new Timer(1500);
            timer.Elapsed += OnTimedEvent;
            Start();


            _positions = new Dictionary<Section, SectionData>();

            AddParticipantsToTrack(Track, Participants);
        }

        public Race()
        {
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var currentSection = Track.Sections.First;

            for (int i = 0; i < Track.Sections.Count; i++)
            {
                if (GetSectionData(currentSection.Value).Left != null)
                {

                    GetSectionData(currentSection.Value).DistanceLeft = GetSectionData(currentSection.Value).DistanceLeft - calculateDistanceForCar(GetSectionData(currentSection.Value).Left);

                    if (GetSectionData(currentSection.Value).DistanceLeft < 0 && GetSectionData(currentSection.Next.Value).Left == null)
                    {
                        GetSectionData(currentSection.Next.Value).Left = GetSectionData(currentSection.Value).Left;
                        GetSectionData(currentSection.Value).Left = null;

                    }

                    //Console.WriteLine("Driver: " + GetSectionData(section).Left.Name + "\n Performance: " + GetSectionData(section).Left.Equipment.Performance + "\n" + "Speed: " + GetSectionData(section).Left.Equipment.Speed + "\n" + "Quality: " + GetSectionData(section).Left.Equipment.Quality);
                }
                DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
                currentSection = currentSection.Next;
            }          
        }

        public int calculateDistanceForCar(IParticipant driver)
        {
            return driver.Equipment.Performance / driver.Equipment.Quality * driver.Equipment.Speed;
        }


        public void Start()
        {
            //timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void AddParticipantsToTrack(Track track, List<IParticipant> participants)
        {
            int currentDriver = 0;
            int remainingDrivers = participants.Count;

            if (remainingDrivers < 3) return;

            //bouw stack op met sections
            var startGrids = new Stack<Section>();

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            while (startGrids.Count > 0)
            {
                var startSection = startGrids.Pop();

                if (remainingDrivers > 0)
                {
                    GetSectionData(startSection).Left = participants[currentDriver];
                    remainingDrivers--;
                    currentDriver++;
                }
                if (remainingDrivers > 0)
                {
                    GetSectionData(startSection).Right = participants[currentDriver];
                    remainingDrivers--;
                    currentDriver++;
                }
            }
        }

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section))
            {
                return _positions[section];
            }
            else
            {
                _positions.Add(section, new SectionData());
                return _positions[section];
            }
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1, 10);
                participant.Equipment.Performance = _random.Next(1, 10);
            }
            _random.Next();
        }

    }
}
