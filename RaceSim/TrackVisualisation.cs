using System;
using System.Collections.Generic;
using Model;

namespace RaceSim
{
    public static class TrackVisualisation
    {

        public static string[,,] completeTrack;
        public static List<SectionBuildingDetails> SectionGridDetails = new List<SectionBuildingDetails>();

        public static void Initialize()
        {

        }

        #region graphics
        private static string[] _startHorizontal = { "----", "  > ", " >  ", "----" };
        private static string[] _startVertical = { "|  |", "|^ |", "| ^|", "|  |" };

        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _finishVertical = { "|  |", "| ## |", "|  |", "|  |" };

        private static string[] _trackHorizontal = { "----", "    ", "    ", "----" };
        private static string[] _trackVertical = { "|  |", "|  |", "|  |", "|  |" };

        private static string[] _cornerRightHorinzontal = { "--\\ ", "   \\", "   |", "\\  |" };
        private static string[] _cornerRightVertical = { "/  |", "   |", "   /", "--/ " };


        private static string[] _cornerLeftHorizontal = { " /--", "/   ", "|   ", "|  /" };
        private static string[] _cornerLefVertical = { "|  \\", "|   ", "\\   ", " \\--" };


        #endregion

        public static void DrawTrack(Track track)
        {

            fillSectionBuildingDetailsArray(SectionGridDetails, track);
            GetLowestXAndYFromList(SectionGridDetails);
            completeTrack = new string[GetHighestYValue(), GetHighestXValue(), 4];

            BuildTrackArray(completeTrack, SectionGridDetails);
            DrawTrackComponentWithCompleteTrackArray(completeTrack);


        }
        //BIGGEST FLIPPIN METHOD EVER!!! Used to fill the trackArray
        public static void BuildTrackArray(string[,,] completeTrack, List<SectionBuildingDetails> sectionBuildingDetails)
        {
            foreach (var Section in sectionBuildingDetails)
            {
                //Start track horizontal & vertical 
                if (Section.SectionType == SectionTypes.StartGrid)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _startHorizontal[i];
                        }
                    }
                    //Vertical
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _startVertical[i];
                        }
                    }
                }
                //Straight track horizontal & vertical 
                else if (Section.SectionType == SectionTypes.Straight)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _trackHorizontal[i];
                        }
                    }
                    //Vertical
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _trackVertical[i];
                        }
                    }
                }
                //Finish track horizontal & vertical 
                else if (Section.SectionType == SectionTypes.Finish)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _finishHorizontal[i];
                        }
                    }
                    //Vertical
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _finishVertical[i];
                        }
                    }
                }
                //Corner left
                else if (Section.SectionType == SectionTypes.LeftCorner)
                {
                    //Left -> North
                    if (Section.Direction == Direction.North)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerRightVertical[i];
                        }
                    }
                    //Left -> East
                    else if (Section.Direction == Direction.East)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerLefVertical[i];
                        }
                    }
                    //Left South
                    else if (Section.Direction == Direction.South)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerLeftHorizontal[i];
                        }
                    }
                    else if (Section.Direction == Direction.West)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerRightHorinzontal[i];
                        }
                    }
                }
                //Corner right
                else if (Section.SectionType == SectionTypes.RightCorner)
                {
                    //Right -> North
                    if (Section.Direction == Direction.North)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerLefVertical[i];
                        }
                    }
                    //Right -> East
                    else if (Section.Direction == Direction.East)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerLeftHorizontal[i];
                        }
                    }
                    //Right South
                    else if (Section.Direction == Direction.South)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerRightHorinzontal[i];
                        }
                    }
                    //Right West
                    else if (Section.Direction == Direction.West)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = _cornerRightVertical[i];
                        }
                    }

                }


            }

        }

        public static void fillSectionBuildingDetailsArray(List<SectionBuildingDetails> details, Track track)
        {
            Direction lastDirection = Direction.East;
            Direction direction = Direction.East;

            int x = 0;
            int y = 0;

            foreach (var Section in track.Sections)
            {

                //Determine direction when section is a corner
                if (GetSectionType(Section) == SectionTypes.LeftCorner)
                {

                    if (lastDirection == Direction.North)
                    {
                        direction = lastDirection + 3;
                    }
                    else
                    {
                        direction = lastDirection - 1;
                    }
                }
                //Determine direction when section is a corner
                if (GetSectionType(Section) == SectionTypes.RightCorner)
                {
                    if (lastDirection == Direction.West)
                    {
                        direction = lastDirection - 3;
                    }
                    else
                    {
                        direction = lastDirection + 1;
                    }
                }

                details.Add(new SectionBuildingDetails(Section.SectionType, x, y, direction));

                switch (direction)
                {
                    case Direction.North:
                        y--;
                        break;
                    case Direction.South:
                        y++;
                        break;
                    case Direction.East:
                        x++;
                        break;
                    case Direction.West:
                        x--;
                        break;

                }

                lastDirection = direction;

            }
        }

        public static void GetLowestXAndYFromList(List<SectionBuildingDetails> details)
        {
            int lowestX = 0;
            int lowestY = 0;

            foreach (var detail in details)
            {
                if (detail.X < lowestX)
                {
                    lowestX = detail.X;
                }
                if (detail.Y < lowestY)
                {
                    lowestY = detail.Y;
                }
            }

            UpdateListWithLowestXAndY(details, lowestX, lowestY);
        }

        public static void UpdateListWithLowestXAndY(List<SectionBuildingDetails> details, int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);


            foreach (var detail in details)
            {
                detail.X += x;
                detail.Y += y;
            }
        }

        public static SectionTypes GetSectionType(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    return SectionTypes.StartGrid;
                case SectionTypes.Straight:
                    return SectionTypes.Straight;
                case SectionTypes.RightCorner:
                    return SectionTypes.RightCorner;
                case SectionTypes.LeftCorner:
                    return SectionTypes.LeftCorner;
                case SectionTypes.Finish:
                    return SectionTypes.Finish;
            }
            return SectionTypes.Straight;
        }


        public static int GetHighestXValue()
        {
            int highestX = 0;

            foreach (var sectionBuilding in SectionGridDetails)
            {
                if (sectionBuilding.X > highestX)
                {
                    highestX = sectionBuilding.X;
                }
            }
            return highestX + 1;
        }

        public static int GetHighestYValue()
        {
            int highestY = 0;

            foreach (var sectionBuilding in SectionGridDetails)
            {
                if (sectionBuilding.Y > highestY)
                {
                    highestY = sectionBuilding.Y;
                }
            }
            return highestY + 1;
        }

        public static void DrawTrackComponentWithCompleteTrackArray(String[,,] completeTrack)
        {

            for (int y = 0; y < GetHighestYValue(); y++)
            {

                for (int k = 0; k < 4; k++)
                {

                    for (int x = 0; x < GetHighestXValue(); x++)
                    {
                        if (completeTrack[y, x, k] == null)
                        {
                            Console.Write("    ");
                        }
                        else
                        {
                            Console.Write(completeTrack[y, x, k]);
                        }
                    }
                    Console.WriteLine();
                }

            }
        }
    }
}




