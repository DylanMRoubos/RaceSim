using System;
namespace Model
{
    //Array to keep track of the sectiondata to build the track in visualisation
    public class SectionBuildingDetails
    {
        public Section Section { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction;

        public SectionBuildingDetails(Section section, int x, int y, Direction direction)
        {
            Section = section;
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}
