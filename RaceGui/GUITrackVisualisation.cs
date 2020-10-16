using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;

namespace RaceGui
{
    static class GUITrackVisualisation
    {
        //3D array with x, y coordinates and array with the drawn trackcomponent
        public static Bitmap[,] CompleteTrack;
        //List with the buildingdetails for the completetrack
        public static List<SectionBuildingDetails> SectionBuildingGridDetails;
        //Current Track
        public static Track Track;

        public static Bitmap canvas;

        #region graphics
        const string CornerLeftHorizontal = ".\\Assets\\CornerLeftHorizontal.png";
        const string CornerLeftVertical = ".\\Assets\\CornerLeftVertical.png";
        const string CornerRightHorizontal = ".\\Assets\\CornerRightHorizontal.png";
        const string CornerRightVertical = ".\\Assets\\CornerRightVertical.png";
        const string Finish = ".\\Assets\\Finish.png";
        const string TrackHorizontal = ".\\Assets\\TrackHorizontal.png";
        const string TrackVertical = ".\\Assets\\TrackVertical.png";
        const string GrassTile = ".\\Assets\\Grass_Tile.png";
        const string WaterTile = ".\\Assets\\Water.png";

        const string Blue = ".\\Assets\\Blue.png";
        const string Grey = ".\\Assets\\Grey.png";
        const string Red = ".\\Assets\\Red.png";
        const string Yellow = ".\\Assets\\Yellow.png";
        const string Green = ".\\Assets\\Green.png";
        #endregion
          
        public static BitmapSource DrawTrack()
        {
            return ImageCache.CreateBitmapSourceFromGdiBitmap(canvas);
        }

        public static void Initialize(Track track)
        {
            CreateArrayWithTrack(track);

            canvas = ImageCache.CreateBitmap(GetHighestXValue(SectionBuildingGridDetails) * 691, GetHighestYValue(SectionBuildingGridDetails) * 691);

            CompleteTrack = new Bitmap[GetHighestYValue(SectionBuildingGridDetails), GetHighestXValue(SectionBuildingGridDetails)];

            BuildTrackArray(CompleteTrack, SectionBuildingGridDetails);
            AddGrass(CompleteTrack);

            PlaceTrack(canvas);
        }

        public static void CreateArrayWithTrack(Track track)
        {
            Track = track;
            SectionBuildingGridDetails = new List<SectionBuildingDetails>();

            FillSectionBuildingGridDetailsArray(SectionBuildingGridDetails, Track);
            UpdateListWithLowestXAndY(SectionBuildingGridDetails, GetLowestXValue(SectionBuildingGridDetails), GetLowestYValue(SectionBuildingGridDetails));
        }

        public static void PlaceTrack(Bitmap canvas)
        {
            int x = 0;
            int y = 0;

            Graphics g = Graphics.FromImage(canvas);

            for (int i = 0; i < CompleteTrack.GetLength(0); i++)
            {
                for (int j = 0; j < CompleteTrack.GetLength(1); j++)
                {
                    g.DrawImage(CompleteTrack[i, j], x, y, 691, 691);
                    x += 691;
                }
                x = 0;
                y += 691;
            }
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
                if (Section.SectionType == SectionTypes.LeftCorner)
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
                if (Section.SectionType == SectionTypes.RightCorner)
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

        public static void BuildTrackArray(Bitmap[,] completeTrack, List<SectionBuildingDetails> sectionBuildingDetails)
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
                       completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(TrackHorizontal), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                       // completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackHorizontal);
                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(TrackVertical), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);
                    }
                }
                //Straight track horizontal & vertical 
                else if (Section.Section.SectionType == SectionTypes.Straight)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(TrackHorizontal), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(TrackVertical), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                }
                //Finish track horizontal & vertical 
                else if (Section.Section.SectionType == SectionTypes.Finish)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(Finish), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(Finish), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                }
                //Corner left
                else if (Section.Section.SectionType == SectionTypes.LeftCorner)
                {
                    //Left -> North
                    if (Section.Direction == Direction.North)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(CornerLeftHorizontal), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                    //Left -> East
                    else if (Section.Direction == Direction.East)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(CornerRightHorizontal), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                    //Left South
                    else if (Section.Direction == Direction.South)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(CornerRightVertical), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                    else if (Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = PlaceParticipantsOnTrack(ImageCache.GetImage(CornerLeftVertical), Data.CurrentRace.GetSectionData(Section.Section).Left, Data.CurrentRace.GetSectionData(Section.Section).Right);

                    }
                }
                //Corner right
                else if (Section.Section.SectionType == SectionTypes.RightCorner)
                {
                    //Right -> North
                    if (Section.Direction == Direction.North)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerRightHorizontal);
                    }
                    //Right -> East
                    else if (Section.Direction == Direction.East)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerRightVertical);
                    }
                    //Right South
                    else if (Section.Direction == Direction.South)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerLeftVertical);
                    }
                    //Right West
                    else if (Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerLeftHorizontal);
                    }

                }


            }

        }

        public static void AddGrass(Bitmap[,] completeTrack)
        {

            Random random = new Random();

            for (int i = 0; i < CompleteTrack.GetLength(0); i++)
            {
                for (int j = 0; j < CompleteTrack.GetLength(1); j++)
                {
                    if(CompleteTrack[i,j] == null)
                    {
                        if(random.Next(0,2) == 0)
                        {
                            CompleteTrack[i, j] = ImageCache.GetImage(WaterTile);
                            
                        } else
                        {
                            CompleteTrack[i, j] = ImageCache.GetImage(GrassTile);
                        }
                        
                    }
                }
            }
        }

        //TODO: make sure not change bitmap in cache -- go cry
        public static Bitmap PlaceParticipantsOnTrack(Bitmap image, IParticipant participant1, IParticipant participant2)
        {
            var clone = (Bitmap)image.Clone();

            Graphics g = Graphics.FromImage(clone);

            if (participant1 != null) 
            { 
                if (participant1.Equipment.IsBroken)
                {
                    g.DrawImage(ImageCache.GetImage(GetParticipantColor(participant1)), 300, 200, 200, 99);
                }
                else
                {                
                    g.DrawImage(ImageCache.GetImage(GetParticipantColor(participant1)), 300, 200, 200, 99);
                    
                }
            } 
            if (participant2 != null)
            {
                if (participant2.Equipment.IsBroken)
                {
                    g.DrawImage(ImageCache.GetImage(GetParticipantColor(participant2)), 200, 400, 200, 99);
                }
                else
                {
                    g.DrawImage(ImageCache.GetImage(GetParticipantColor(participant2)), 200, 400, 200, 99);
                }
            }
            if (participant1 == null && participant2 == null )
            {
                return image;
            }

            return clone;
        }

        public static String GetParticipantColor(IParticipant participant)
        {
            switch (participant.TeamColor)
            {
                case TeamColors.Blue:
                    return Blue;
                case TeamColors.Grey:
                    return Grey;
                case TeamColors.Green:
                    return Green;
                case TeamColors.Red:
                    return Red;
                case TeamColors.Yellow:
                    return Yellow;
                default:
                    throw new ArgumentOutOfRangeException("Color null");
            }
        }


    }
}
