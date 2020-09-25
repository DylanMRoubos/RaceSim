using System;
using System.Collections.Generic;
using Controller;
using Model;


//TODO: remove list from parameters - add comments
namespace RaceSim
{
    public static class TrackVisualisation
    {

        //3D array with x, y coordinates and array with the drawn trackcomponent
        public static string[,,] CompleteTrack;
        //List with the buildingdetails for the completetrack
        public static List<SectionBuildingDetails> SectionBuildingGridDetails = new List<SectionBuildingDetails>();
        //Current Track
        public static Track Track;

        public static void Initialize(Track track)
        {
            Track = track;
        }

        #region graphics
        private static string[] _startHorizontal = { "----", " 1> ", "2>  ", "----" };
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

        public static void DrawTrack()
        {
            FillSectionBuildingGridDetailsArray(SectionBuildingGridDetails, Track);
            UpdateListWithLowestXAndY(SectionBuildingGridDetails, GetLowestXValue(SectionBuildingGridDetails), GetLowestYValue(SectionBuildingGridDetails));


            CompleteTrack = new string[GetHighestYValue(SectionBuildingGridDetails), GetHighestXValue(SectionBuildingGridDetails), 4];

            BuildTrackArray(CompleteTrack, SectionBuildingGridDetails);
            DrawTrackComponentWithCompleteTrackArray(CompleteTrack, SectionBuildingGridDetails);


        }

        public static void FillSectionBuildingGridDetailsArray(List<SectionBuildingDetails> sectionBuildingDetaisl, Track track)
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

                sectionBuildingDetaisl.Add(new SectionBuildingDetails(Section, x, y, direction));

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

        //BIGGEST FLIPPIN METHOD EVER!!! Used to fill the trackArray
        public static void BuildTrackArray(string[,,] completeTrack, List<SectionBuildingDetails> sectionBuildingDetails)
        {
            foreach (var Section in sectionBuildingDetails)
            {
                //Start track horizontal & vertical 
                if (Section.Section.SectionType == SectionTypes.StartGrid)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {

                        //Check if driver is on section
                        //update _startHorizontal with the update method

                        for (int i = 0; i < 4; i++)
                        {
                            completeTrack[Section.Y, Section.X, i] = PLaceParticipantsOnTrack(_startHorizontal[i], Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);
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
                else if (Section.Section.SectionType == SectionTypes.Straight)
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
                else if (Section.Section.SectionType == SectionTypes.Finish)
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
                else if (Section.Section.SectionType == SectionTypes.LeftCorner)
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
                else if (Section.Section.SectionType == SectionTypes.RightCorner)
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

        public static int GetLowestXValue(List<SectionBuildingDetails> details)
        {
            int lowestX = 0;

            foreach (var detail in details)
            {
                if (detail.X < lowestX)
                {
                    lowestX = detail.X;
                }
            }
            return lowestX;
        }

        public static int GetLowestYValue(List<SectionBuildingDetails> details)
        {
            int lowestY = 0;

            foreach (var detail in details)
            {
                if (detail.Y < lowestY)
                {
                    lowestY = detail.Y;
                }
            }
            return lowestY;
        }

        public static int GetHighestXValue(List<SectionBuildingDetails> details)
        {
            int highestX = 0;

            foreach (var sectionBuilding in details)
            {
                if (sectionBuilding.X > highestX)
                {
                    highestX = sectionBuilding.X;
                }
            }
            return highestX + 1;
        }

        public static int GetHighestYValue(List<SectionBuildingDetails> details)
        {
            int highestY = 0;

            foreach (var sectionBuilding in details)
            {
                if (sectionBuilding.Y > highestY)
                {
                    highestY = sectionBuilding.Y;
                }
            }
            return highestY + 1;
        }

        public static void DrawTrackComponentWithCompleteTrackArray(String[,,] completeTrack, List<SectionBuildingDetails> detail)
        {

            for (int y = 0; y < GetHighestYValue(detail); y++)
            {

                for (int k = 0; k < 4; k++)
                {

                    for (int x = 0; x < GetHighestXValue(detail); x++)
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

        public static string PLaceParticipantsOnTrack(string sectionRow, IParticipant participant1, IParticipant participant2)
        {

            var returnvalue = sectionRow;

            if (participant1 != null)
            {
                returnvalue = returnvalue.Replace("1", $"{participant1.Name.Substring(0, 1)}");
            }
            else
            {
                returnvalue = returnvalue.Replace("1", " ");
            }

            if (participant2 != null)
            {
                returnvalue = returnvalue.Replace("2", $"{participant2.Name.Substring(0, 1)}");
            }
            else
            {
                returnvalue = returnvalue.Replace("2", " ");
            }


            return returnvalue;
        }
    }
}