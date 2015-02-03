/*
 * The Game of Life
 * 
 * The Game of Life is a simulation program that simulates the rules designed
 * by John Horton Conway. It was written as a class project by Marc King for
 * CIS275 Discrete Structures I - Winter 2015 at the University of Michigan -
 * Dearborn.
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
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
            // The sole function of this method is to create and view the main
            // Windows form.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Program());
        }
    }
}
