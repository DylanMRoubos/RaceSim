using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        //List of all the participants in the competition
        public List<IParticipant> Participants { get; set; }
        //Queueue of al the tracks within the competitiotn
        public Queue<Track> Tracks { get; set; }
        //Generic list with all the driverpoints from the competition
        public RaceDetails<DriverPoints> DriverPoints {get; set;}
        //Competition name
        public string Name { get; set; }

        //Set the Competition details with a name
        public Competition(List<IParticipant> Participants, Queue<Track> Tracks, string name)
        {
            Participants = new List<IParticipant>();
            this.Participants = Participants;
            this.Tracks = Tracks;
            Name = name;
            DriverPoints = new RaceDetails<DriverPoints>();
        }
        //Set the empty Competition details with a name
        public Competition(string name)
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            Name = name;
            DriverPoints = new RaceDetails<DriverPoints>();
        }
        //add points to the drivers based on dictionary with key as finish position
        public void AddPointsToDirvers(Dictionary<int, string> FinishPosition)
        {
            int points = 0;

            foreach (KeyValuePair<int, string> position in  FinishPosition)
            {
                switch (position.Key)
                {
                    case 1:
                        points = 25;
                        break;
                    case 2:
                        points = 20;
                        break;
                    case 3:
                        points = 18;
                        break;
                    case 4:
                        points = 15;
                        break;
                    case 5:
                        points = 10;
                        break;
                    case 6:
                        points = 5;
                        break;
                    default:
                        points = 0;
                        break;
                }
                DriverPoints.AddItemToList(new DriverPoints(position.Value, points));
            }
        }
        //Get the next track if there is one
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
