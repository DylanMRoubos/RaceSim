using System;
using System.Collections.Generic;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime Start { get; set; }
        public Random _random { get; set; }

        private Dictionary<Section, SectionData> _positions;

        public Race(Track Track, List<IParticipant> Participants)
        {
            this.Track = Track;
            this.Participants = Participants;

            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();

            AddParticipantsToTrack(Track, Participants);
        }
        public Race()
        {
        }

        public void AddParticipantsToTrack(Track track, List<IParticipant> participants)
        {
            int currentDriver = 0;
            int remainingDrivers = participants.Count;

            //bouw stack op met sections
            var startGrids = new Stack<Section>();

            foreach (var section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    startGrids.Push(section);
                }
            }

            while(startGrids.Count > 0)
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
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
            _random.Next();
        }

    }
}
