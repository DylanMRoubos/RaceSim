using System;
using System.Collections.Generic;
using System.Timers;
using Model;

namespace Controller
{

    public class Race
    {
        //Delegates for the Events with custom EventArgs
        public delegate void onDriversChanged(object Sender, DriversChangedEventArgs dirversChangedEventArgs);
        public delegate void onNextRace(object Sender, RaceStartEventArgs nextRaceEventArgs);
        //Events for when a driver changes and a race ends
        public event onDriversChanged DriversChanged;
        public event onNextRace NextRace;

        //CurrentTrack
        public Track Track { get; set; }
        //Current Participants
        public List<IParticipant> Participants { get; set; }
        //public DateTime StartTime { get; set; }
        public Random _random { get; set; }
        //Timer to set the interval
        private Timer Timer;
        //Check how much drivers are removed from the race
        private int DriversRemoved = 0;
        //Constant to keep track of the amount of laps that need to be driven before finishing
        private const int AmountOfLaps = 1;
        //Refresh interval in MiliSeconds
        private const int IntervalMiliSeconds = 100;

        //Dictionaries to keep track of data within the race
        public Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Dictionary<IParticipant, int> DrivenRounds = new Dictionary<IParticipant, int>();
        public Dictionary<int, string> FinishPosition = new Dictionary<int, string>();

        //Generic list to keep track of section times from driver
        public RaceDetails<DriverSectionTimes> sectionTimes = new RaceDetails<DriverSectionTimes>();
        //Generic list to keep track of distance driven by driver
        public RaceDetails<DriverDistanceDriven> distanceDriven = new RaceDetails<DriverDistanceDriven>();
        //Generic list to keep track of the amount of times a driver breaks down
        public RaceDetails<DriverBrokenDownAmount> brokenDownAmount = new RaceDetails<DriverBrokenDownAmount>();

        //Setup the race details and start the race
        public Race(Track track, List<IParticipant> participants)
        {
            //Set the track & participants for the race
            Track = track;
            Participants = participants;
            //Create random to use later in the race
            _random = new Random(DateTime.Now.Millisecond);
            //Initialize Race
            Initialize();
            //Configure the timer
            SetupTimer();
            //Start the Race
            Start();
        }

        //Initialize the needed race components
        public void Initialize()
        {
            RandomizeEquipment();
            AddParticipantsToTrack(Track, Participants, DateTime.Now);
            FillDriverDictionary();
        }

        //Setup the timer with the give interval + add the event
        public void SetupTimer()
        {
            Timer = new Timer(IntervalMiliSeconds);
            Timer.Elapsed += OnTimedEvent;
        }

        //Loop through the track on a timed event and check if a driver can be moved to next section
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            LinkedListNode<Section> section = Track.Sections.Last;
            SectionData sectionValue = GetSectionData(section.Value);

            LinkedListNode<Section> previousSection = section;
            SectionData previousSectionValue = GetSectionData(previousSection.Value);

