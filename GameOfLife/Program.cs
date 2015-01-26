/* 
 * 
 * 
 * FILES
 * 
 * What each partial class contains:
 * Program.cs - Initializes the windows form.
 * Program.Keys.cs - Handles all keyboard input. There should only be function
 *     calls and branching in this file; no actual code.
 * Program.Graphics.cs - Handles all the drawing to the screen. Only code
 *     necessary for drawing should be contained in this file.
 * Program.Styles.cs - Handles all of the colors and fonts.
 * Program.States.cs - Handles all the various states of the program and provides
 *     methods to toggle them.
 * 
 * 
 * TODO
 * 
 * Move screen element size calculations to their own functions, do during initialization
 * Convert ints to bytes in LifeManager
 * Remove Program.Designer.cs and integrate into other files (primarily Program.cs)
 * Make comments everywhere.
 * Refactor and make more efficient.
 * Clean-up usings.
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace GameOfLife
{
    public partial class Program : Form
    {
        

        

        // Grid fields
        private int gridRows = 1;
        private int gridColumns = 1;
        private byte gridCellSize = 50;
        private byte lifeChance = 50;


        private bool Increase = true;
        private bool Decrease = false;

        private LifeManager Life = new LifeManager();

        

        

       

        

        public Program()
        {
            InitializeComponent(); // rename after integrating Designer code : InitializeProgram();
            Cursor.Hide();
            InitializeStyles();
            InitializeStates();
            
            //this.SetStyle(ControlStyles.DoubleBuffer, true);


            

            //this.BackColor = uiColor[1];

        }


        private void CancelExit()
        {
            State[ShowConfirmExit] = false;
        }

        private void EndGame()
        {
            State[GameRunning] = false;
            Life.ResetGame();
        }

        private void PauseAutoStep()
        {
            AutoStepTimer.Enabled = false;
            State[AutoStepping] = false;
        }

        private void BeginAutoStep()
        {
            if (!State[GameRunning])
            {
                BeginGame();
            }
            AutoStepTimer.Enabled = true;
            State[AutoStepping] = true;
        }

        private void CloseSplashScreen()
        {
            State[ShowSplash] = false;
        }

        private void OpenHelpScreen()
        {
            State[ShowHelp] = true;
            if (State[AutoStepping])
            {
                PauseAutoStep();
            }
        }

        

        private void NextStep()
        {
            State[GameRunning] = Life.GameRunning;
            if (State[GameRunning])
            {
                Life.NextGeneration();
                State[GameRunning] = Life.GameRunning;
            }
            if (State[AutoStepping] && State[GameRunning])
            {
                this.Invalidate();
            }
            else
            {
                PauseAutoStep();
            }
            if (Life.Stabilization || Life.Extinction)
            {
                State[GameComplete] = true;
                State[ShowOutcome] = true;
                this.Invalidate();
            }
        }

        private void AutoStep(object sender, EventArgs e)
        {
            NextStep();
        }

        private void BeginGame()
        {
            Life.CreateNewMatrix(gridRows, gridColumns);
            Life.RandomizeMatrix(lifeChance);
            State[GameRunning] = Life.GameRunning;
        }

        private void ChangeRows(bool Change)
        {
            if (!State[GameRunning] && Life.GameRunning)
            {
                Life.ResetGame();
                State[GameRunning] = false;
            }

                if (Change == Increase && gridRows < CalculateMaxRows())
                {
                    gridRows++;
                }
                else if (Change == Decrease && gridRows > 1)
                {
                    gridRows--;
                }

            InvalidateGrid();
        }

        private void ChangeColumns(bool Change)
        {
            if (!State[GameRunning] && Life.GameRunning)
            {
                Life.ResetGame();
                State[GameRunning] = false;
            }
            if (Change == Increase && gridColumns < CalculateMaxColumns())
            {
                gridColumns++;
            }
            else if (Change == Decrease && gridColumns > 1)
            {
                gridColumns--;
            }
            InvalidateGrid();
        }

        private void ChangeCellSize(bool Change)
        {
            if (!State[GameRunning] && Life.GameRunning)
            {
                Life.ResetGame();
                State[GameRunning] = false;
            }
            if (Change == Increase && gridCellSize < CalculateMaxCellSize())
            {
                gridCellSize++;
            }
            else if (Change == Decrease && gridCellSize > 5)
            {
                gridCellSize--;
            }
            InvalidateGrid();
        }

        private void ChangeLifeChance(bool Change)
        {
            if (Change == Increase && lifeChance < 100)
            {
                lifeChance++;
            }
            else if (Change == Decrease && lifeChance > 1)
            {
                lifeChance--;
            }
        }

        private Size CalculateGridSpace()
        {
            Size gridSpace = new Size(
                this.Width - (elementMargin.Width * 2),
                this.Height -
                    (int)Math.Ceiling(Math.Max(
                        totalLogoSize.Height,
                        totalHelpPromptSize.Height
                    )) -
                    (int)Math.Ceiling(Math.Max(
                        totalGenerationCountSize.Height,
                        totalSettingsSize.Height
                    )) + (elementMargin.Width * 6)
            );

            return gridSpace;
        }

        private int CalculateMaxRows()
        {
            int maxRows = (CalculateGridSpace().Height - 1) / (gridCellSize + 1);
            return maxRows;
        }

        private int CalculateMaxColumns()
        {
            int maxColumns = (CalculateGridSpace().Width - 1) / (gridCellSize + 1);
            return maxColumns;
        }

        private int CalculateMaxCellSize()
        {
            int maxCellSize = Math.Min(
                ((CalculateGridSpace().Height - 1) / gridRows) - 1,
                ((CalculateGridSpace().Width - 1) / gridColumns) - 1
                );
            return maxCellSize;
        }

        private void SetDefaultGridSize()
        {
            gridRows = CalculateMaxRows();
            gridColumns = CalculateMaxColumns();
        }

        private void ConfirmExit()
        {
            State[ShowConfirmExit] = true;
            if(State[AutoStepping])
            {
                PauseAutoStep();
            }
        }
    }
}
