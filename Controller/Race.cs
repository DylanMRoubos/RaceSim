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
        private int amountOfLaps = 2;
        private int driversRemoved;

        public delegate void onDriversChanged(object Sender, DriversChangedEventArgs dirversChangedEventArgs);
        public delegate void onNextRace(object Sender, EventArgs nextRaceEventArgs);

        public event onDriversChanged DriversChanged;
        public event onNextRace NextRace;

        private Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, int> DrivenRounds = new Dictionary<IParticipant, int>();

        public Race(Track Track, List<IParticipant> Participants)
        {
            this.Track = Track;
            this.Participants = Participants;
            driversRemoved = 0;

            _random = new Random(DateTime.Now.Millisecond);
            RandomizeEquipment();

            _positions = new Dictionary<Section, SectionData>();


            AddParticipantsToTrack(Track, Participants);
            fillDriverDictionaray();

            timer = new Timer(200);
            timer.Elapsed += OnTimedEvent;
            Start();
        }

        public Race()
        {
        }

        

        public void CheckIfDriverCrossedFinish(Section section)
        {
            if (section.SectionType == SectionTypes.Finish)
            {
                SectionData currentSectionData = GetSectionData(section);

                if (currentSectionData.Left != null)
                {
                    DrivenRounds[currentSectionData.Left] += 1;

                    if (DrivenRounds[currentSectionData.Left] == amountOfLaps + 1)
                    {
                        currentSectionData.Left = null;
                        currentSectionData.DistanceLeft = 100;
                        driversRemoved++;
                    }
                }
                if (currentSectionData.Right != null)
                {
                    DrivenRounds[currentSectionData.Right] += 1;
                    if (DrivenRounds[currentSectionData.Right] == amountOfLaps + 1)
                    {
                        currentSectionData.Right = null;
                        currentSectionData.DistanceRight = 100;
                        driversRemoved++;
                    }
                }
            }
        }

        public void cleanReferences()
        {
            DriversChanged = null;
        }
        public void fillDriverDictionaray()
        {
            foreach (IParticipant p in Participants)
            {
                DrivenRounds.Add(p, 0);
            }
        }
        public bool DriverMovedToNextSection(LinkedListNode<Section> section, LinkedListNode<Section> nextSection, int LeftRight)
        {

            SectionData sectionValue = GetSectionData(section.Value);
            SectionData nextSectionValue;

            //Check if last section
            if (Track.Sections.Last == section)
            {
                nextSectionValue = GetSectionData(Track.Sections.First.Value);
            }
            else
            {
                nextSectionValue = GetSectionData(nextSection.Value);
            }


            if (nextSectionValue.Left == null)
            {
                if (LeftRight == 0)
                {
                    nextSectionValue.Left = sectionValue.Left;
                    nextSectionValue.DistanceLeft += sectionValue.DistanceLeft;
                    //Reset values on current tile
                    sectionValue.Left = null;
                    sectionValue.DistanceLeft = 100;
                }
                else
                {
                    nextSectionValue.Left = sectionValue.Right;
                    nextSectionValue.DistanceLeft += sectionValue.DistanceRight;
                    //Reset values on current tile
                    sectionValue.Right = null;
                    sectionValue.DistanceRight = 100;
                }
                return true;
            }
            else if (nextSectionValue.Right == null)
            {
                if (LeftRight == 0)
                {
                    nextSectionValue.Right = sectionValue.Left;
                    nextSectionValue.DistanceRight += sectionValue.DistanceLeft;
                    sectionValue.Left = null;
                    sectionValue.DistanceLeft = 100;
                }
                else
                {
                    nextSectionValue.Right = sectionValue.Right;
                    nextSectionValue.DistanceRight += sectionValue.DistanceRight;
                    //Reset values on current tile
                    sectionValue.Right = null;
                    sectionValue.DistanceRight = 100;
                }


                return true;
            }

            else
            {
                return false;
            }
        }

        private bool brokenToggler(IParticipant participant)
        {
            //if not broken
            if(!participant.Equipment.IsBroken)
            {
                //create chance to be broke
                if(_random.Next(1, 100) == 1)
                {
                    participant.Equipment.IsBroken = true;
                    return true;
                }
                //Car stays healthy
                else
                {
                    return false;

                }
            }
            //Create change to be repaired
            else
            {
                //Create chance to be repaired
                if (_random.Next(1, 10) == 1)
                {
                    participant.Equipment.IsBroken = false;
                    //Quality will be lowered if possible
                    if (participant.Equipment.Quality > 1) participant.Equipment.Quality -= 1;
                    return false;
                }
                //Car will still be broken
                else
                {
                    return true;

                }
            }
        }

        //Make more suffisticated
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Loop through sections
            LinkedListNode<Section> section = Track.Sections.Last;
            SectionData sectionValue = GetSectionData(section.Value);

            LinkedListNode<Section> previousSection = section;
            SectionData previousSectionValue = GetSectionData(previousSection.Value);

            for (int i = 0; i < Track.Sections.Count; i++)
            {
                if (sectionValue.Left != null || sectionValue.Right != null)
                {

                    if (sectionValue.Left != null)
                    {

                        if(!brokenToggler(sectionValue.Left))
                        {
                            sectionValue.DistanceLeft -= calculateDistanceForCar(sectionValue.Left);
                        }                       
                        if (sectionValue.DistanceLeft < 0)
                        {
                            if (!DriverMovedToNextSection(section, section.Next, 0))
                            {
                                sectionValue.DistanceLeft = 0;
                            }
                        }


                    }
                    if (sectionValue.Right != null)
                    {
                        if (!brokenToggler(sectionValue.Right))
                        {
                            sectionValue.DistanceRight -= calculateDistanceForCar(sectionValue.Right);
                        }                      
                        if (sectionValue.DistanceRight < 0)
                        {
                            if (!DriverMovedToNextSection(section, section.Next, 1))
                            {
                                sectionValue.DistanceRight = 0;
                            }
                        }
                    }
                }

                if (previousSection.Previous != null)
                {
                    previousSection = previousSection.Previous;
                    previousSectionValue = GetSectionData(previousSection.Value);
                    section = previousSection;
                    sectionValue = GetSectionData(previousSection.Value);
                }
                else
                {
                    section = previousSection;
                    sectionValue = GetSectionData(previousSection.Value);

                    previousSection = Track.Sections.First;
                    previousSectionValue = GetSectionData(previousSection.Value);

                }

                //nextSection = nextSection.Next;
                //Console.Clear();
                CheckIfDriverCrossedFinish(section.Value);
            }

            DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));

            if (driversRemoved == Participants.Count)
            {
                Stop();
            }

        }

        public int calculateDistanceForCar(IParticipant driver)
        {
            return driver.Equipment.Performance * driver.Equipment.Quality * driver.Equipment.Speed;
        }


        public void Start()
        {
            //timer.AutoReset = true;
            timer.Enabled = true;
        }
        public void Stop()
        {
            //timer.AutoReset = true;
            timer.Enabled = false;
            cleanReferences();
            Console.WriteLine("Stopped");

            Data.NextRace();
            NextRace.Invoke(this, new EventArgs());
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
                participant.Equipment.Performance = _random.Next(1, 5);
            }
            _random.Next();
        }

    }
}
