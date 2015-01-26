/*
 * TODO
 * 
 * cleanup usings
 * integrate graphics states
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
using System.Collections.Specialized;

namespace GameOfLife
{
    /// <summary>
    /// Stores the states of the program. Does not require instantiation.
    /// 
    /// Example usage: State.Screen == ScreenState.Splash
    /// </summary>
    /// <remarks>Still need to change code to start using this</remarks>
    internal static class State
    {
        /// <summary>
        /// The current screen state of the program. Defaults to the splash
        /// screen; the first screen of the program.
        /// </summary>
        public static ScreenState Screen = ScreenState.Splash;
        /// <summary>
        /// The current popup state of the program. Defaults to no popup being
        /// displayed.
        /// </summary>
        public static PopupState Popup = PopupState.None;
    }

    /// <summary>
    /// The mutually exclusive screen states that the program can have.
    /// </summary>
    /// <remarks>Still need to convert the code to use this state handler.</remarks>
    internal enum ScreenState
    {
        /// <summary>
        /// Showing the splash screen.
        /// </summary>
        Splash,
        /// <summary>
        /// Showing the main screen for the very first time, this causes the
        /// screen to be built twice so that the initial grid size can be
        /// maximized.
        /// </summary>
        FirstDisplay,
        /// <summary>
        /// Showing the main screen without a game in memory.
        /// </summary>
        NoGame,
        /// <summary>
        /// Showing the main screen while a game is running.
        /// </summary>
        GameRunning,
        /// <summary>
        /// Showing the main screen after a game has ended but not been reset.
        /// </summary>
        GameStopped,
    }

    /// <summary>
    /// The mutually exclusive popups that can be displayed.
    /// </summary>
    /// <remarks>Still need to make the code use these states.</remarks>
    internal enum PopupState
    {
        /// <summary>
        /// The program is not displaying a popup.
        /// </summary>
        None,
        /// <summary>
        /// The help popup is being displayed.
        /// </summary>
        Help,
        /// <summary>
        /// The credits popup is being displayed.
        /// </summary>
        Credits,
        /// <summary>
        /// The exit confirmation popup is being displayed.
        /// </summary>
        ExitConfirmation,
        /// <summary>
        /// The game outcome popup is being displayed.
        /// </summary>
        Outcome
    }









    /*
     * ################### REMOVE AFTER CHANGING STATE STRUCTURE ############
     */
    partial class Program
    {
        /// <summary>
        /// This bit vector holds a bit string of states for the program.
        /// States are primarily used to indicate to the program which
        /// interface elements to paint and which keys to accept.
        /// </summary>
        private BitVector32 State = new BitVector32(0);

        // Creates masks for each of the state bit flags
        private static int GameRunning = BitVector32.CreateMask();
        private static int AutoStepping = BitVector32.CreateMask(GameRunning);
        private static int ShowSplash = BitVector32.CreateMask(AutoStepping);
        private static int ShowConfirmExit = BitVector32.CreateMask(ShowSplash);
        private static int ShowHelp = BitVector32.CreateMask(ShowConfirmExit);
        private static int ShowCredits = BitVector32.CreateMask(ShowHelp);
        private static int FirstDisplay = BitVector32.CreateMask(ShowCredits);
        private static int GameComplete = BitVector32.CreateMask(FirstDisplay);
        private static int ShowOutcome = BitVector32.CreateMask(GameComplete);

        private void InitializeStates()
        {
            State[ShowSplash] = true;
        }
    }
    /*
     * ################### REMOVE AFTER CHANGING STATE STRUCTURE ############
     */
}
