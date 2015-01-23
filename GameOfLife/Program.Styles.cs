
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
    partial class Program
    {

        


        /// <summary>
        /// This is an array to store the colors for the various types of
        /// life. Although the names may sound similar to colors that are
        /// already built in to the Color class, they do not share actual
        /// RGB values.
        /// </summary>
        static Color[] lifeColor = new Color[]
        {
            Color.FromArgb(206,215,219), //  0 - Background (Blue Grey)
            Color.FromArgb(155, 38,175), //  1 - Purple
            Color.FromArgb(102, 57,182), //  2 - Deep Purple
            Color.FromArgb( 62, 80,180), //  3 - Indigo
            Color.FromArgb( 32,149,242), //  4 - Blue
            Color.FromArgb(  2,168,243), //  5 - Light Blue
            Color.FromArgb(  0,187,211), //  6 - Cyan
            Color.FromArgb(  0,149,135), //  7 - Teal
            Color.FromArgb( 75,174, 79), //  8 - Green
            Color.FromArgb(138,194, 73), //  9 - Light Green
            Color.FromArgb(204,219, 56), // 10 - Lime
            Color.FromArgb(255,234, 58), // 11 - Yellow
            Color.FromArgb(255,192,  6), // 12 - Amber
            Color.FromArgb(255,151,  0), // 13 - Orange
            Color.FromArgb(255, 86, 33), // 14 - Deep Orange
            Color.FromArgb(243, 66, 53), // 15 - Red
            Color.FromArgb(232, 29, 98)  // 16 - Pink
        };

        // Create solid brushes for each of the colors.
        // This could be performed in a for loop if we didn't
        // want the variable to be static.
        static SolidBrush[] lifeBrush = new SolidBrush[]
        {
            new SolidBrush(lifeColor[0]),
            new SolidBrush(lifeColor[1]),
            new SolidBrush(lifeColor[2]),
            new SolidBrush(lifeColor[3]),
            new SolidBrush(lifeColor[4]),
            new SolidBrush(lifeColor[5]),
            new SolidBrush(lifeColor[6]),
            new SolidBrush(lifeColor[7]),
            new SolidBrush(lifeColor[8]),
            new SolidBrush(lifeColor[9]),
            new SolidBrush(lifeColor[10]),
            new SolidBrush(lifeColor[11]),
            new SolidBrush(lifeColor[12]),
            new SolidBrush(lifeColor[13]),
            new SolidBrush(lifeColor[14]),
            new SolidBrush(lifeColor[15]),
            new SolidBrush(lifeColor[16])
        };
        
        

        /// <summary>
        /// This is an array to store various other colors used throughout
        /// the GUI.
        /// </summary>
        static Color[] uiColor = new Color[]
        {
            Color.FromArgb(  0,  0,  0), //  0 - Black
            Color.FromArgb(206,215,219), //  1 - Background
            Color.FromArgb(143,163,173), //  2 - Darker Background
            Color.FromArgb(175,189,196), //  3 - Cell Border
            Color.FromArgb(150, 0, 0, 0)  //  4 - Black Shade
        };

        static Pen cellPen = new Pen(uiColor[3], 1);
        static Pen darkCellPen = new Pen(uiColor[2], 1);
        
        static SolidBrush blackBrush = new SolidBrush(uiColor[0]);
        static SolidBrush backgroundBrush = new SolidBrush(uiColor[1]);
        static SolidBrush darkBackgroundBrush = new SolidBrush(uiColor[2]);
        static SolidBrush shadeBrush = new SolidBrush(uiColor[4]);

        private PrivateFontCollection customFonts;
        private Font promptFont;
        private Font subPromptFont;
        private Font generationTitleFont;
        private Font generationValueFont;
        private Font gridSettingTitleFont;
        private Font gridSettingValueFont;


        private void InitializeStyles()
        {
            cellPen.LineJoin = LineJoin.Bevel;


            customFonts = new PrivateFontCollection();

            // Load custom fonts.
            customFonts.AddFontFile("GameOfLife-Regular.ttf");
            customFonts.AddFontFile("GameOfLife-Bold.ttf");

            promptFont = new Font(customFonts.Families[0], 16, FontStyle.Bold);
            subPromptFont = new Font(customFonts.Families[0], 12, FontStyle.Regular);
            generationTitleFont = new Font(customFonts.Families[0], 20, FontStyle.Bold);
            generationValueFont = new Font(customFonts.Families[0], 20, FontStyle.Regular);
            gridSettingTitleFont = new Font(customFonts.Families[0], 14, FontStyle.Bold);
            gridSettingValueFont = new Font(customFonts.Families[0], 14, FontStyle.Regular);
        }
    }
}
