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
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Program : Form
    {
        
        public Program()
        {
            InitializeComponent(); // rename after integrating Designer code : InitializeProgram();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Cursor.Hide();
            Style.Initialize(this);
            Screen.Initialize(this.Size);
        }

        // We override the OnPaintBackground function to allow the paint event
        // to handle all the graphics. This prevents screen flickering.
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        private void Program_Paint(object sender, PaintEventArgs e)
        {
            Screen.Initialize(this.Size);
            Screen.DrawScreen();
            e.Graphics.DrawImage(Screen.GetBuffer(), 0,0);
        }


        private void ShowSplashScreen()
        {
            State.Screen = ScreenState.Splash;
        }

        private void CloseSplashScreen()
        {
            State.Screen = ScreenState.FirstDisplay;
        }

        private void ShowHelpPopup()
        {
            State.Popup = PopupState.Help;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        private void CloseHelpPopup()
        {
            State.Popup = PopupState.None;
        }

        private void ShowCreditsPopup()
        {
            State.Popup = PopupState.Credits;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        private void CloseCreditsPopup()
        {
            State.Popup = PopupState.None;
        }

        private void ShowExitConfirmationPopup()
        {
            State.Popup = PopupState.ExitConfirmation;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        private void CloseExitConfirmationPopup()
        {
            State.Popup = PopupState.None;
        }

        private void ShowOutcomePopup()
        {
            State.Popup = PopupState.Outcome;
        }

        private void CloseOutcomePopup()
        {
            State.Popup = PopupState.None;
            State.Screen = ScreenState.NoGame;
        }

        private void StartGame()
        {
            Life.Start();
            State.Screen = ScreenState.GameStopped;
        }

        private void NextStep()
        {
            if (State.Screen == ScreenState.NoGame)
            {
                StartGame();
            }
            else
            {
                Life.Next();
            }
        }

        private void StartAutoStep()
        {
            if (State.Screen == ScreenState.NoGame)
            {
                StartGame();
            }
            AutoStepTimer.Enabled = true;
            State.Screen = ScreenState.GameRunning;
        }

        private void AutoStep(object sender, EventArgs e)
        {
            if (Life.GameRunning)
            {
                NextStep();
            }
            else
            {
                PauseAutoStep();
                ShowOutcomePopup();
            }
            this.Invalidate();
        }

        private void PauseAutoStep()
        {
            AutoStepTimer.Enabled = false;
            State.Screen = ScreenState.GameStopped;
        }

        private void ResetGame()
        {
            PauseAutoStep();
            State.Screen = ScreenState.NoGame;
            Life.Reset();
        }

        private void ExitGame()
        {
            this.Close();
        }

        private void IncreaseRows()
        {
            if (Setting.Rows < Screen.CalculateMaxRows())
            {
                Setting.Rows++;
                Screen.InvalidateGrid();
            }
        }
        private void DecreaseRows()
        {
            if (Setting.Rows > Setting.MinimumRows)
            {
                Setting.Rows--;
                Screen.InvalidateGrid();
            }
        }

        private void IncreaseColumns()
        {
            if (Setting.Columns < Screen.CalculateMaxColumns())
            {
                Setting.Columns++;
                Screen.InvalidateGrid();
            }
        }
        private void DecreaseColumns()
        {
            if (Setting.Columns > Setting.MinimumColumns)
            {
                Setting.Columns--;
                Screen.InvalidateGrid();
            }
        }

        private void IncreaseCellSize()
        {
            if (Setting.CellSize < Screen.CalculateMaxCellSize())
            {
                Setting.CellSize++;
                Screen.InvalidateGrid();
            }
        }
        private void DecreaseCellSize()
        {
            if (Setting.CellSize > Setting.MinimumCellSize)
            {
                Setting.CellSize--;
                Screen.InvalidateGrid();
            }
        }

        private void IncreaseLifeChance()
        {
            if (Setting.LifeChance < Setting.MaximumLifeChance)
            {
                Setting.LifeChance++;
            }
        }
        private void DecreaseLifeChance()
        {
            if (Setting.LifeChance > Setting.MinimumLifeChance)
            {
                Setting.LifeChance--;
            }
        }
    }
}
