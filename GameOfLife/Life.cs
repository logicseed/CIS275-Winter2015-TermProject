/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

namespace GameOfLife
{
    /// <summary>
    /// Manages all aspects of the life grid.
    /// </summary>
    internal static class Life
    {
        #region Public Members

        // Two potential end-game states.
        public static bool Stabilization = false;
        public static bool Extinction = false;
        public static bool Oscillation = false;

        // Keeps track of the game state, the rules may end the game on the user.
        public static bool GameRunning = false;

        // A count of the current generation.
        public static int CurrentGeneration = 0;

        #endregion Public Members

        #region Private Members

        // The matrices used to hold the life data.
        private static byte[,] CurrentGrid;
        private static byte[,] NextGrid;
        private static byte[,] PreviousGrid;

        /// <summary>
        /// These fields manage the size of the current grid, they are the true
        /// size of the grid, as used by this class, which means they include
        /// the buffer rows/columns.
        /// </summary>
        private static int Rows;
        private static int Columns;

        #endregion Private Members

        #region Public Interface

        /// <summary>
        /// Starts a new game. Flushes any old game data and generates new life
        /// for the new game.
        /// </summary>
        public static void Start()
        {
            CreateNewGrid();
            RandomizeLife();
        }

        /// <summary>
        /// Resets the current game. Flushes any game data and indicates the
        /// game is no longer running.
        /// </summary>
        public static void Reset()
        {
            CreateNewGrid();
            GameRunning = false;
        }

        /// <summary>
        /// Steps to the next generation of life. This public function doesn't
        /// do anything besides call NextGeneration(), but was put in place
        /// in case any changes in the future require more logic in the public
        /// interface.
        /// </summary>
        public static void Next()
        {
            NextGeneration();
        }

        /// <summary>
        /// Returns the value of the requested cell. A value of 0 indicates a
        /// dead cell, while any positive integer indicates a live cell.
        /// </summary>
        /// <param name="Row">The row of the cell requested.</param>
        /// <param name="Column">The column of the cell requested.</param>
        /// <returns>An integer value stored in the cell.</returns>
        public static byte GetCell(int Row, int Column)
        {
            return CurrentGrid[Row + 1, Column + 1];
        }

        #endregion Public Interface

        #region Private Interface

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
            PreviousGrid = new byte[Rows, Columns];

            CurrentGeneration = 0;
        }

        /// <summary>
        /// Randomly determine if each cell has life in it, based on the life
        /// chance in settings. If a cell has life then randomly determine a
        /// integer value that will be used by the graphics painter to give
        /// that cell of life a color.
        /// </summary>
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

        /// <summary>
        /// Creates the next generation and compares it with the current generation
        /// to determine if an end-game condition has occured.
        /// </summary>
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

            // Compare matrices to check for stabilization, extinction, or oscillation.
            Extinction = CheckExtinction();

            if (!Extinction)
            {
                Stabilization = CompareGrids(CurrentGrid, NextGrid);
                if (Stabilization)
                {
                    GameRunning = false;
                }
                else
                {
                    Oscillation = CompareGrids(PreviousGrid, NextGrid);
                    if (Oscillation) GameRunning = false;
                }
            }
            else
            {
                GameRunning = false;
            }


            PreviousGrid = CurrentGrid;
            CurrentGrid = NextGrid;
            
        }

        /// <summary>
        /// Checks if an individual cell has life in it.
        /// </summary>
        /// <param name="Row">The row of the cell to check for life.</param>
        /// <param name="Column">The column of the cell to check for life.</param>
        /// <returns>A boolean representing if the cell has life or not.</returns>
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

        /// <summary>
        /// Checks the surrounding eight cells for life and keeps a count. Since
        /// the rules don't care about the position of surrounding life we only
        /// need to know how many life cells there are.
        /// </summary>
        /// <param name="Row">The row of the cell of which to check the surroundings.</param>
        /// <param name="Column">The column of the cell of which to check the surroundings.</param>
        /// <returns>The total surrounding cells that have life.</returns>
        private static byte CheckSurroundings(int Row, int Column)
        {
            byte Count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // We don't want to count the cell we're checking.
                    if (HasLife(Row + i, Column + j) && !(i == 0 && j == 0))
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }

        /// <summary>
        /// Examines the next generation for life. As soon as we find a single
        /// cell with life in it we stop checking because we know we don't have
        /// an extinction event.
        /// </summary>
        /// <returns>A boolean representing if extinction has occured or not.</returns>
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

        /// <summary>
        /// Compares one grid of life to another to see if they are the same.
        /// This was written abstractly in case we wanted to implement checking
        /// for oscillating life patterns.
        /// </summary>
        /// <param name="FirstGrid">The life grid to compare.</param>
        /// <param name="SecondGrid">The life graid to compare to.</param>
        /// <returns>A boolean representing if the grids are the same or not.</returns>
        private static bool CompareGrids(byte[,] FirstGrid, byte[,] SecondGrid)
        {
            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if ((FirstGrid[i, j] > 0) != (SecondGrid[i, j] > 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if a life should randomly spawn.
        /// </summary>
        /// <returns>A boolean representing whether or not life has spawned.</returns>
        private static bool SpawnLife()
        {
            if (Random.Next(1, 100) <= Setting.LifeChance) return true;
            return false;
        }

        #endregion Private Interface

    }
}
