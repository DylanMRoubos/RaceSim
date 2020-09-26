using System;
namespace Model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }

        public SectionData(IParticipant left, int DistanceLeft, IParticipant right, int DistanceRight)
        {
            this.Left = left;
            this.DistanceLeft = DistanceLeft;
            this.Right = right;
            this.DistanceRight = DistanceRight;
        }
        public SectionData()
        {
            this.DistanceLeft = 100;
            this.DistanceRight = 100;
        }
    }
}
