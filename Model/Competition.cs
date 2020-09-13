using System;
using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition(List<IParticipant> Participants, Queue<Track> Tracks)
        {
            Participants = new List<IParticipant>();
            this.Participants = Participants;
            this.Tracks = Tracks;

        }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            if (Tracks.Count != 0)
            {
                return Tracks.Dequeue();
            }
            else
            {
                return null;
            }

        }
    }
}
