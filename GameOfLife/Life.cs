/*
 * TODO
 * 
 * complete rewrite using new knowledge
 * comments
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal static class Life
    {
        // Fields

        // The matrices used to hold the life data.
        private static byte[,] CurrentGrid;
        private static byte[,] NextGrid;

        // Two potential end-game states.
        public static bool Stabilization = false;
        public static bool Extinction = false;
        public static bool GameRunning = false;

        // A count of the current generation.
        public static int CurrentGeneration = 0;

        /// <summary>
        /// These fields manage the size of the current grid, they
        /// are not the true size as they include the buffer rows/columns.
        /// </summary>
        private static int Rows;
        private static int Columns;

        public static void Start()
        {
            CreateNewGrid();
            RandomizeLife();
        }

        public static void Reset()
        {
            CreateNewGrid();
            GameRunning = false;
        }

        public static void Next()
        {
            NextGeneration();
        }

        public static byte GetCell(int Row, int Column)
        {
            return CurrentGrid[Row, Column];
        }

        /// <summary>
        /// Creates two new grids for tracking life. Intended for use after
        /// the size of the board has be set. 
        /// </summary>
        private static void CreateNewGrid()
        {
            // Copy grid settings to class's private fields for later use.
            Rows = Setting.Rows + 2;
            Columns = Setting.Columns + 2;

            // Reset the two matrices by de-referencing the old matrices
            // and creating two new ones.
            CurrentGrid = new byte[Rows, Columns];
            NextGrid = new byte[Rows, Columns];

            CurrentGeneration = 0;
        }

        

        private static void RandomizeLife()
        {
            // Loop through the matrix and randomly determine if life exists
            // in each individual element.
            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (SpawnLife())
                    {
                        CurrentGrid[i, j] = (byte)Random.Next(1, 16);
                    }
                }
            }
            CurrentGeneration = 1;
            GameRunning = true;
        }

        private static void NextGeneration()
        {

            NextGrid = new byte[Rows, Columns];

            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    byte Count = CheckSurroundings(i, j);

                    // Apply game rules.
                    // Survival
                    if ((Count == 2 || Count == 3) && HasLife(i, j))
                    {
                        NextGrid[i, j] = CurrentGrid[i, j];
                    }
                    // Death
                    else if ((Count >= 4 || Count <= 1) && HasLife(i, j))
                    {
                        NextGrid[i, j] = 0;
                    }
                    // Birth
                    else if (Count == 3 && !HasLife(i, j))
                    {
                        NextGrid[i, j] = (byte)Random.Next(1, 16);
                    }
                }
            }

            CurrentGeneration++;

            // Compare matrices to check for stabilization or extinction.
            Extinction = CheckExtinction();

            if (!Extinction)
            {
                Stabilization = CompareGrids(CurrentGrid, NextGrid);
                if (Stabilization) GameRunning = false;
            }
            else
            {
                GameRunning = false;
            }

            CurrentGrid = NextGrid;
        }

        private static bool HasLife(int Row, int Column)
        {
            if (CurrentGrid[Row, Column] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static byte CheckSurroundings(int Row, int Column)
        {
            byte Count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (HasLife(Row + i, Column + j) && !(i == 0 && j == 0))
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }

        private static bool CheckExtinction()
        {
            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (NextGrid[i, j] > 0) return false;
                }
            }
            return true;
        }

        

        private static bool CompareGrids(byte[,] FirstGrid, byte[,] SecondGrid)
        {
            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (FirstGrid[i, j] != SecondGrid[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool SpawnLife()
        {
            if (Random.Next(1, 100) <= Setting.LifeChance) return true;
            return false;
        }

        
    }
}
