using System;
using Model;

namespace RaceSim
{
    public static class TrackVisualisation
    {


        public static int Xmax;
        public static int Ymax;

        public static string[,,] completeTrack;

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

        public static void DrawTrack(Track track, int x, int y)
        {
            Xmax = x;
            Ymax = y;

            completeTrack = new string[Ymax, Xmax, 4];


            int tempX = 0;
            int tempY = 0;

            foreach (var section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        fillThreeDArray(tempX, tempY, _startHorizontal);
                        break;

                    case SectionTypes.Straight:
                        fillThreeDArray(tempX, tempY, _trackHorizontal);
                        break;

                    case SectionTypes.RightCorner:
                        fillThreeDArray(tempX, tempY, _cornerRightHorinzontal);
                        break;
                    case SectionTypes.LeftCorner:
                        fillThreeDArray(tempX, tempY, _cornerLeftHorizontal);
                        break;
                    case SectionTypes.Finish:
                        fillThreeDArray(tempX, tempY, _finishHorizontal);
                        break;

                }
                tempY++;

                if (tempY % 6 == 0)
                {
                    tempX++;
                    tempY = 0;
                }
            }

            DrawTrackComponentWithCompleteTrackArray(completeTrack);

        }
        public static void fillThreeDArray(int x, int y, string[] section)
        {
            for (int j = 0; j < 4; j++)
            {
                completeTrack[x, y, j] = section[j];
            }

        }
        public static void DrawTrackComponent(String[] trackLines)
        {
            foreach (String trackLine in trackLines)
            {
                Console.WriteLine(trackLine);
            }
            Console.WriteLine();
        }

        //TODO: Fix number position in for loops to make sense based on X and Y in array
        public static void DrawTrackComponentWithCompleteTrackArray(String[,,] completeTrack)
        {
            for (int x = 0; x < Ymax; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int k = 0; k < Xmax; k++)
                    {
                        if (completeTrack[x, k, y] == null)
                        {
                            Console.Write("    ");
                        }
                        else
                        {
                            Console.Write(completeTrack[x, k, y]);
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