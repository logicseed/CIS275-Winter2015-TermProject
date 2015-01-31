/* 
 * 
 * 
 */

/*
 * The Game of Life - Marc King
 * Programmed for CIS275 - Winter 2015
 * 
 * Program.cs
 * 
 * The main part of the Program class. Handles initializing the Windows form, 
 * and the various static classes used by the program. Also controls most of
 * the program flow.
 * 
 */

using System;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Program : Form
    {

        /// <summary>
        /// Handles the timing for the auto-step functions.
        /// </summary>
        private System.Windows.Forms.Timer AutoStepTimer;

        /// <summary>
        /// The constructor for the primary Windows form. Initializes the form,
        /// states, and styles before letting the paint event handle the rest
        /// of the program flow.
        /// </summary>
        public Program()
        {
            InitializeProgram();
            Style.Initialize();
            Screen.Initialize(this.Size);
        }

        /// <summary>
        /// Initializes the primary Windows form before handing graphics off
        /// to the screen painter.
        /// </summary>
        private void InitializeProgram()
        {
            // Build Windows form
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(100, 100);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = Properties.Resources.Icon;
            KeyPreview = true;
            Name = "Program";
            Text = "The Game of Life";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Paint += new System.Windows.Forms.PaintEventHandler(Program_Paint);
            KeyDown += new System.Windows.Forms.KeyEventHandler(Program_KeyDown);
            SetStyle(ControlStyles.DoubleBuffer, true);
            Cursor.Hide();

            // Setup AutoStepTimer
            AutoStepTimer = new System.Windows.Forms.Timer();
            AutoStepTimer.Interval = 50;
            AutoStepTimer.Tick += new System.EventHandler(AutoStep);
        }

        // We override the OnPaintBackground function to allow the paint event
        // to handle all the graphics. This helps prevent screen flickering.
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        /// <summary>
        /// Handles the Paint event from the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Program_Paint(object sender, PaintEventArgs e)
        {
            // Make sure the graphics painter has the correct screen size.
            Screen.Initialize(this.Size);
            // Have the graphics painter build the screen buffer.
            Screen.DrawScreen();
            // Paint the screen buffer to the main form.
            e.Graphics.DrawImage(Screen.GetBuffer(), 0,0);
        }

        /// <summary>
        /// Tells the graphics painter that the splash screen needs to be drawn.
        /// </summary>
        private void ShowSplashScreen()
        {
            State.Screen = ScreenState.Splash;
        }

        /// <summary>
        /// Tells the graphics painter that the splash screen has been displayed
        /// and that it now needs to measure all the screen elements.
        /// </summary>
        private void CloseSplashScreen()
        {
            State.Screen = ScreenState.FirstDisplay;
        }

        /// <summary>
        /// Tells the graphics painter that the introduction screen needs to
        /// be displayed.
        /// </summary>
        private void ShowIntroductionPopup()
        {
            State.Popup = PopupState.Introduction;
        }

        /// <summary>
        /// Tells the graphics painter that the introduction screen no longer
        /// needs to be displayed.
        /// </summary>
        private void CloseIntroductionPopup()
        {
            State.Popup = PopupState.None;
        }

        /// <summary>
        /// Tells the graphics painter that the help screen needs to be displayed
        /// and pauses auto-stepping if it is happening.
        /// </summary>
        private void ShowHelpPopup()
        {
            State.Popup = PopupState.Help;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        /// <summary>
        /// Tells the graphics painter that the help screen no longer needs to
        /// be displayed.
        /// </summary>
        private void CloseHelpPopup()
        {
            State.Popup = PopupState.None;
        }

        /// <summary>
        /// Tells the graphics painter that the credits should be displayed and
        /// pauses auto-stepping if it is happening.
        /// </summary>
        private void ShowCreditsPopup()
        {
            State.Popup = PopupState.Credits;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        /// <summary>
        /// Tells the graphics painter that the credits no longer need to be
        /// displayed.
        /// </summary>
        private void CloseCreditsPopup()
        {
            State.Popup = PopupState.None;
        }

        /// <summary>
        /// Tells the graphics painter that the user wants to exit the game, and
        /// that they should be prompted for confirmation. This function will also
        /// pause auto-stepping if it is happening.
        /// </summary>
        private void ShowExitConfirmationPopup()
        {
            State.Popup = PopupState.ExitConfirmation;
            if (State.Screen == ScreenState.GameRunning) PauseAutoStep();
        }

        /// <summary>
        /// Tells the graphics painter that the user did not want to exit and
        /// that the confirmation should be closed.
        /// </summary>
        private void CloseExitConfirmationPopup()
        {
            State.Popup = PopupState.None;
        }

        /// <summary>
        /// Tells the graphics painter to show the outcome of the game.
        /// </summary>
        private void ShowOutcomePopup()
        {
            State.Popup = PopupState.Outcome;
        }

        /// <summary>
        /// Tells the graphics painter that the outcome no longer needs to be
        /// displayed and that the game should be reset.
        /// </summary>
        private void CloseOutcomePopup()
        {
            State.Popup = PopupState.None;
            State.Screen = ScreenState.NoGame;
        }

        /// <summary>
        /// Begins a game with the current settings.
        /// </summary>
        private void StartGame()
        {
            Life.Start();
            State.Screen = ScreenState.GameStopped;
        }

        /// <summary>
        /// Advances the game a single generation. If a game is not running it
        /// will additionally start a game with the current settings.
        /// </summary>
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

        /// <summary>
        /// Begins auto-stepping through generations. If a game is not running it
        /// will additionally start a game with the current settings.
        /// </summary>
        private void StartAutoStep()
        {
            if (State.Screen == ScreenState.NoGame)
            {
                StartGame();
            }
            AutoStepTimer.Enabled = true;
            State.Screen = ScreenState.GameRunning;
        }

        /// <summary>
        /// Handles the AutoStepTimer Tick event and steps to the next generation
        /// unless the game has ended, in whcih case the outcome is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Tells the AutoStepTimer to pause the autostepping, but keeps the
        /// current game.
        /// </summary>
        private void PauseAutoStep()
        {
            AutoStepTimer.Enabled = false;
            State.Screen = ScreenState.GameStopped;
        }

        /// <summary>
        /// Resets the current game and opens the grid setting back up for 
        /// modifying.
        /// </summary>
        private void ResetGame()
        {
            PauseAutoStep();
            State.Screen = ScreenState.NoGame;
            Life.Reset();
        }

        /// <summary>
        /// Exits the program. This method is only called after confirmation from
        /// the user.
        /// </summary>
        private void ExitGame()
        {
            this.Close();
        }

        /// <summary>
        /// Increases the rows in the settings if the current value is less
        /// than the maximum.
        /// </summary>
        private void IncreaseRows()
        {
            if (Setting.Rows < Screen.CalculateMaxRows())
            {
                Setting.Rows++;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Decreases the rows in the settings if the current value is greater
        /// than the minimum.
        /// </summary>
        private void DecreaseRows()
        {
            if (Setting.Rows > Setting.MinimumRows)
            {
                Setting.Rows--;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Increases the columns in the settings if the current value is less
        /// than the maximum.
        /// </summary>
        private void IncreaseColumns()
        {
            if (Setting.Columns < Screen.CalculateMaxColumns())
            {
                Setting.Columns++;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Decreases the columns in the settings if the current value is greater
        /// than the minimum.
        /// </summary>
        private void DecreaseColumns()
        {
            if (Setting.Columns > Setting.MinimumColumns)
            {
                Setting.Columns--;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Increases the cell size in the settings if the current value is less
        /// than the maximum.
        /// </summary>
        private void IncreaseCellSize()
        {
            if (Setting.CellSize < Screen.CalculateMaxCellSize())
            {
                Setting.CellSize++;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Decreases the cell size in the settings if the current value is
        /// greater than the minimum.
        /// </summary>
        private void DecreaseCellSize()
        {
            if (Setting.CellSize > Setting.MinimumCellSize)
            {
                Setting.CellSize--;
                Screen.InvalidateGrid();
            }
        }

        /// <summary>
        /// Increases the chance for life to occur in a cell if the current
        /// value is less than the maximum.
        /// </summary>
        private void IncreaseLifeChance()
        {
            if (Setting.LifeChance < Setting.MaximumLifeChance)
            {
                Setting.LifeChance++;
            }
        }

        /// <summary>
        /// Decreases the chance of life to occur in a cell if the current
        /// value is greater than the minimum.
        /// </summary>
        private void DecreaseLifeChance()
        {
            if (Setting.LifeChance > Setting.MinimumLifeChance)
            {
                Setting.LifeChance--;
            }
        }
    }
}
