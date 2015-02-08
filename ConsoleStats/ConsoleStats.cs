/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System;

namespace GameOfLife
{
    class ConsoleStats
    {
        static int TotalTests;
        static int TotalTestsMax;
        static int BlocksComplete;
        static double BlockSize;
        static int BlocksDrawn;

        static void Main(string[] args)
        {
            // ◄►■║═╔╗╚╝│─┌┐└┘┬┴┼
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════╗\n" +
                              "║ Game of Life Stat Collector ║\n" +
                              "╚═════════════════════════════╝\n");
            while (true)
            {

                Console.WriteLine("┌─────────────────────┐\n" +
                                  "│ Enter Grid Settings │\n" +
                                  "└─────────────────────┘\n");
                Console.Write(" Rows: ");
                Setting.Rows = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Columns: ");
                Setting.Columns = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Life Chance: ");
                Setting.LifeChance = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Tests: ");
                TotalTestsMax = Convert.ToInt32(Console.ReadLine());
                TotalTests = 1;
                Console.Clear();

                CalculateProgressBlockSize();
                DrawEmptyProgressBar();

                // Variable to store the outcome counts
                int ExtinctionCount = 0;
                int StabilizationCount = 0;
                int OscillationCount = 0;
                int UnknownCount = 0;

                // Start a new game
                Life.Start();

                // Continue looping through tests until we've reached the amount wanted
                while (TotalTests <= TotalTestsMax)
                {
                    // We're not interested in grids that have more than 100,000 generations
                    // that likely means a unanticipated oscillator has occured.
                    while (Life.CurrentGeneration <= 100000 && Life.GameRunning)
                    {
                        Life.Next();
                    }

                    // Let's make sure the previous loop ended for the right reason
                    if (!Life.GameRunning)
                    {
                        // Check outcome and increment
                        if (Life.Extinction)
                        {
                            ExtinctionCount++;

                        }
                        else if (Life.Stabilization)
                        {
                            StabilizationCount++;
                        }
                        else if (Life.Oscillation)
                        {
                            OscillationCount++;
                        }
                    }
                    else
                    {
                        UnknownCount++;
                    }

                    // Prepare for the next test
                    if (TotalTests < TotalTestsMax)
                    {
                        Life.Reset();
                        Life.Start();
                    }

                    DrawProgressBar();

                    // Increment the test count
                    TotalTests++;

                }

                Console.WriteLine("┌─────────┐\n" +
                                  "│ Results │\n" +
                                  "└─────────┘\n");
                Console.WriteLine(" Settings: " + Setting.Rows + "x" + Setting.Columns +
                    " @ " + Setting.LifeChance + "%");
                // Display the outcome counts
                Console.WriteLine(" Extinctions: " + ExtinctionCount);
                Console.WriteLine(" Stabilizations: " + StabilizationCount);
                Console.WriteLine(" Oscillations: " + OscillationCount);
                Console.WriteLine(" Unknowns: " + UnknownCount);

                // Exit the program after one last key press
                Console.Write("\n Press Any Key to Continue");
                Console.ReadKey(true);
                Console.Clear();
                Random.NewSeed();
            }
        
        }

        static void DrawProgressBar()
        {
            BlocksComplete = (int)(BlockSize * TotalTests);

            if(TotalTests < TotalTestsMax && BlocksComplete == 50)
            {
                BlocksComplete = 49;
            }
            
            // Draw Progress Bar
            if(BlocksComplete > BlocksDrawn)
            {
                Console.SetCursorPosition(BlocksDrawn + 1, 3);
                for (int i = 0; i < (BlocksComplete - BlocksDrawn); i++)
                {
                    Console.Write("█");
                    BlocksDrawn++;
                }
            }
            
            // Draw Numeric Progress
            string LeadingZeroFormat = "";
            for (int i = 0; i < TotalTestsMax.ToString().Length; i++) LeadingZeroFormat += "0";
            Console.SetCursorPosition(53, 3);
            Console.Write(TotalTests.ToString(LeadingZeroFormat));
            Console.SetCursorPosition(0, 6);


            
        }

        private static void DrawEmptyProgressBar()
        {
            // Draw header and lines
            Console.SetCursorPosition(0, 0);
            Console.Write("┌──────────┐\n" +
                          "│ Progress │\n" +
                          "├──────────┴───────────────────────────────────────┬");
            int MaxTestsLength = (int)Math.Ceiling(Math.Log10(TotalTestsMax));
            for (int i = 0; i <= ((MaxTestsLength * 2) + 6); i++) Console.Write("─");
            Console.Write("┐\n");

            // Draw empty bar
            Console.Write("│");
            for (int i = 0; i < 50; i++) Console.Write("■");
            Console.Write("│");
            for (int i = 0; i <= (MaxTestsLength + 2); i++) Console.Write(" ");
            Console.Write("/ " + TotalTestsMax + " │\n");

            // Draw bottom line
            Console.Write("└──────────────────────────────────────────────────┴");
            for (int i = 0; i <= ((MaxTestsLength * 2) + 6); i++) Console.Write("─");
            Console.Write("┘\n");

        }

        private static void CalculateProgressBlockSize()
        {
            BlockSize = 50d / TotalTestsMax;
        }
    }
}
