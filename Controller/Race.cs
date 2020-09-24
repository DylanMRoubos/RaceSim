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
        //TODO: Optimise method
        public void AddParticipantsToTrack(Track track, List<IParticipant> participants)
        {
            int i = 0;
            int startGridAmount = 0; ;
            int remainingDrivers = participants.Count;
            var section = track.Sections.First;

            for (int j = 0; j < track.Sections.Count; j++)
            {
                if (section.Value.SectionType == SectionTypes.StartGrid && section.Next.Value.SectionType != SectionTypes.StartGrid)
                {
                    for (int k = 0; k <= startGridAmount; k++)
                    {

                        if (remainingDrivers > 0)
                        {
                            GetSectionData(section.Value).Left = participants[i];
                            i++;
                            remainingDrivers--;
                        }
                        if (remainingDrivers > 0)
                        {
                            GetSectionData(section.Value).Right = participants[i];
                            i++;
                            remainingDrivers--;
                        }
                        if (remainingDrivers == 0)
                        {
                            break;
                        }
                        else if (section.Previous.Value.SectionType == SectionTypes.StartGrid)
                        {
                            section = section.Previous;
                        }

                    }
                }

                if (section.Value.SectionType == SectionTypes.StartGrid)
                {
                    startGridAmount++;
                }
                section = section.Next;
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

        public object GetSectionData(SectionTypes sectionType)
        {
            throw new NotImplementedException();
        }
    }
}
