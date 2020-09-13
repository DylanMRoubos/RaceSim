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
        }
        public Race()
        {
        }

        public SectionData GetSectionData(Section section)
        {
            try {
                return _positions[section];
            }
            catch (ArgumentException)
            {
                _positions.Add(section, null);
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
