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
        

        

        // Matrix fields
        private int rows = 20;
        private int columns = 30;
        
        private byte lifeChance = 50;

        

        private LifeManager lifeManager = new LifeManager();

        

        

       

        

        public Program()
        {
            InitializeComponent();
            Cursor.Hide();
            InitializeStyles();
            //this.SetStyle(ControlStyles.DoubleBuffer, true);


            

            this.BackColor = uiColor[1];

        }

        private void ConfirmExit()
        {
            isConfirmingExit = true;
        }

        private void CancelExit()
        {
            isConfirmingExit = false;
        }

        private void PauseAutoStep()
        {

        }

        private void BeginAutoStep()
        {

        }

        private void CloseSplashScreen()
        {
            hasShownSplash = true;
        }

        private void OpenHelpScreen()
        {

        }

        private void NextStep()
        {
            lifeManager.NextGeneration();
        }

        private void BeginGame()
        {
            lifeManager.CreateNewMatrix(rows, columns);
            lifeManager.RandomizeMatrix(lifeChance);
        }

        private void ChangeRows(bool Change)
        {
            if(Change == Increase)
            {
                rows++;
            }
            else
            {
                rows--;
            }
            InvalidateGrid();
        }

        private void ChangeColumns(bool Change)
        {
            if (Change == Increase)
            {
                columns++;
            }
            else
            {
                columns--;
            }
            InvalidateGrid();
        }

        private void ChangeCellSize(bool Change)
        {
            if (Change == Increase)
            {
                cellSize++;
            }
            else
            {
                cellSize--;
            }
            InvalidateGrid();
        }

        private void ChangeLifeChance(bool Change)
        {
            if (Change == Increase)
            {
                lifeChance++;
            }
            else
            {
                lifeChance--;
            }
        }

        private int CalculateMaxRows()
        {
            //private SizeF totalLogoSize;
        //private SizeF totalHelpPromptSize;
        //private SizeF totalGenerationCountSize;
        //private SizeF totalSettingsSize;
            return 20;
        }

        private int CalculateMaxColumns()
        {
            return 20;
        }

        private int CalculateMaxCellSize()
        {
            return 20;
        }
    }
}
