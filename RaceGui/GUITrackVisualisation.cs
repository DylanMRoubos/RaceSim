﻿using Controller;
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
        //Canvas of the track size
        public static Bitmap canvas;

        //Keep track of all the image variables
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

        const string Fire = ".\\Assets\\Fire.png";
        #endregion

        //Draw the track every frame
        public static BitmapSource DrawTrack(Track track)
        {
            CreateArrayWithTrack(track);

            canvas = ImageCache.CreateBitmap(GetHighestXValue(SectionBuildingGridDetails) * 256, GetHighestYValue(SectionBuildingGridDetails) * 256);

            CompleteTrack = new Bitmap[GetHighestYValue(SectionBuildingGridDetails), GetHighestXValue(SectionBuildingGridDetails)];

            BuildTrackArray(CompleteTrack, SectionBuildingGridDetails);
            AddGrass(CompleteTrack);

            Graphics g = PlaceTrack(canvas);
            PlaceParticipantsOnTrack(g, SectionBuildingGridDetails);
            return ImageCache.CreateBitmapSourceFromGdiBitmap(canvas);
        }

        //Create the track array based on the track given
        public static void CreateArrayWithTrack(Track track)
        {
            Track = track;
            SectionBuildingGridDetails = new List<SectionBuildingDetails>();

            FillSectionBuildingGridDetailsArray(SectionBuildingGridDetails, Track);
            UpdateListWithLowestXAndY(SectionBuildingGridDetails, GetLowestXValue(SectionBuildingGridDetails), GetLowestYValue(SectionBuildingGridDetails));
        }

        //Place the track on the canvas
        public static Graphics PlaceTrack(Bitmap canvas)
        {
            int x = 0;
            int y = 0;

            Graphics g = Graphics.FromImage(canvas);

            for (int i = 0; i < CompleteTrack.GetLength(0); i++)
            {
                for (int j = 0; j < CompleteTrack.GetLength(1); j++)
                {
                    g.DrawImage(CompleteTrack[i, j], x, y, 256, 256);
                    x += 256;
                }
                x = 0;
                y += 256;
            }
            return g;
        }

        //Add the sections to the grid details array
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

        //Update the sectionbuildingarray with the minimal of 0 to create a new array
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

        //Build the complete track array
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
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackHorizontal);

                        // completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackHorizontal);
                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackVertical);
                    }
                }
                //Straight track horizontal & vertical 
                else if (Section.Section.SectionType == SectionTypes.Straight)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackHorizontal);

                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(TrackVertical);

                    }
                }
                //Finish track horizontal & vertical 
                else if (Section.Section.SectionType == SectionTypes.Finish)
                {
                    //Horizontal
                    if (Section.Direction == Direction.East || Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(Finish);
                    }
                    //Vertical
                    else
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(Finish);
                    }
                }
                //Corner left
                else if (Section.Section.SectionType == SectionTypes.LeftCorner)
                {
                    //Left -> North
                    if (Section.Direction == Direction.North)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerLeftHorizontal);
                    }
                    //Left -> East
                    else if (Section.Direction == Direction.East)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerRightHorizontal);
                    }
                    //Left South
                    else if (Section.Direction == Direction.South)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerRightVertical);
                    }
                    else if (Section.Direction == Direction.West)
                    {
                        completeTrack[Section.Y, Section.X] = ImageCache.GetImage(CornerLeftVertical);
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

        //Add grass on the empty places
        public static void AddGrass(Bitmap[,] completeTrack)
        {

            Random random = new Random();

            for (int i = 0; i < CompleteTrack.GetLength(0); i++)
            {
                for (int j = 0; j < CompleteTrack.GetLength(1); j++)
                {
                    if (CompleteTrack[i, j] == null)
                    {
                        CompleteTrack[i, j] = ImageCache.GetImage(GrassTile);
                        /*if(random.Next(0,2) == 0)
                        {
                            CompleteTrack[i, j] = ImageCache.GetImage(WaterTile);
                            
                        } else
                        {
                            CompleteTrack[i, j] = ImageCache.GetImage(GrassTile);
                        }*/

                    }
                }
            }
        }

        //Place the participants if there is a driver on a section
        public static void PlaceParticipantsOnTrack(Graphics g, List<SectionBuildingDetails> sectionBuildingDetails)
        {

            foreach (var Section in sectionBuildingDetails)
            {
                var Sectiondata = Data.CurrentRace.GetSectionData(Section.Section);

                if (Sectiondata.Left != null)
                {
                    if (Sectiondata.Left.Equipment.IsBroken)
                    {
                        g.DrawImage(ImageCache.GetImage(Fire), Section.X * 256 + 128, Section.Y * 256, 128, 128);
                    }
                    Bitmap carleft = GetParticipantImage(Sectiondata.Left, Section.Direction);
                    g.DrawImage(carleft, ((Section.X * 256 + 128)), ((Section.Y * 256)), 128, 128);
                }
                if (Sectiondata.Right != null)
                {
                    if (Sectiondata.Right.Equipment.IsBroken)
                    {
                        g.DrawImage(ImageCache.GetImage(Fire), Section.X * 256, Section.Y * 256 + 128, 128, 128);
                    }
                    Bitmap carRight = GetParticipantImage(Sectiondata.Right, Section.Direction);
                    g.DrawImage(carRight, ((Section.X * 256)), ((Section.Y * 256 + 128)), 128, 128);

                }
            }
        }

        //Get the image of the car based on the direction
        public static Bitmap GetParticipantImage(IParticipant participant, Direction direction)
        {

            switch (participant.TeamColor)
            {
                case TeamColors.Blue:
                    return RotateImage(ImageCache.GetImage(Blue), direction);
                case TeamColors.Grey:
                    return RotateImage(ImageCache.GetImage(Grey), direction);
                case TeamColors.Green:
                    return RotateImage(ImageCache.GetImage(Green), direction);
                case TeamColors.Red:
                    return RotateImage(ImageCache.GetImage(Red), direction);
                case TeamColors.Yellow:
                    return RotateImage(ImageCache.GetImage(Yellow), direction);
                default:
                    throw new ArgumentOutOfRangeException("Color null");
            }

        }
       
        //Method to rotate a bitmap
        public static Bitmap RotateImage(Bitmap b, Direction direction)
        {
            int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));

            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(maxside, maxside);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);

            //move rotation point to center of image

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);

            switch (direction)
            {
                case Direction.South:
                    g.RotateTransform(90);
                    break;
                case Direction.West:
                    g.RotateTransform(180);
                    break;
                case Direction.North:
                    g.RotateTransform(270);
                    break;
            }

            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);

            g.DrawImage(b, 0, 0, 128, 128);
            return returnBitmap;
        }

    }
}
