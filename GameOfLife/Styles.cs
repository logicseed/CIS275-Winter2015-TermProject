/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GameOfLife
{
    internal static class Style
    {

        #region Public Members

        // Various values used through the program.
        public static int ElementMargin = 10;
        public static int ElementSpacing = 5;
        public static int TextMargin = 20;
        public static int TextPadding = 10;
        public static int TextSpacing = 10;
        public static int PopupMargin = 0;
        public static int PopupPadding = 20;
        public static int PopupSpacing = 5;

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

        // The Brush array used to draw the life rectangles
        public static SolidBrush[] LifeBrush = new SolidBrush[LifeColor.Length];

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
            Color.FromArgb(100, 0, 0, 0)  //  4 - Black Shade
        };

        // Brushes used to draw the various screen elements
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
        public static SolidBrush OutcomePopupOscillationTextColor = new SolidBrush(LifeColor[3]);
        public static SolidBrush CreditsPopupTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush CreditsPopupSectionTextColor = new SolidBrush(LifeColor[14]);
        public static SolidBrush CreditsPopupItemTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush IntroductionPopupTitleTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush IntroductionPopupItemTextColor = new SolidBrush(UIColor[0]);
        public static SolidBrush IntroductionArrowTextColor = new SolidBrush(LifeColor[12]);
        public static SolidBrush PressAnyKeyTextColor = new SolidBrush(LifeColor[2]);

        // The grid is darker when a game isn't running to make it easier to modify
        // the grid settings. Once a game starts the grid lightens to give the
        // life rectangles more prominence.
        public static Pen GridCellPen = new Pen(UIColor[3], 1);
        public static Pen GridDarkCellPen = new Pen(UIColor[2], 1);

        // The fonts used by various screen elements.
        public static PrivateFontCollection[] RobotoFontCollection;

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
        public static Font IntroductionPopupTitleFont;
        public static Font IntroductionPopupItemFont;
        public static Font IntroductionArrowTextFont;
        public static Font PopupSpacerFont;
        public static Font PressAnyKeyFont;

        #endregion Public Members

        #region Private Members

        // Used during the initialization of fonts to make it easier to specify
        // which font a particular element uses.
        private static FontFamily Roboto;
        private static FontFamily RobotoThin;
        private static FontFamily RobotoLight;
        private static FontFamily RobotoMedium;
        private static FontFamily RobotoBlack;
        private static FontFamily RobotoCondensed;
        private static FontFamily RobotoCondensedLight;
        private static FontFamily RobotoKeys;

        #endregion Private Members

        #region Initialize and Destruct

        /// <summary>
        /// Initializes the Style class for use in the program.
        /// </summary>
        public static void Initialize()
        {
            InitializeColors();
            InitializeRobotoFonts();
            InitializeFonts();
        }

        /// <summary>
        /// Fills the LifeBrush array with colors for use in drawing the life
        /// rectangles.
        /// </summary>
        private static void InitializeColors()
        {
            // Create brush for use in painting the life squares.
            for (int i = 0; i < LifeColor.Length; i++)
            {
                LifeBrush[i] = new SolidBrush(LifeColor[i]);
            }
        }

        /// <summary>
        /// Sets the font properties for the fonts used by various screen elements.
        /// </summary>
        private static void InitializeFonts()
        {

            SplashScreenFont = new Font(RobotoBlack, 16, FontStyle.Bold);

            HelpPromptFont = new Font(RobotoKeys, 16, FontStyle.Bold);

            GenerationCountTitleFont = new Font(RobotoCondensed, 20, FontStyle.Bold);

            GenerationCountValueFont = new Font(RobotoCondensed, 20, FontStyle.Regular);

            GridSettingsTitleFont = new Font(RobotoCondensed, 14, FontStyle.Bold);

            GridSettingsValueFont = new Font(RobotoCondensed, 14, FontStyle.Regular);

            HelpPopupTitleFont = new Font(Roboto, 16, FontStyle.Bold);

            HelpPopupSectionFont = new Font(Roboto, 12, FontStyle.Regular);

            HelpPopupItemFont = new Font(RobotoKeys, 14, FontStyle.Regular);

            ExitConfirmationTitleFont = new Font(RobotoKeys, 16, FontStyle.Bold);

            ExitConfirmationSectionFont = new Font(RobotoBlack, 12, FontStyle.Regular);

            OutcomePopupFont = new Font(Roboto, 20, FontStyle.Bold);

            CreditsPopupTitleFont = new Font(Roboto, 18, FontStyle.Bold);

            CreditsPopupSectionFont = new Font(Roboto, 12, FontStyle.Regular);

            CreditsPopupItemFont = new Font(Roboto, 14, FontStyle.Bold);

            IntroductionPopupTitleFont = new Font(Roboto, 18, FontStyle.Bold);

            IntroductionPopupItemFont = new Font(Roboto, 14, FontStyle.Regular);

            IntroductionArrowTextFont = new Font(RobotoBlack, 16, FontStyle.Regular);

            PopupSpacerFont = new Font(Roboto, 10, FontStyle.Regular);

            PressAnyKeyFont = new Font(RobotoBlack, 12, FontStyle.Bold);

        }

        /// <summary>
        /// Loads all the Roboto and custom Roboto fonts into memory and then
        /// creates FontFamily objects for them.
        /// </summary>
        private static void InitializeRobotoFonts()
        {
            // Due to a limitation in PrivateFontCollection, we need to use
            // an array from PrivateFontCollections in order to have consistent
            // access to our font families.

            // We plan to have eight font families.
            RobotoFontCollection = new PrivateFontCollection[RobotoFamily.TotalFamilies];

            // Variable to store the file names for each family.
            string[] FontFiles;

            // Roboto
            FontFiles = new string[] {
                "Roboto-Regular.ttf",
                "Roboto-Italic.ttf",
                "Roboto-Bold.ttf",
                "Roboto-BoldItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Regular] = LoadFontFamily(FontFiles);

            // Roboto Thin
            FontFiles = new string[] {
                "Roboto-Thin.ttf",
                "Roboto-ThinItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Thin] = LoadFontFamily(FontFiles);

            // Roboto Light
            FontFiles = new string[] {
                "Roboto-Light.ttf",
                "Roboto-LightItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Light] = LoadFontFamily(FontFiles);

            // Roboto Medium
            FontFiles = new string[] {
                "Roboto-Medium.ttf",
                "Roboto-MediumItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Medium] = LoadFontFamily(FontFiles);

            // Roboto Black
            FontFiles = new string[] {
                "Roboto-Black.ttf",
                "Roboto-BlackItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Black] = LoadFontFamily(FontFiles);

            // Roboto Condensed
            FontFiles = new string[] {
                "RobotoCondensed-Regular.ttf",
                "RobotoCondensed-Italic.ttf",
                "RobotoCondensed-Bold.ttf",
                "RobotoCondensed-BoldItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Condensed] = LoadFontFamily(FontFiles);

            // Roboto Condensed Light
            FontFiles = new string[] {
                "RobotoCondensed-Light.ttf",
                "RobotoCondensed-LightItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.CondensedLight] = LoadFontFamily(FontFiles);

            // Roboto Keys
            FontFiles = new string[] {
                "Roboto-Keys-Regular.ttf",
                "Roboto-Keys-Italic.ttf",
                "Roboto-Keys-Bold.ttf",
                "Roboto-Keys-BoldItalic.ttf"
            };
            RobotoFontCollection[RobotoFamily.Keys] = LoadFontFamily(FontFiles);

            // Font Families
            Roboto = RobotoFontCollection[RobotoFamily.Regular].Families[0];
            RobotoThin = RobotoFontCollection[RobotoFamily.Thin].Families[0];
            RobotoLight = RobotoFontCollection[RobotoFamily.Light].Families[0];
            RobotoMedium = RobotoFontCollection[RobotoFamily.Medium].Families[0];
            RobotoBlack = RobotoFontCollection[RobotoFamily.Black].Families[0];
            RobotoCondensed = RobotoFontCollection[RobotoFamily.Condensed].Families[0];
            RobotoCondensedLight = RobotoFontCollection[RobotoFamily.CondensedLight].Families[0];
            RobotoKeys = RobotoFontCollection[RobotoFamily.Keys].Families[0];
        }

        #endregion Initialize and Destruct

        #region Private Interface

        /// <summary>
        /// Will load a family of fonts and store them in a PrivateFontCollection
        /// if they are located in the Fonts folder.
        /// </summary>
        /// <param name="FontFiles">An array of strings for the font files names.</param>
        /// <returns>A PrivateFontCollection containing the loaded font family.</returns>
        private static PrivateFontCollection LoadFontFamily(string[] FontFiles)
        {
            PrivateFontCollection FontCollection = new PrivateFontCollection();

            for (int i = 0; i < FontFiles.Length; i++)
            {
                string FontFile = typeof(Program).Namespace + ".Fonts." + FontFiles[i];
                Stream FontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FontFile);

                // create an unsafe memory block for the font data
                System.IntPtr Data = Marshal.AllocCoTaskMem((int)FontStream.Length);

                // create a buffer to read in to
                byte[] FontData = new byte[FontStream.Length];

                // read the font data from the resource
                FontStream.Read(FontData, 0, (int)FontStream.Length);

                // copy the bytes to the unsafe memory block
                Marshal.Copy(FontData, 0, Data, (int)FontStream.Length);

                // pass the font to the font collection
                FontCollection.AddMemoryFont(Data, (int)FontStream.Length);

                // close the resource stream
                FontStream.Close();

                // free up the unsafe memory
                Marshal.FreeCoTaskMem(Data);
            }

            return FontCollection;
        }

        /// <summary>
        /// Used to easily reference the font families by name.
        /// This static class is used to overcome a limitation in enum that will
        /// not allow it to be automatically cast to an integer.
        /// </summary>
        private static class RobotoFamily
        {
            public static int Regular = 0;
            public static int Thin = 1;
            public static int Light = 2;
            public static int Medium = 3;
            public static int Black = 4;
            public static int Condensed = 5;
            public static int CondensedLight = 6;
            public static int Keys = 7;
            public static int TotalFamilies = 8;
        }

        #endregion Private Interface

    }
}
