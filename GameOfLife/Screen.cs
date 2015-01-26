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
    /// Maintains a screen buffer that it will paint on until the program requests the buffer.
    /// Screen.
    /// 
    /// Initialize() needs to be called after the program knows how big the screen is.
    /// </summary>
    internal static class Screen
    {
        private static Graphics Painter;
        private static Bitmap Buffer;
        private static Size BufferSize;

        private static SizeF LogoSize;
        private static SizeF HelpPromptSize;
        private static SizeF GenerationCountSize;
        private static SizeF GridSettingsSize;

        private static PointF LogoPosition;
        private static PointF HelpPromptPosition;
        private static PointF GenerationCountTitlePosition;
        private static PointF GenerationCountValuePosition;
        private static PointF RowCountTitlePosition;
        private static PointF RowCountValuePosition;
        private static PointF ColumnCountTitlePosition;
        private static PointF ColumnCountValuePosition;
        private static PointF CellSizeTitlePosition;
        private static PointF CellSizeValuePosition;
        private static PointF LifeChanceTitlePosition;
        private static PointF LifeChanceValuePosition;

        /// <summary>
        /// Initializes the class to begin painting on the buffer.
        /// </summary>
        /// <param name="ScreenSize">The pixel size of the buffer.</param>
        public static void Initialize(Size ScreenSize)
        {
            BufferSize = ScreenSize;
            Buffer = new Bitmap(BufferSize.Width, BufferSize.Height);
            Painter = Graphics.FromImage(Buffer);
            Painter.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        /// <summary>
        /// Returns the current screen buffer and then resets it in preparation for future drawing.
        /// </summary>
        public static Bitmap GetBuffer()
        {
            Bitmap BufferToReturn = Buffer;
            Initialize(BufferSize);
            return BufferToReturn;
        }

        /// <summary>
        /// Draws the screen based on current program states.
        /// </summary>
        public static void DrawScreen()
        {
            // Draw the appropriate screen.
            switch(State.Screen)
            {
                case ScreenState.Splash:
                    DrawSplashScreen();
                    break;
                case ScreenState.FirstDisplay:
                    DrawFirstMainScreen();
                    break;
                default:
                    DrawMainScreen();
                    break;
            }

            // Draw a popup if one is being displayed.
            switch(State.Popup)
            {
                case PopupState.Help:
                    DrawHelpPopup();
                    break;
                case PopupState.Credits:
                    DrawCreditsPopup();
                    break;
                case PopupState.ExitConfirmation:
                    DrawExitConfirmationPopup();
                    break;
                case PopupState.Outcome:
                    DrawOutcomePopup();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Draws the splash screen to the buffer.
        /// </summary>
        private static void DrawSplashScreen()
        {
            // Draw background.
            Painter.FillRectangle(
                Style.SplashScreenBackgroundColor,
                0, 0,
                BufferSize.Width,
                BufferSize.Height
            );

            // Draw splash screen graphic.
            Bitmap SplashImage = new Bitmap(Properties.Resources.Splash);
            Painter.DrawImage(
                SplashImage,
                (BufferSize.Width - SplashImage.Width) / 2,
                (BufferSize.Height - SplashImage.Height) / 2
            );
            SplashImage.Dispose();

            // Draw press-any-key text.
            String text = Properties.Resources.SplashScreen;
            SizeF MessageSize = Painter.MeasureString(
                Properties.Resources.PressAnyKey,
                Style.SplashScreenFont
            );

            Painter.DrawString(
                Properties.Resources.PressAnyKey,
                Style.SplashScreenFont, 
                Style.SplashScreenTextColor,
                (BufferSize.Width - MessageSize.Width) / 2,
                (BufferSize.Height - MessageSize.Height)
            );
        }

        /// <summary>
        /// Draws the main screen to the buffer for the very first time. This
        /// allows the screen elements to be measured so that the initial grid
        /// size can be calculated. The main screen is then redrawn using the
        /// calculated grid size.
        /// </summary>
        private static void DrawFirstMainScreen()
        {
            // Because we're not actually keeping this screen, we don't need
            // to draw all of the elements, only the ones we need measured.
            MeasureLogo();
            MeasureHelpPrompt();
            MeasureGenerationCount();
            MeasureGridSettings();

            SetDefaultGridSize();

            State.Screen = ScreenState.NoGame;
            
            DrawMainScreen();
        }

        /// <summary>
        /// Draws the main screen to the buffer.
        /// </summary>
        private static void DrawMainScreen()
        {
            Painter.FillRectangle(
                Style.MainScreenBackgroundColor, 
                0, 0,
                BufferSize.Width, 
                BufferSize.Height
            );

            DrawLogo();
            DrawHelpPrompt();
            DrawGenerationCount();
            DrawGridSettings();
            DrawGrid();

            if (State.Screen == ScreenState.GameRunning ||
                State.Screen == ScreenState.GameStopped)
            {
                DrawLife();
            }
        }

        /// <summary>
        /// Measures the game logo to speed up future calculations.
        /// </summary>
        private static void MeasureLogo()
        {
            Bitmap LogoImage = new Bitmap(Properties.Resources.Logo);
            LogoSize = new SizeF(
                LogoImage.Width,
                LogoImage.Height + Style.ElementMargin
            );
            LogoImage.Dispose();
        }

        /// <summary>
        /// Measures the help prompt text to speed up future calculations.
        /// </summary>
        private static void MeasureHelpPrompt()
        {
            HelpPromptSize = Painter.MeasureString(
                Properties.Resources.HelpPrompt,
                Style.HelpPromptFont
            );
            HelpPromptSize.Height += Style.ElementMargin * 2;
            HelpPromptSize.Width += Style.ElementMargin * 2;
        }

        /// <summary>
        /// Measures the generation count text to speed up future calculations.
        /// </summary>
        private static void MeasureGenerationCount()
        {
            // Measure generation count title
            SizeF GenerationCountTitleSize = Painter.MeasureString(
                Properties.Resources.GenerationTitle,
                Style.GenerationCountTitleFont
            );

            // Measure generation count value
            SizeF GenerationCountValueSize = Painter.MeasureString(
                "888888888",
                Style.GenerationCountValueFont
            );

            // Calculate positions
            GenerationCountTitlePosition = new PointF(
                Style.ElementMargin * 2,
                BufferSize.Height - GenerationCountTitleSize.Height - Style.ElementMargin
            );
            GenerationCountValuePosition = new PointF(
                (Style.ElementMargin * 2) + GenerationCountTitleSize.Width,
                GenerationCountTitlePosition.Y
            );

            // Record total size for determining maximum grid values
            GenerationCountSize = new SizeF(
                GenerationCountTitleSize.Width + GenerationCountValueSize.Width
                    + (Style.ElementMargin * 4),
                Math.Max(GenerationCountTitleSize.Height, GenerationCountValueSize.Height)
                    + (Style.ElementMargin * 4)
            );
        }

        /// <summary>
        /// Measures the grid settings text to speed up future calculations.
        /// </summary>
        private static void MeasureGridSettings()
        {
                // Measure grid settings titles
                SizeF RowCountTitleSize = Painter.MeasureString(
                    Properties.Resources.RowCountTitle,
                    Style.GridSettingsTitleFont
                );
                SizeF ColumnCountTitleSize = Painter.MeasureString(
                    Properties.Resources.ColumnCountTitle,
                    Style.GridSettingsTitleFont
                );
                SizeF CellSizeTitleSize = Painter.MeasureString(
                    Properties.Resources.CellSizeTitle,
                    Style.GridSettingsTitleFont
                );
                SizeF LifeChanceTitleSize = Painter.MeasureString(
                    Properties.Resources.LifeChanceTitle,
                    Style.GridSettingsTitleFont
                );

                // Measure generation count value
                SizeF RowCountValueSize = Painter.MeasureString(
                    "8888 (" + Properties.Resources.Max + " 8888)",
                    Style.GridSettingsValueFont
                );
                SizeF ColumnCountValueSize = Painter.MeasureString(
                    "8888 (" + Properties.Resources.Max + " 8888)",
                    Style.GridSettingsValueFont
                );
                SizeF CellSizeValueSize = Painter.MeasureString(
                    "8888px (" + Properties.Resources.Max + " 8888px)",
                    Style.GridSettingsValueFont
                );
                SizeF LifeChanceValueSize = Painter.MeasureString(
                    "888%",
                    Style.GridSettingsValueFont
                );

                // Calculate positions
                RowCountTitlePosition = new PointF(
                    BufferSize.Width -
                        (Math.Max(RowCountTitleSize.Width + RowCountValueSize.Width, 
                            ColumnCountTitleSize.Width + ColumnCountValueSize.Width) +
                        Style.ElementSpacing +
                        Math.Max(CellSizeTitleSize.Width, LifeChanceTitleSize.Width) +
                        Math.Max(CellSizeValueSize.Width, LifeChanceValueSize.Width) +
                        Style.ElementMargin),
                    BufferSize.Height -
                        (Math.Max(
                            Math.Max(RowCountTitleSize.Height, RowCountValueSize.Height),
                            Math.Max(CellSizeTitleSize.Height, CellSizeValueSize.Height)) +
                        Style.ElementSpacing +
                        Math.Max(
                            Math.Max(ColumnCountTitleSize.Height, ColumnCountValueSize.Height),
                            Math.Max(LifeChanceTitleSize.Height, LifeChanceValueSize.Height)) +
                        Style.ElementMargin)
                );
                RowCountValuePosition = new PointF(
                    RowCountTitlePosition.X + RowCountTitleSize.Width,
                    RowCountTitlePosition.Y
                );
                ColumnCountTitlePosition = new PointF(
                    RowCountTitlePosition.X,
                    BufferSize.Height -
                        (Math.Max(
                            Math.Max(ColumnCountTitleSize.Height, ColumnCountValueSize.Height),
                            Math.Max(LifeChanceTitleSize.Height, LifeChanceValueSize.Height)) +
                        Style.ElementMargin)
                );
                ColumnCountValuePosition = new PointF(
                    ColumnCountTitlePosition.X + ColumnCountTitleSize.Width,
                    ColumnCountTitlePosition.Y
                );
                CellSizeTitlePosition = new PointF(
                    BufferSize.Width -
                        (Math.Max(CellSizeTitleSize.Width + CellSizeValueSize.Width, 
                            LifeChanceTitleSize.Width + LifeChanceValueSize.Width) +
                        Style.ElementMargin),
                    RowCountTitlePosition.Y
                );
                CellSizeValuePosition = new PointF(
                    CellSizeTitlePosition.X + CellSizeTitleSize.Width,
                    RowCountTitlePosition.Y
                );
                LifeChanceTitlePosition = new PointF(
                    CellSizeTitlePosition.X,
                    ColumnCountTitlePosition.Y
                );
                LifeChanceValuePosition = new PointF(
                    LifeChanceTitlePosition.X + LifeChanceTitleSize.Width,
                    ColumnCountTitlePosition.Y
                );

                // Calculate total size
                GridSettingsSize = new SizeF(
                    BufferSize.Width - RowCountTitlePosition.X + Style.ElementMargin,
                    BufferSize.Height - RowCountTitlePosition.Y + Style.ElementMargin
                );
        }

        /// <summary>
        /// Draws the game logo to the buffer.
        /// </summary>
        private static void DrawLogo()
        {
            Bitmap LogoImage = new Bitmap(Properties.Resources.Logo);
            Painter.DrawImage(LogoImage, 0, 0);
            LogoImage.Dispose();
        }

        /// <summary>
        /// Draws the help prompt to the buffer.
        /// </summary>
        private static void DrawHelpPrompt()
        {
            Painter.DrawString(
                Properties.Resources.HelpPrompt,
                Style.HelpPromptFont,
                Style.HelpPromptTextColor,
                BufferSize.Width - HelpPromptSize.Width - Style.ElementMargin,
                Style.ElementMargin
            );
        }

        /// <summary>
        /// Draws the generation count to the buffer.
        /// </summary>
        private static void DrawGenerationCount()
        {
            // Paint generation count title
            Painter.DrawString(
                Properties.Resources.GenerationTitle,
                Style.GenerationCountTitleFont,
                Style.GenerationCountTitleTextColor,
                GenerationCountTitlePosition
            );

            // Paint generation count value
            String GenerationCountValue;
            if (State.Screen == ScreenState.GameRunning ||
                State.Screen == ScreenState.GameStopped)
            {
                GenerationCountValue = "" + Life.CurrentGeneration;
            }
            else
            {
                GenerationCountValue = Properties.Resources.GenerationEmpty;
            }
            Painter.DrawString(
                GenerationCountValue,
                Style.GenerationCountValueFont,
                Style.GenerationCountValueTextColor,
                GenerationCountValuePosition
            );
        }

        /// <summary>
        /// Draws the grid settings to the buffer.
        /// </summary>
        private static void DrawGridSettings()
        {
            // Paint titles and values
            Painter.DrawString(
                Properties.Resources.RowCountTitle,
                Style.GridSettingsTitleFont,
                Style.GridSettingsTitleTextColor,
                RowCountTitlePosition
            );

            Painter.DrawString(
                Properties.Resources.ColumnCountTitle,
                Style.GridSettingsTitleFont,
                Style.GridSettingsTitleTextColor,
                ColumnCountTitlePosition
            );

            Painter.DrawString(
                Properties.Resources.CellSizeTitle,
                Style.GridSettingsTitleFont,
                Style.GridSettingsTitleTextColor,
                CellSizeTitlePosition
            );

            Painter.DrawString(
                Properties.Resources.LifeChanceTitle,
                Style.GridSettingsTitleFont,
                Style.GridSettingsTitleTextColor,
                LifeChanceTitlePosition
            );

            Painter.DrawString(
                Setting.Rows + " (" + Properties.Resources.Max +
                    " " + CalculateMaxRows() + ")",
                Style.GridSettingsValueFont,
                Style.GridSettingsValueTextColor,
                RowCountValuePosition
            );

            Painter.DrawString(
                Setting.Columns + " (" + Properties.Resources.Max +
                    " " + CalculateMaxColumns() + ")",
                Style.GridSettingsValueFont,
                Style.GridSettingsValueTextColor,
                ColumnCountValuePosition
            );

            Painter.DrawString(
                Setting.CellSize + "px (" + Properties.Resources.Max +
                    " " + CalculateMaxCellSize() + "px)",
                Style.GridSettingsValueFont,
                Style.GridSettingsValueTextColor,
                CellSizeValuePosition
            );

            Painter.DrawString(
                Setting.LifeChance + "%",
                Style.GridSettingsValueFont,
                Style.GridSettingsValueTextColor,
                LifeChanceValuePosition
            );

        }

        /// <summary>
        /// Builds the help popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawHelpPopup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Builds the credits popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawCreditsPopup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Builds the exit confirmation popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawExitConfirmationPopup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Builds the game outcome popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawOutcomePopup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws the grid to the buffer.
        /// </summary>
        private static void DrawGrid()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws the colored squares representing life to the buffer.
        /// </summary>
        private static void DrawLife()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws a popup to the buffer.
        /// </summary>
        private static void DrawPopup()
        {
            throw new System.NotImplementedException();
        }



        private static Size CalculateGridSpace()
        {
            Size GridSpace = new Size(
                BufferSize.Width - (Style.ElementMargin * 2),
                BufferSize.Height -
                    (int)Math.Ceiling(Math.Max(
                        LogoSize.Height,
                        HelpPromptSize.Height
                    )) -
                    (int)Math.Ceiling(Math.Max(
                        GenerationCountSize.Height,
                        GridSettingsSize.Height
                    )) + (Style.ElementMargin * 6)
            );
            return GridSpace;
        }

        public static int CalculateMaxRows()
        {
            int MaxRows = (CalculateGridSpace().Height - 1) / (Setting.CellSize + 1);
            return MaxRows;
        }

        public static int CalculateMaxColumns()
        {
            int MaxColumns = (CalculateGridSpace().Width - 1) / (Setting.CellSize + 1);
            return MaxColumns;
        }

        public static int CalculateMaxCellSize()
        {
            int MaxCellSize = Math.Min(
                ((CalculateGridSpace().Height - 1) / Setting.Rows) - 1,
                ((CalculateGridSpace().Width - 1) / Setting.Columns) - 1
                );
            return MaxCellSize;
        }

        private static void SetDefaultGridSize()
        {
            Setting.Rows = CalculateMaxRows();
            Setting.Columns = CalculateMaxColumns();
        }
    }
}
