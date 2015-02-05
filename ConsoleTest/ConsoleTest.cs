/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System;
using System.Threading;

namespace GameOfLife
{
    class ConsoleTest
    {
        static void Main(string[] args)
        {
            // Set the rows and columns to sensical values
            Setting.Rows = 20;
            Setting.Columns = 50;

            // Start a new game
            Life.Start();

            // Continue looping through generations until an outcome is reached
            while(true)
            {
                // A short thread-safe pause between generations
                Thread.Sleep(50);

                // Put the cursor at the origin
                Console.SetCursorPosition(0, 0);

                // Have the life class generate the next generation
                Life.Next();

                // Display the current generation
                DisplayMatrix();

                // Display the current generation count
                Console.WriteLine("Generation: " + Life.CurrentGeneration);

                // Break the loop and display an outcome if one has been reached
                if(Life.Extinction)
                {
                    Console.WriteLine("Life went extinct.");
                    break;
                }
                else if (Life.Stabilization)
                {
                    Console.WriteLine("Life has stabilized.");
                    break;
                }
                else if (Life.Oscillation)
                {
                    Console.WriteLine("Life oscillating.");
                    break;
                }
            }

            // Exit the program after one last key press
            Console.ReadKey(true);
        }

        private static void DisplayMatrix()
        {
            // Loop through the elements
            for (int i = 1; i <= Setting.Rows; i++)
            {
                for (int j = 1; j <= Setting.Columns; j++)
                {
                    // Write a different character to the console depending on
                    // if the element has life or not
                    if (Life.GetCell(i, j) > 0)
                    {
                        Console.Write("▓");
                    }
                    else
                    {
                        Console.Write("▒");
                    }

                }
                // Move to the next line
                Console.WriteLine();
            }
        }

    }
}
