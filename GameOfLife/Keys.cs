/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System.Windows.Forms;
using System.Media;

namespace GameOfLife
{
    // This partial class is part of the Windows form Program class that resides
    // in Program.cs. This was separated into this file to keep the key press
    // logic in an easy to locate place.
    partial class Program
    {
        /// <summary>
        /// Handles all key presses during the running of the game. Due to the
        /// complexity of the branching, this method should only contain branching
        /// code and function calls. No other code should be included.
        /// </summary>
        private void Program_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.H:
                    // H will open the help popup at all points during the game
                    // except when the splash screen is being displayed.
                    if (State.Screen != ScreenState.Splash &&
                        State.Popup == PopupState.None)
                    {
                        ShowHelpPopup();
                        break;
                    }
                    goto default; // This handles H used as any-key.
                case Keys.X:
                    // X will reset the current game when a game is running.
                    if ((State.Screen == ScreenState.GameStopped ||
                        State.Screen == ScreenState.GameRunning) &&
                        State.Popup == PopupState.None)
                    {
                        ResetGame();
                        break;
                    }
                    goto default; // This handles X used as any-key.
                case Keys.C:
                    // C will shows the credits screen at all points during the
                    // game except when the splash screen is being displayed.
                    if (State.Screen != ScreenState.Splash &&
                        State.Popup == PopupState.None)
                    {
                        ShowCreditsPopup();
                        break;
                    }
                    goto default; // This handles C used as any-key.
                case Keys.Escape:
                    // ESC will exit the game at all point during the game, except
                    // when a popup screen is showing. However, if a game is
                    // currently running the user will be asked to confirm exiting.
                    if (State.Screen != ScreenState.Splash &&
                        (State.Popup == PopupState.ExitConfirmation ||
                        State.Popup == PopupState.None))
                    {
                        if(State.Popup == PopupState.ExitConfirmation)
                        {
                            ExitGame();
                            break;
                        }
                        else
                        {
                            ShowExitConfirmationPopup();
                            break;
                        }
                    }
                    goto default; // This handles ESC used as any-key.
                case Keys.Up:
                    // UP only functions when the game isn't running. Without any
                    // modifier keys it will increase the row count. With the
                    // SHIFT modifer key it will increase the cell size, and with
                    // the CTRL modifier key it will increase the life chance.
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        // Handle modifier keys.
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY UP
                            IncreaseRows();
                            break;
                        }
                        else if(e.Shift && !e.Control)
                        {
                            // SHIFT UP
                            IncreaseCellSize();
                            break;
                        }
                        else if (!e.Shift && e.Control)
                        {
                            // CONTROL UP
                            IncreaseLifeChance();
                            break;
                        }
                    }
                    goto default; // This handles UP used as any-key.
                case Keys.Down:
                    // DOWN only functions when the game isn't running. Without any
                    // modifier keys it will decrease the row count. With the
                    // SHIFT modifer key it will decrease the cell size, and with
                    // the CTRL modifier key it will decrease the life chance.
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        // Handle modifier keys.
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY DOWN
                            DecreaseRows();
                            break;
                        }
                        else if (e.Shift && !e.Control)
                        {
                            // SHIFT DOWN
                            DecreaseCellSize();
                            break;
                        }
                        else if (!e.Shift && e.Control)
                        {
                            // CONTROL DOWN
                            DecreaseLifeChance();
                            break;
                        }
                    }
                    goto default; // This handles DOWN used as any-key.
                case Keys.Left:
                    // LEFT only functions when the game isn't running. It will
                    // decrease the column count.
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY LEFT
                            DecreaseColumns();
                            break;
                        }
                    }
                    goto default; // This handles LEFT used as any-key.
                case Keys.Right:
                    // RIGHT only functions when the game isn't running. It will
                    // increase the column count.
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY RIGHT
                            IncreaseColumns();
                            break;
                        }
                    }
                    goto default; // This handles RIGHT used as any-key.
                case Keys.Space:
                    // SPACE functions at all points during the game except when
                    // a popup screen is being displayed. It will step through
                    // generations of the game.
                    if (State.Screen != ScreenState.Splash &&
                        State.Popup == PopupState.None)
                    {
                        if (State.Screen == ScreenState.NoGame)
                        {
                            StartGame();
                            break;
                        }
                        else if (State.Screen == ScreenState.GameRunning)
                        {
                            PauseAutoStep();
                            break;
                        }
                        else if (State.Screen == ScreenState.GameStopped)
                        {
                            NextStep();
                            break;
                        }
                    }
                    goto default; // This handles SPACE used as any-key.
                case Keys.Enter:
                    // ENTER functions at all points during the game except when
                    // a popup screen is being displayed. It will start auto-
                    // stepping if the game isn't auto-stepping, or pause auto-
                    // stepping if it is.
                    if (State.Screen != ScreenState.Splash &&
                        State.Popup == PopupState.None)
                    {
                        if (State.Screen == ScreenState.GameRunning)
                        {
                            PauseAutoStep();
                            break;
                        }
                        else if (State.Screen == ScreenState.GameStopped ||
                            State.Screen == ScreenState.NoGame)
                        {
                            StartAutoStep();
                            break;
                        }
                    }
                    goto default; // This handles ENTER used as any-key.
                
                
                // Developer-Only Keys BEGIN
                case Keys.M:
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        if (e.Shift && e.Control)
                        {
                            Screen.MaximizeGridSize();
                            break;
                        }
                    }
                    goto default;
                case Keys.N:
                    if (State.Screen == ScreenState.NoGame &&
                        State.Popup == PopupState.None)
                    {
                        if (e.Shift && e.Control)
                        {
                            Screen.MinimizeGridSize();
                            break;
                        }
                    }
                    goto default;
                // Developer-Only Keys END

                // Used to ignore the Print Screen button when taking screen
                // shots for the report.
                case Keys.PrintScreen: break;

                default:
                    // Any other key that is pressed will close the splash screen,
                    // cancel an exit confirmation, exit the help screen, or be
                    // ignored.
                    if (State.Screen == ScreenState.Splash)
                    {
                        CloseSplashScreen();
                    }
                    else if (State.Popup == PopupState.Introduction)
                    {
                        CloseIntroductionPopup();
                    }
                    else if (State.Popup == PopupState.Help)
                    {
                        CloseHelpPopup();
                    }
                    else if (State.Popup == PopupState.Credits)
                    {
                        CloseCreditsPopup();
                    }
                    else if (State.Popup == PopupState.ExitConfirmation)
                    {
                        CloseExitConfirmationPopup();
                    }
                    else if (State.Popup == PopupState.Outcome)
                    {
                        CloseOutcomePopup();
                    }
                    else
                    {
                        // Handle invalid key press
                        // SystemSounds.Beep.Play(); // only works if users have sounds enabled
                    }
                    break;
            }

            // After handling a key press, our display almost certainly needs
            // to be refreshed. We'll force a Paint event.
            this.Invalidate();
        }
    }
}