//TESTTRACK
//fillThreeDArray(0, 0, _cornerLeftHorizontal);
//fillThreeDArray(0, 1, _startHorizontal);
//fillThreeDArray(0, 2, _startHorizontal);
//fillThreeDArray(0, 3, _startHorizontal);
//fillThreeDArray(0, 4, _finishHorizontal);
//fillThreeDArray(0, 5, _cornerRightHorinzontal);
//fillThreeDArray(1, 0, _trackVertical);
//fillThreeDArray(1, 5, _trackVertical);
//fillThreeDArray(2, 0, _trackVertical);
//fillThreeDArray(2, 5, _cornerLefVertical);
//fillThreeDArray(2, 6, _trackHorizontal);
//fillThreeDArray(2, 7, _trackHorizontal);
//fillThreeDArray(2, 8, _cornerRightHorinzontal);
//fillThreeDArray(3, 0, _trackVertical);
//fillThreeDArray(3, 5, _cornerLeftHorizontal);
//fillThreeDArray(3, 6, _trackHorizontal);
//fillThreeDArray(3, 7, _trackHorizontal);
//fillThreeDArray(3, 8, _cornerRightVertical);
//fillThreeDArray(4, 0, _cornerLefVertical);
//fillThreeDArray(4, 1, _trackHorizontal);
//fillThreeDArray(4, 2, _cornerRightHorinzontal);
//fillThreeDArray(4, 3, _cornerLeftHorizontal);
//fillThreeDArray(4, 4, _trackHorizontal);
//fillThreeDArray(4, 5, _cornerRightVertical);
//fillThreeDArray(5, 2, _cornerLefVertical);
//fillThreeDArray(5, 3, _cornerRightVertical);