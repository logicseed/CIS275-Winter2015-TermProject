/*
 * TODO
 * 
 * Cleanup usings
 * Refactor
 * Check for efficiency changes
 * 
 * IF TIME
 * 
 * make fonts embedded resources
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
using System.Reflection;

namespace GameOfLife
{
    internal static class Style
    {
        /// <summary>
        /// This is an array to store the colors for the various types of
        /// life. Although the names may sound similar to colors that are
        /// already built in to the Color class, they do not share actual
        /// RGB values.
        /// </summary>
        public static Color[] LifeColor = new Color[]
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
        public static SolidBrush[] LifeBrush = new SolidBrush[]
        {
            new SolidBrush(LifeColor[0]),
            new SolidBrush(LifeColor[1]),
            new SolidBrush(LifeColor[2]),
            new SolidBrush(LifeColor[3]),
            new SolidBrush(LifeColor[4]),
            new SolidBrush(LifeColor[5]),
            new SolidBrush(LifeColor[6]),
            new SolidBrush(LifeColor[7]),
            new SolidBrush(LifeColor[8]),
            new SolidBrush(LifeColor[9]),
            new SolidBrush(LifeColor[10]),
            new SolidBrush(LifeColor[11]),
            new SolidBrush(LifeColor[12]),
            new SolidBrush(LifeColor[13]),
            new SolidBrush(LifeColor[14]),
            new SolidBrush(LifeColor[15]),
            new SolidBrush(LifeColor[16])
        };

        /// <summary>
        /// This is an array to store various other colors used throughout
        /// the GUI.
        /// </summary>
        public static Color[] UIColor = new Color[]
        {
            Color.FromArgb(  0,  0,  0),  //  0 - Black
            Color.FromArgb(206,215,219),  //  1 - Background
            Color.FromArgb(143,163,173),  //  2 - Darker Background
            Color.FromArgb(175,189,196),  //  3 - Cell Border
            Color.FromArgb(150, 0, 0, 0)  //  4 - Black Shade
        };

        public static SolidBrush SplashScreenBackgroundColor = new SolidBrush(UIColor[2]);
        public static SolidBrush SplashScreenTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush MainScreenBackgroundColor = new SolidBrush(UIColor[1]);
        public static SolidBrush HelpPromptTextColor = new SolidBrush(UIColor[4]);
        public static SolidBrush GenerationCountTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush GenerationCountValueTextColor = new SolidBrush(LifeColor[14]);
        public static SolidBrush GridSettingsTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush GridSettingsValueTextColor = new SolidBrush(LifeColor[3]);
        public static SolidBrush PopupBackgroundColor = new SolidBrush(UIColor[1]);
        public static SolidBrush PopupShadingColor = new SolidBrush(UIColor[4]);
        public static SolidBrush PopupSpacerColor = new SolidBrush(UIColor[0]);
        public static SolidBrush HelpPopupTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush HelpPopupSectionTextColor = new SolidBrush(LifeColor[4]);
        public static SolidBrush HelpPopupItemTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush ExitConfirmationTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush ExitConfirmationSectionTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush OutcomePopupExtinctionTextColor = new SolidBrush(LifeColor[15]);
        public static SolidBrush OutcomePopupStabilizationTextColor = new SolidBrush(LifeColor[8]);
        public static SolidBrush CreditsPopupTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush CreditsPopupSectionTextColor = new SolidBrush(LifeColor[14]);
        public static SolidBrush CreditsPopupItemTextColor = new SolidBrush(UIColor[0]);


        public static int ElementMargin = 10;
        public static int ElementSpacing = 5;
        public static int TextMargin = 20;
        public static int TextPadding = 10;
        public static int TextSpacing = 10;
        public static int PopupMargin = 0;
        public static int PopupPadding = 20;
        public static int PopupSpacing = 5;

        private static PrivateFontCollection CustomFonts;
        public static Font SplashScreenFont;
        public static Font HelpPromptFont;
        public static Font GenerationCountTitleFont;
        public static Font GenerationCountValueFont;
        public static Font GridSettingsTitleFont;
        public static Font GridSettingsValueFont;
        public static Font HelpPopupTitleFont;
        public static Font HelpPopupSectionFont;
        public static Font HelpPopupItemFont;
        public static Font ExitConfirmationTitleFont;
        public static Font ExitConfirmationSectionFont;
        public static Font OutcomePopupFont;
        public static Font CreditsPopupTitleFont;
        public static Font CreditsPopupSectionFont;
        public static Font CreditsPopupItemFont;
        public static Font PopupSpacerFont;

        public static Pen GridCellPen = new Pen(UIColor[3], 1);
        public static Pen GridDarkCellPen = new Pen(UIColor[2], 1);


        public static void Initialize(Form Parent)
        {
            CustomFonts = new PrivateFontCollection();

            Stream FontStream = Parent.GetType().Assembly.GetManifestResourceStream("GameOfLife.GameOfLife-Regular.ttf");
            byte[] FontData = new byte[FontStream.Length];
            FontStream.Read(FontData, 0, (int)FontStream.Length);
            FontStream.Close();
            unsafe
            {
                fixed (byte* pFontData = FontData)
                {
                    CustomFonts.AddMemoryFont((System.IntPtr)pFontData, FontData.Length);
                }
            }
            FontStream = Parent.GetType().Assembly.GetManifestResourceStream("GameOfLife.GameOfLife-Bold.ttf");
            FontData = new byte[FontStream.Length];
            FontStream.Read(FontData, 0, (int)FontStream.Length);
            FontStream.Close();
            unsafe
            {
                fixed (byte* pFontData = FontData)
                {
                    CustomFonts.AddMemoryFont((System.IntPtr)pFontData, FontData.Length);
                }
            }





            // Load custom fonts.
            //CustomFonts.AddFontFile("GameOfLife-Regular.ttf");
            //CustomFonts.AddFontFile("GameOfLife-Bold.ttf");

            InitializeFonts();
        }

        private static void InitializeFonts()
        {
            SplashScreenFont = new Font(
                CustomFonts.Families[0],
                16,
                FontStyle.Bold
            );

            HelpPromptFont = new Font(
                CustomFonts.Families[0],
                16,
                FontStyle.Bold
            );

            GenerationCountTitleFont = new Font(
                CustomFonts.Families[0], 
                20, 
                FontStyle.Bold
            );
            
            GenerationCountValueFont = new Font(
                CustomFonts.Families[0], 
                20, 
                FontStyle.Regular
            );

            GridSettingsTitleFont = new Font(
                CustomFonts.Families[0], 
                14, 
                FontStyle.Bold
            );
            
            GridSettingsValueFont = new Font(
                CustomFonts.Families[0], 
                14, 
                FontStyle.Regular
            );

            HelpPopupTitleFont = new Font(
                CustomFonts.Families[0], 
                16, 
                FontStyle.Bold
            );

            HelpPopupSectionFont = new Font(
                CustomFonts.Families[0], 
                12, 
                FontStyle.Regular
            );

            HelpPopupItemFont = new Font(
                CustomFonts.Families[0], 
                14, 
                FontStyle.Regular
            );

            ExitConfirmationTitleFont = new Font(
                CustomFonts.Families[0],
                16,
                FontStyle.Bold
            );

            ExitConfirmationSectionFont = new Font(
                CustomFonts.Families[0],
                12,
                FontStyle.Regular
            );

            OutcomePopupFont = new Font(
                CustomFonts.Families[0],
                20,
                FontStyle.Bold
            );

            CreditsPopupTitleFont = new Font(
                CustomFonts.Families[0],
                18,
                FontStyle.Bold
            );

            CreditsPopupSectionFont = new Font(
                CustomFonts.Families[0],
                12,
                FontStyle.Regular
            );

            CreditsPopupItemFont = new Font(
                CustomFonts.Families[0],
                14,
                FontStyle.Bold
            );

            PopupSpacerFont = new Font(
                CustomFonts.Families[0],
                10,
                FontStyle.Regular
            );
        }
    }
}