            //Loop through sections from the last to the first
            for (int i = 0; i < Track.Sections.Count; i++)
            {
                MoveDriverIfPossible(section, sectionValue, e);

                //Set the next section to the previous because we loop from the back to the front
                if (previousSection.Previous != null)
                {
                    previousSection = previousSection.Previous;
                    previousSectionValue = GetSectionData(previousSection.Value);
                    section = previousSection;
                    sectionValue = GetSectionData(previousSection.Value);
                }
                //Set the last section to the first because we loop from the back to the front
                else
                {
                    section = previousSection;
                    sectionValue = GetSectionData(previousSection.Value);
                    previousSection = Track.Sections.First;
                    previousSectionValue = GetSectionData(previousSection.Value);
                }
            }
            //Call the methods from the event driverschanged in the gui
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track, Data.Competition));

            //Stop the race if all drivers are removed
            if (DriversRemoved == Participants.Count) Stop();
        }

        //Remove all references on the DriversChanged Event
        public void CleanReferences()
        {
            DriversChanged = null;
        }

        //Fill the driver dictionary with all drivers an 0 rounds
        public void FillDriverDictionary()
        {
            foreach (IParticipant p in Participants)
            {
                DrivenRounds.Add(p, 0);
            }
        }

        //Add distance driven to a driver in the generic list
        public void AddDrivenDistance(string drivername, int distance)
        {
            distanceDriven.AddItemToList(new DriverDistanceDriven(drivername, distance));
        }

        //Add driver boken down amount  in the generic list
        public void AddDriverBrokenDown(string drivername)
        {
            brokenDownAmount.AddItemToList(new DriverBrokenDownAmount(drivername, 1));
        }

        //TODO: make more elegent this methods
        public bool DriverMovedToNextSection(LinkedListNode<Section> section, LinkedListNode<Section> nextSection, int LeftRight, DateTime CurrentTime)
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
            //Check if left or right driver crosses finish
            if (LeftRight == 0)
            {
                //Check if left driver is crossing finish
                if (section.Value.SectionType == SectionTypes.Finish)
                {
                    DrivenRounds[sectionValue.Left] += 1;

                    if (DrivenRounds[sectionValue.Left] == AmountOfLaps + 1)
                    {
                        sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Left.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));

                        FinishPosition.Add(FinishPosition.Count + 1, sectionValue.Left.Name);
                        sectionValue.Left = null;
                        sectionValue.DistanceLeft = 100;
                        DriversRemoved++;
                        return true;
                    }
                }
            }
            else
            {
                //Check if left driver is crossing finish
                if (section.Value.SectionType == SectionTypes.Finish)
                {
                    DrivenRounds[sectionValue.Right] += 1;

                    if (DrivenRounds[sectionValue.Right] == AmountOfLaps + 1)
                    {
                        sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Right.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));

                        FinishPosition.Add(FinishPosition.Count + 1, sectionValue.Right.Name);
                        AddDrivenDistance(sectionValue.Right.Name, 100);
                        sectionValue.Right = null;
                        sectionValue.DistanceRight = 100;
                        DriversRemoved++;

                        return true;
                    }
                }
            }


            //Move to left section
            if (nextSectionValue.Left == null)
            {
                //Move the left driver
                if (LeftRight == 0)
                {
                    sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Left.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));
                    AddDrivenDistance(sectionValue.Left.Name, 100);

                    nextSectionValue.Left = sectionValue.Left;
                    nextSectionValue.DistanceLeft += sectionValue.DistanceLeft;
                    nextSectionValue.startTimeLeft = CurrentTime;
                    //Reset values on current tile
                    sectionValue.Left = null;
                    sectionValue.DistanceLeft = 100;
                }
                // Move the right driver
                else
                {
                    sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Right.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));
                    AddDrivenDistance(sectionValue.Right.Name, 100);

                    nextSectionValue.Left = sectionValue.Right;
                    nextSectionValue.DistanceLeft += sectionValue.DistanceRight;
                    nextSectionValue.startTimeLeft = CurrentTime;
                    //Reset values on current tile
                    sectionValue.Right = null;
                    sectionValue.DistanceRight = 100;
                }
                return true;
            }
            //Move to right section
            else if (nextSectionValue.Right == null)
            {
                //Move the left driver
                if (LeftRight == 0)
                {
                    sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Left.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));
                    AddDrivenDistance(sectionValue.Left.Name, 100);

                    nextSectionValue.Right = sectionValue.Left;
                    nextSectionValue.DistanceRight += sectionValue.DistanceLeft;
                    nextSectionValue.startTimeRight = CurrentTime;
                    sectionValue.Left = null;
                    sectionValue.DistanceLeft = 100;
                }
                //Move the right driver
                else
                {
                    sectionTimes.AddItemToList(new DriverSectionTimes(sectionValue.Right.Name, CurrentTime - sectionValue.startTimeLeft, section.Value));
                    AddDrivenDistance(sectionValue.Right.Name, 100);

                    nextSectionValue.Right = sectionValue.Right;
                    nextSectionValue.DistanceRight += sectionValue.DistanceRight;
                    nextSectionValue.startTimeRight = CurrentTime;
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

        //Return if a driver is broken after determining if a driver is brkoen or not
        public bool BrokenToggler(IParticipant participant)
        {
            //if not broken
            if (!participant.Equipment.IsBroken)
            {
                //create chance to be broke
                if (_random.Next(1, 100) == 1)
                {
                    participant.Equipment.IsBroken = true;
                    AddDriverBrokenDown(participant.Name);
                    return true;
                }
                //Car stays healthy
                else return false;
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
                else return true;
            }
        }

        //Check if section has drivers on a position based on passed position
        private bool SectionEmpty(SectionPosition position, SectionData section)
        {
            switch (position)
            {
                //Check if driver on left section
                case SectionPosition.Left:
                    if (section.Left != null) return true;
                    break;
                //Check if driver on right section
                case SectionPosition.Right:
                    if (section.Right != null) return true;
                    break;
                //Check if driver on any of the two sections
                case SectionPosition.Both:
                    if (section.Left != null || section.Right != null) return true;
                    break;
            }
            return false;
        }

        //Move the DriverIfPossible
        public void MoveDriverIfPossible(LinkedListNode<Section> section, SectionData sectionValue, ElapsedEventArgs e)
        {
            //Check if one of both values is filled with a driver
            if (SectionEmpty(SectionPosition.Both, sectionValue))
            {
                //Check if Right position is filled with a driver
                if (SectionEmpty(SectionPosition.Right, sectionValue))
                {
                    //Check if driver is broken
                    if (!BrokenToggler(sectionValue.Right))
                    {
                        //Calculate distance drive in the interval
                        sectionValue.DistanceRight -= CalculateDistanceForCar(sectionValue.Right);
                    }
                    //If distance to next section is less van 0
                    if (sectionValue.DistanceRight < 0)
                    {
                        //If driver could not move set distance to 0
                        if (!DriverMovedToNextSection(section, section.Next, 1, e.SignalTime)) sectionValue.DistanceRight = 0;
                    }
                }

                //Check if left position is filled with a driver
                if (SectionEmpty(SectionPosition.Left, sectionValue))
                {
                    //Check if driver is broken
                    if (!BrokenToggler(sectionValue.Left))
                    {
                        //Calculate distance drive in the interval
                        sectionValue.DistanceLeft -= CalculateDistanceForCar(sectionValue.Left);
                    }
                    //If distance to next section is less van 0
                    if (sectionValue.DistanceLeft < 0)
                    {
                        //If driver could not move set distance to 0
                        if (!DriverMovedToNextSection(section, section.Next, 0, e.SignalTime)) sectionValue.DistanceLeft = 0;
                    }
                }

            }
        }

        //Calculate the distance the driver will drive this interval
        public int CalculateDistanceForCar(IParticipant driver)
        {
            return driver.Equipment.Performance * driver.Equipment.Quality * driver.Equipment.Speed;
        }

        //Start the timer
        public void Start()
        {
            Timer.Enabled = true;
        }

        //Stop the race
        public void Stop()
        {
            Timer.Enabled = false;
            //Remove all excisting references
            CleanReferences();
            //Set the Nextrace as currentrace in Data
            Data.NextRace();
            //Invoke next race with the new race as parameter
            NextRace.Invoke(this, new RaceStartEventArgs(Data.CurrentRace));
        }

        //Add the participants to the track
        public void AddParticipantsToTrack(Track track, List<IParticipant> participants, DateTime startTime)
        {
            //Create base variables needed to place the participants
            int currentDriver = 0;
            int remainingDrivers = participants.Count;

            if (remainingDrivers < 3) return;

            //Create a stack with all the startgrids
            Stack<Section> startGrids = GetStartGrids(track);
            //Place the participants
            PlaceParticipantsOnStartGrids(startGrids, remainingDrivers, currentDriver, participants, startTime);
        }

        //Place the participants on the starttrack
        public void PlaceParticipantsOnStartGrids(Stack<Section> startGrids, int remainingDrivers, int currentDriver, List<IParticipant> participants, DateTime startTime)
        {
            //Add the participants to the startgrids
            while (startGrids.Count > 0)
            {
                //Get the first available start grid
                var startSection = startGrids.Pop();
                var sectionData = GetSectionData(startSection);
                //Place on left side of startgrid
                if (remainingDrivers > 0)
                {
                    sectionData.Left = participants[currentDriver];
                    sectionData.startTimeLeft = startTime;
                    remainingDrivers--;
                    currentDriver++;
                }
                //Place on left side of startgrid
                if (remainingDrivers > 0)
                {
                    sectionData.Right = participants[currentDriver];
                    sectionData.startTimeRight = startTime;
                    remainingDrivers--;
                    currentDriver++;
                }
            }
        }

        //Get all the startgrids from the track in a Stack
        public Stack<Section> GetStartGrids(Track track)
        {
            var startGrids = new Stack<Section>();
            //Add all startgrids to a stack
            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }
            return startGrids;
        }

        //Get sectiondata matchin the key in dictionary
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

        //Randomize all the participants equipment
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
