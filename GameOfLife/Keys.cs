using System.Windows.Forms;
using System.Media;

namespace GameOfLife
{
    partial class Program
    {
        /// <summary>
        /// Handles all key presses during the running of the game. Due to the
        /// complexity of the branching, this method should only contain branching
        /// code, function calls, and state changes. No other code should be included.
        /// </summary>
        private void Program_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.H:
                    // H will open the help screen at all points during the game
                    // except when the splash screen is being displayed.
                    if (!State[ShowSplash] && !State[ShowHelp] 
                        && !State[ShowConfirmExit] && !State[ShowCredits]
                        && !State[ShowOutcome])
                    {
                        OpenHelpScreen();
                        break;
                    }
                    goto default; // This handles H used as any-key.
                case Keys.X:
                    // X will reset the current game when a game is running.
                    if (State[GameRunning] && !State[ShowHelp] 
                        && !State[ShowCredits] && !State[ShowConfirmExit]
                        && !State[ShowOutcome])
                    {
                        EndGame();
                        break;
                    }
                    goto default; // This handles X used as any-key.
                case Keys.C:
                    // C will shows the credits screen at all points during the
                    // game except when the splash screen is being displayed.
                    if(!State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowConfirmExit] && !State[ShowOutcome])
                    {
                        State[ShowCredits] = true;
                        break;
                    }
                    goto default; // This handles C used as any-key.
                case Keys.Escape:
                    // ESC will exit the game at all point during the game, except
                    // when a popup screen is showing. However, if a game is
                    // currently running the user will be asked to confirm exiting.
                    if (State[GameRunning] && !State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowCredits] && !State[ShowOutcome])
                    {
                        if (State[ShowConfirmExit])
                        {
                            this.Close();
                        }
                        else
                        {
                            ConfirmExit();
                            break;
                        }
                    }
                    else if (!State[ShowSplash] && !State[ShowHelp] && !State[ShowCredits]
                        && !State[ShowOutcome])
                    {
                        this.Close();
                    }
                    goto default; // This handles ESC used as any-key.
                case Keys.Up:
                    // UP only functions when the game isn't running. Without any
                    // modifier keys it will increase the row count. With the
                    // SHIFT modifer key it will increase the cell size, and with
                    // the CTRL modifier key it will increase the life chance.
                    if (!State[GameRunning] && !State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowCredits] && !State[ShowConfirmExit]
                        && !State[ShowOutcome])
                    {
                        // Handle modifier keys.
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY UP
                            ChangeRows(Increase);
                            break;
                        }
                        else if(e.Shift && !e.Control)
                        {
                            // SHIFT UP
                            ChangeCellSize(Increase);
                            break;
                        }
                        else if (!e.Shift && e.Control)
                        {
                            // CONTROL UP
                            ChangeLifeChance(Increase);
                            break;
                        }
                    }
                    goto default; // This handles UP used as any-key.
                case Keys.Down:
                    // DOWN only functions when the game isn't running. Without any
                    // modifier keys it will decrease the row count. With the
                    // SHIFT modifer key it will decrease the cell size, and with
                    // the CTRL modifier key it will decrease the life chance.
                    if (!State[GameRunning] && !State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowCredits] && !State[ShowConfirmExit]
                        && !State[ShowOutcome])
                    {
                        // Handle modifier keys.
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY DOWN
                            ChangeRows(Decrease);
                            break;
                        }
                        else if (e.Shift && !e.Control)
                        {
                            // SHIFT DOWN
                            ChangeCellSize(Decrease);
                            break;
                        }
                        else if (!e.Shift && e.Control)
                        {
                            // CONTROL DOWN
                            ChangeLifeChance(Decrease);
                            break;
                        }
                    }
                    goto default; // This handles DOWN used as any-key.
                case Keys.Left:
                    // LEFT only functions when the game isn't running. It will
                    // decrease the column count.
                    if (!State[GameRunning] && !State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowCredits] && !State[ShowConfirmExit]
                        && !State[ShowOutcome])
                    {
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY LEFT
                            ChangeColumns(Decrease);
                            break;
                        }
                    }
                    goto default; // This handles LEFT used as any-key.
                case Keys.Right:
                    // RIGHT only functions when the game isn't running. It will
                    // increase the column count.
                    if (!State[GameRunning] && !State[ShowSplash] && !State[ShowHelp]
                        && !State[ShowCredits] && !State[ShowConfirmExit]
                        && !State[ShowOutcome])
                    {
                        if (!e.Shift && !e.Control)
                        {
                            // ONLY RIGHT
                            ChangeColumns(Increase);
                            break;
                        }
                    }
                    goto default; // This handles RIGHT used as any-key.
                case Keys.Space:
                    // SPACE functions at all points during the game except when
                    // a popup screen is being displayed. It will step through
                    // generations of the game.
                    if (!State[ShowSplash] && !State[ShowHelp] && !State[ShowCredits]
                        && !State[ShowConfirmExit] && !State[ShowOutcome])
                    {
                        if (!State[GameRunning])
                        {
                            BeginGame();
                            break;
                        }
                        else if (State[GameRunning] && State[AutoStepping])
                        {
                            PauseAutoStep();
                            break;
                        }
                        else if (State[GameRunning] && !State[AutoStepping])
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
                    if (!State[ShowSplash] && !State[ShowHelp] && !State[ShowCredits]
                        && !State[ShowConfirmExit] && !State[ShowOutcome])
                    {
                        if (State[AutoStepping] && State[GameRunning])
                        {
                            PauseAutoStep();
                            break;
                        }
                        else if (!State[AutoStepping])
                        {
                            BeginAutoStep();
                            break;
                        }
                    }
                    goto default; // This handles ENTER used as any-key.
                default:
                    // Any other key that is pressed will close the splash screen,
                    // cancel an exit confirmation, exit the help screen, or be
                    // ignored.
                    if (State[ShowSplash])
                    {
                        State[ShowSplash] = false;
                        State[FirstDisplay] = true;
                    }
                    else if (State[ShowHelp])
                    {
                        State[ShowHelp] = false;
                    }
                    else if (State[ShowCredits])
                    {
                        State[ShowCredits] = false;
                    }
                    else if (State[ShowConfirmExit])
                    {
                        State[ShowConfirmExit] = false;
                    }
                    else if (State[ShowOutcome])
                    {
                        State[ShowOutcome] = false;
                    }
                    else
                    {
                        // Handle invalid key press
                        SystemSounds.Beep.Play(); // only works if users have sounds enabled
                    }
                    break;
            }

            // After handling a key press, our display almost certainly needs
            // to be refreshed. We'll force a Paint event.
            this.Invalidate();
        }
    }
}

