using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    partial class LifeManager
    {
        // Fields

        // The matrices used to hold the life data.
        private int[,] currentMatrix;
        private int[,] nextMatrix;
        private int[,] previousMatrix;

        // Two potential end-game states.
        private bool stabilization;
        private bool extinction;
        private bool gameRunning;

        // A count of the current generation.
        private int currentGeneration;

        /// <summary>
        /// These fields manage the size of the current matrix, they
        /// are not the true size as they include the buffer rows/columns.
        /// The get/set functions will return the true size.
        /// The buffer constants *must* be even.
        /// </summary>
        private int matrixRows;
        private int matrixColumns;

        // Properties

        public int MatrixRows
        {
            get
            {
                return matrixRows - 2;
            }
        }

        public int MatrixColumns
        {
            get
            {
                return matrixColumns - 2;
            }
        }

        public bool Extinction
        {
            get
            {
                return extinction;
            }
        }

        public bool Stabilization
        {
            get
            {
                return stabilization;
            }
        }

        public bool GameRunning
        {
            get
            {
                return gameRunning;
            }
        }

        public int CurrentGeneration
        {
            get
            {
                return currentGeneration;
            }
        }


        public LifeManager()
        {

        }



        /// <summary>
        /// Creates two new matrices for tracking life. Intended for use after
        /// the size of the board has be set. 
        /// </summary>
        /// <param name="matrixRows">Number of rows per matrix.</param>
        /// <param name="matrixColumns">Number of columns per matrix.</param>
        public void CreateNewMatrix(int rows, int cols)
        {
            // Copy parameters to object's private fields for later use.
            matrixRows = rows + 2;
            matrixColumns = cols + 2;

            // Reset the two matrices by de-referencing the old matrices
            // and creating two new ones.
            currentMatrix = new int[matrixRows, matrixColumns];
            previousMatrix = new int[matrixRows, matrixColumns];

            currentGeneration = 0;
        }

        public void RandomizeMatrix(int chance)
        {
            // Loop through the matrix and randomly determine if life exists
            // in each individual element.
            for (int i = 1; i < matrixRows - 1; i++)
            {
                for (int j = 1; j < matrixColumns - 1; j++)
                {
                    if (SpawnLife(chance))
                    {
                        this.currentMatrix[i, j] = Rand.Next(1, 16);
                    }
                }
            }
            this.currentGeneration = 1;
            this.gameRunning = true;
        }

        public void NextGeneration()
        {

            nextMatrix = new int[matrixRows, matrixColumns];

            for (int i = 1; i < matrixRows - 1; i++)
            {
                for (int j = 1; j < matrixColumns - 1; j++)
                {
                    int count = CheckSurroundings(i, j);

                    // Apply game rules.
                    // Survival
                    if ((count == 2 || count == 3) && HasLife(i, j))
                    {
                        nextMatrix[i, j] = currentMatrix[i, j];
                    }
                    // Death
                    else if ((count >= 4 || count <= 1) && HasLife(i, j))
                    {
                        nextMatrix[i, j] = 0;
                    }
                    // Birth
                    else if (count == 3 && !HasLife(i, j))
                    {
                        //nextMatrix[i, j] = 1;
                        nextMatrix[i, j] = Rand.Next(1, 16);
                    }
                }
            }

            currentGeneration++;

            // Compare matrices to check for stabilization or extinction.
            extinction = CheckExtinction();

            if (!extinction)
            {
                stabilization = CompareMatrices(currentMatrix, nextMatrix);
                if (!stabilization)
                {
                    stabilization = CompareMatrices(previousMatrix, nextMatrix);
                }
                if (stabilization) gameRunning = false;
            }
            else
            {
                this.gameRunning = false;
            }

            previousMatrix = currentMatrix;
            currentMatrix = nextMatrix;

        }

        private bool HasLife(int _row, int _col)
        {
            if (currentMatrix[_row, _col] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int CheckSurroundings(int row, int col)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (HasLife(row + i, col + j) && !(i == 0 && j == 0))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private bool CheckExtinction()
        {
            for (int i = 1; i < matrixRows - 1; i++)
            {
                for (int j = 1; j < matrixColumns - 1; j++)
                {
                    if (nextMatrix[i, j] > 0) return false;
                }
            }
            return true;
        }

        public int GetElement(int row, int col)
        {
            return currentMatrix[row, col];
        }

        private bool CompareMatrices(int[,] _firstMatrix, int[,] _secondMatrix)
        {
            bool likeMatrices = true;

            for (int i = 1; i < matrixRows - 1; i++)
            {
                for (int j = 1; j < matrixColumns - 1; j++)
                {
                    if (_firstMatrix[i, j] != _secondMatrix[i, j])
                    {
                        likeMatrices = false;
                    }
                }
            }

            return likeMatrices;
        }

        private bool SpawnLife(int chance)
        {
            if (Rand.Next(1, 100) <= chance) return true;
            return false;
        }

        internal void ResetGame()
        {
            CreateNewMatrix(1, 1);
        }
    }
}
