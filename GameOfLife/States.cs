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
        /// The game outcome popup is being displayed.
        /// </summary>
        Introduction,
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
}
