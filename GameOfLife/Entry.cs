/*
 * The Game of Life - Marc King
 * Programmed for CIS275 - Winter 2015
 * 
 * Entry.cs
 * 
 * Contains the Main() of the program. It is only responsible for loading the
 * Windows form that will handle the rest of the program flow.
 * 
 */

using System;
using System.Windows.Forms;

namespace GameOfLife
{
    static class Entry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Program());
        }
    }
}
