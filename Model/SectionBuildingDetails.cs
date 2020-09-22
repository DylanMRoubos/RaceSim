using System;
namespace Model
{
    public class SectionBuildingDetails
    {
        public SectionTypes SectionType { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction;

        public SectionBuildingDetails()
        {
        }

        public SectionBuildingDetails(SectionTypes sectionType, int x, int y, Direction direction)
        {
            SectionType = sectionType;
            X = x;
            Y = y;
            Direction = direction;
        }

        public virtual string ToString()
        {
            return $"type: {SectionType} x: {X} y: {Y} Direction: {Direction}";
        }
    }
}
