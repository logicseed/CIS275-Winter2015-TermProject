/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System;
using System.Drawing;
using System.Drawing.Text;

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

        // Some of the measurement and positioning calculations could be
        // resource intensive on some machines, so we want to avoid measuring
        // and calculating positions when we don't have to.
        private static SizeF LogoSize;
        private static SizeF HelpPromptSize;
        private static SizeF GenerationCountSize;
        private static SizeF GridSettingsSize;
        private static Size GridSize;

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
        private static PointF GridPosition;

        // Measuring and positioning the grid is probably the most resource intensive
        // of all of the measuring and positioning calculations, so we want to avoid
        // performing it whenever we can. If there hasn't been any changes to the
        // grid settings then we should always be able to use the same values as
        // before.
        private static bool ValidGrid = false;

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
            //Painter.TextRenderingHint = TextRenderingHint.AntiAlias;
            //Painter.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            //Painter.TextRenderingHint = TextRenderingHint.SystemDefault;
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
                case PopupState.Introduction:
                    DrawIntroductionPopup();
                    break;
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
        /// If something changes a grid setting them we need to know to perform
        /// a recalculation of the grid's size and position.
        /// </summary>
        public static void InvalidateGrid()
        {
            ValidGrid = false;
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
            State.Popup = PopupState.Introduction;
            
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
        /// Builds the introduction popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawIntroductionPopup()
        {
            // Build message array
            PopupMessage[] Messages = new PopupMessage[11];
            Messages[0].Text = Properties.Resources.CreditsTitle;
            Messages[0].Style = Style.CreditsPopupTitleFont;
            Messages[0].Color = Style.CreditsPopupTitleTextColor;
            Messages[1].Text = "  ";
            Messages[1].Style = Style.PopupSpacerFont;
            Messages[1].Color = Style.PopupSpacerColor;
            Messages[2].Text = Properties.Resources.CreditsGame;
            Messages[2].Style = Style.CreditsPopupSectionFont;
            Messages[2].Color = Style.CreditsPopupSectionTextColor;
            Messages[3].Text = Properties.Resources.CreditsConway;
            Messages[3].Style = Style.CreditsPopupItemFont;
            Messages[3].Color = Style.CreditsPopupItemTextColor;
            Messages[4].Text = "  ";
            Messages[4].Style = Style.PopupSpacerFont;
            Messages[4].Color = Style.PopupSpacerColor;
            Messages[5].Text = Properties.Resources.CreditsProgramming;
            Messages[5].Style = Style.CreditsPopupSectionFont;
            Messages[5].Color = Style.CreditsPopupSectionTextColor;
            Messages[6].Text = Properties.Resources.CreditsMarcKing;
            Messages[6].Style = Style.CreditsPopupItemFont;
            Messages[6].Color = Style.CreditsPopupItemTextColor;
            Messages[7].Text = "  ";
            Messages[7].Style = Style.PopupSpacerFont;
            Messages[7].Color = Style.PopupSpacerColor;
            Messages[8].Text = Properties.Resources.CreditsStyles;
            Messages[8].Style = Style.CreditsPopupSectionFont;
            Messages[8].Color = Style.CreditsPopupSectionTextColor;
            Messages[9].Text = Properties.Resources.CreditsGoogle;
            Messages[9].Style = Style.CreditsPopupItemFont;
            Messages[9].Color = Style.CreditsPopupItemTextColor;
            Messages[10].Text = Properties.Resources.CreditsModMarc;
            Messages[10].Style = Style.CreditsPopupItemFont;
            Messages[10].Color = Style.CreditsPopupItemTextColor;

            // Specify alignments
            Alignments Alignment = new Alignments();
            Alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            Alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            Alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            DrawPopup(Messages, Alignment, Style.PopupPadding, Style.PopupSpacing);
        }

        /// <summary>
        /// Builds the help popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawHelpPopup()
        {
            // Build message array
            PopupMessage[] Messages = new PopupMessage[18];
            Messages[0].Text = Properties.Resources.HelpTitle;
            Messages[0].Style = Style.HelpPopupTitleFont;
            Messages[0].Color = Style.HelpPopupTitleTextColor;
            Messages[1].Text = Properties.Resources.HelpMainControls;
            Messages[1].Style = Style.HelpPopupSectionFont;
            Messages[1].Color = Style.HelpPopupSectionTextColor;
            Messages[2].Text = Properties.Resources.HelpExitGame;
            Messages[2].Style = Style.HelpPopupItemFont;
            Messages[2].Color = Style.HelpPopupItemTextColor;
            Messages[3].Text = Properties.Resources.HelpNextStep;
            Messages[3].Style = Style.HelpPopupItemFont;
            Messages[3].Color = Style.HelpPopupItemTextColor;
            Messages[4].Text = Properties.Resources.HelpAutoStep;
            Messages[4].Style = Style.HelpPopupItemFont;
            Messages[4].Color = Style.HelpPopupItemTextColor;
            Messages[5].Text = Properties.Resources.HelpEndGame;
            Messages[5].Style = Style.HelpPopupItemFont;
            Messages[5].Color = Style.HelpPopupItemTextColor;
            Messages[6].Text = Properties.Resources.HelpShowCredits;
            Messages[6].Style = Style.HelpPopupItemFont;
            Messages[6].Color = Style.HelpPopupItemTextColor;
            Messages[7].Text = Properties.Resources.HelpBeforeGame;
            Messages[7].Style = Style.HelpPopupSectionFont;
            Messages[7].Color = Style.HelpPopupSectionTextColor;
            Messages[8].Text = Properties.Resources.HelpIncreaseRows;
            Messages[8].Style = Style.HelpPopupItemFont;
            Messages[8].Color = Style.HelpPopupItemTextColor;
            Messages[9].Text = Properties.Resources.HelpDecreaseRows;
            Messages[9].Style = Style.HelpPopupItemFont;
            Messages[9].Color = Style.HelpPopupItemTextColor;
            Messages[10].Text = Properties.Resources.HelpIncreaseColumns;
            Messages[10].Style = Style.HelpPopupItemFont;
            Messages[10].Color = Style.HelpPopupItemTextColor;
            Messages[11].Text = Properties.Resources.HelpDecreaseColumns;
            Messages[11].Style = Style.HelpPopupItemFont;
            Messages[11].Color = Style.HelpPopupItemTextColor;
            Messages[12].Text = Properties.Resources.HelpIncreaseCellSize;
            Messages[12].Style = Style.HelpPopupItemFont;
            Messages[12].Color = Style.HelpPopupItemTextColor;
            Messages[13].Text = Properties.Resources.HelpDecreaseCellSize;
            Messages[13].Style = Style.HelpPopupItemFont;
            Messages[13].Color = Style.HelpPopupItemTextColor;
            Messages[14].Text = Properties.Resources.HelpIncreaseLifeChance;
            Messages[14].Style = Style.HelpPopupItemFont;
            Messages[14].Color = Style.HelpPopupItemTextColor;
            Messages[15].Text = Properties.Resources.HelpDecreaseLifeChance;
            Messages[15].Style = Style.HelpPopupItemFont;
            Messages[15].Color = Style.HelpPopupItemTextColor;
            Messages[16].Text = " ";
            Messages[16].Style = Style.PopupSpacerFont;
            Messages[16].Color = Style.PopupSpacerColor;
            Messages[17].Text = Properties.Resources.PressAnyKey;
            Messages[17].Style = Style.PressAnyKeyFont;
            Messages[17].Color = Style.PressAnyKeyTextColor;

            // Specify alignments
            Alignments Alignment = new Alignments();
            Alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            Alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            Alignment.MessageHorizontalAlignment = HorizontalAlignment.Left;

            DrawPopup(Messages, Alignment, Style.PopupPadding, Style.PopupSpacing);
        }

        /// <summary>
        /// Builds the credits popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawCreditsPopup()
        {
            // Build message array
            PopupMessage[] Messages = new PopupMessage[13];
            Messages[0].Text = Properties.Resources.CreditsTitle;
            Messages[0].Style = Style.CreditsPopupTitleFont;
            Messages[0].Color = Style.CreditsPopupTitleTextColor;
            Messages[1].Text = "  ";
            Messages[1].Style = Style.PopupSpacerFont;
            Messages[1].Color = Style.PopupSpacerColor;
            Messages[2].Text = Properties.Resources.CreditsGame;
            Messages[2].Style = Style.CreditsPopupSectionFont;
            Messages[2].Color = Style.CreditsPopupSectionTextColor;
            Messages[3].Text = Properties.Resources.CreditsConway;
            Messages[3].Style = Style.CreditsPopupItemFont;
            Messages[3].Color = Style.CreditsPopupItemTextColor;
            Messages[4].Text = "  ";
            Messages[4].Style = Style.PopupSpacerFont;
            Messages[4].Color = Style.PopupSpacerColor;
            Messages[5].Text = Properties.Resources.CreditsProgramming;
            Messages[5].Style = Style.CreditsPopupSectionFont;
            Messages[5].Color = Style.CreditsPopupSectionTextColor;
            Messages[6].Text = Properties.Resources.CreditsMarcKing;
            Messages[6].Style = Style.CreditsPopupItemFont;
            Messages[6].Color = Style.CreditsPopupItemTextColor;
            Messages[7].Text = "  ";
            Messages[7].Style = Style.PopupSpacerFont;
            Messages[7].Color = Style.PopupSpacerColor;
            Messages[8].Text = Properties.Resources.CreditsStyles;
            Messages[8].Style = Style.CreditsPopupSectionFont;
            Messages[8].Color = Style.CreditsPopupSectionTextColor;
            Messages[9].Text = Properties.Resources.CreditsGoogle;
            Messages[9].Style = Style.CreditsPopupItemFont;
            Messages[9].Color = Style.CreditsPopupItemTextColor;
            Messages[10].Text = Properties.Resources.CreditsModMarc;
            Messages[10].Style = Style.CreditsPopupItemFont;
            Messages[10].Color = Style.CreditsPopupItemTextColor;
            Messages[11].Text = " ";
            Messages[11].Style = Style.PopupSpacerFont;
            Messages[11].Color = Style.PopupSpacerColor;
            Messages[12].Text = Properties.Resources.PressAnyKey;
            Messages[12].Style = Style.PressAnyKeyFont;
            Messages[12].Color = Style.PressAnyKeyTextColor;

            // Specify alignments
            Alignments Alignment = new Alignments();
            Alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            Alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            Alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            DrawPopup(Messages, Alignment, Style.PopupPadding, Style.PopupSpacing);
        }

        /// <summary>
        /// Builds the exit confirmation popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawExitConfirmationPopup()
        {
            // Build message array
            PopupMessage[] Messages = new PopupMessage[2];
            Messages[0].Text = Properties.Resources.ExitConfirmation;
            Messages[0].Style = Style.ExitConfirmationTitleFont;
            Messages[0].Color = Style.ExitConfirmationTitleTextColor;
            Messages[1].Text = Properties.Resources.ExitCancel;
            Messages[1].Style = Style.ExitConfirmationSectionFont;
            Messages[1].Color = Style.ExitConfirmationSectionTextColor;

            // Specify alignments
            Alignments Alignment = new Alignments();
            Alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            Alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            Alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            DrawPopup(Messages, Alignment, Style.PopupPadding, Style.PopupSpacing);
        }

        /// <summary>
        /// Builds the game outcome popup data and then calls DrawPopup().
        /// </summary>
        private static void DrawOutcomePopup()
        {
            // Build message array
            PopupMessage[] Messages = new PopupMessage[2];
            if (Life.Extinction)
            {
                Messages[0].Text = Properties.Resources.GameExtinction;
                Messages[0].Color = Style.OutcomePopupExtinctionTextColor;
            }
            else if (Life.Stabilization)
            {
                Messages[0].Text = Properties.Resources.GameStabilization;
                Messages[0].Color = Style.OutcomePopupStabilizationTextColor;
            }
            else
            {
                Messages[0].Text = Properties.Resources.GameOscillation;
                Messages[0].Color = Style.OutcomePopupOscillationTextColor;
            }
            Messages[0].Style = Style.OutcomePopupFont;
            Messages[1].Text = Properties.Resources.PressAnyKey;
            Messages[1].Style = Style.PressAnyKeyFont;
            Messages[1].Color = Style.PressAnyKeyTextColor;

            // Specify alignments
            Alignments Alignment = new Alignments();
            Alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            Alignment.PopupVerticalAlignment = VerticalAlignment.Bottom;
            Alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            DrawPopup(Messages, Alignment, Style.PopupPadding, Style.PopupSpacing);
        }

        /// <summary>
        /// Draws the grid to the buffer.
        /// </summary>
        private static void DrawGrid()
        {
            // make this draw an image after the game starts running to
            // try to make it more efficient

            Pen CurrentPen;

            // When users have access to change the grid settings we use a
            // darker pen to make it easier to see. Then when the game is
            // running we make the grid less pronounced so the life squares
            // stand out more.
            if (State.Screen == ScreenState.GameRunning ||
                State.Screen == ScreenState.GameStopped)
            {
                CurrentPen = Style.GridCellPen;
            }
            else
            {
                CurrentPen = Style.GridDarkCellPen;
            }

            // Check to see if the current grid settings have been measured.
            // Measure them if they haven't.
            if (!ValidGrid) MeasureGrid();

            // DrawGrid surrounding rectangle.
            Painter.DrawRectangle(
                CurrentPen, 
                GridPosition.X, 
                GridPosition.Y,
                GridSize.Width, 
                GridSize.Height
            );

            // DrawGrid horizontal lines.
            for (int i = 1; i < Setting.Rows; i++)
            {
                Painter.DrawLine(
                    CurrentPen,
                    GridPosition.X,
                    GridPosition.Y + i + (i * Setting.CellSize),
                    GridPosition.X + GridSize.Width,
                    GridPosition.Y + i + (i * Setting.CellSize)
                );
            }

            // DrawGrid vertical lines.
            for (int i = 1; i < Setting.Columns; i++)
            {
                Painter.DrawLine(
                    CurrentPen,
                    GridPosition.X + i + (i * Setting.CellSize),
                    GridPosition.Y,
                    GridPosition.X + i + (i * Setting.CellSize),
                    GridPosition.Y + GridSize.Height);
            }
        }

        /// <summary>
        /// Measures the grid that would result with the current grid settings.
        /// </summary>
        private static void MeasureGrid()
        {
            // Calculate the size of the grid.
            // We want a pixel between each cell and a pixel surrounding
            // all the cells.
            GridSize = new Size(
                (Setting.CellSize * Setting.Columns) + Setting.Columns, // Width
                (Setting.CellSize * Setting.Rows) + Setting.Rows // Height
            );

            // The grid position is based on the size of the screen.
            // We attempt to center the grid vertically and horizontally.
            // To overcome grid flickering due to anti-aliasing, we add
            // 0.5f to each value.
            GridPosition = new PointF(
                (float)Math.Floor((BufferSize.Width - GridSize.Width) / 2.0f) + 0.5f,
                (float)Math.Floor((BufferSize.Height - GridSize.Height) / 2.0f) + 0.5f
            );

            // Claim gridSize is valid.
            ValidGrid = true;
        }

        /// <summary>
        /// Draws the colored squares representing life to the buffer.
        /// </summary>
        private static void DrawLife()
        {
            // Draw life rectangles
            for (int i = 0; i < Setting.Rows; i++)
            {
                for (int j = 0; j < Setting.Columns; j++)
                {
                    if (Life.GetCell(i, j) > 0)
                    {
                        Painter.FillRectangle(
                            Style.LifeBrush[Life.GetCell(i, j)],
                            GridPosition.X + ((Setting.CellSize + 1) * j),
                            GridPosition.Y + ((Setting.CellSize + 1) * i),
                            Setting.CellSize, Setting.CellSize
                        );
                    }
                }
            }
        }

        /// <summary>
        /// Will draw a popup to the buffer with a shaded background over the existing
        /// screen elements and a bright background beneath the popup message.
        /// </summary>
        /// <param name="Messages">An array of messages displayed in the popup.</param>
        /// <param name="PopupAlignment">Where the popup will be drawn.</param>
        /// <param name="MessageMargin">The margin between the messages and the edge of the popup.</param>
        /// <param name="MessageSpacing">The space between each individual message.</param>
        private static void DrawPopup(PopupMessage[] Messages, Alignments PopupAlignment, float MessageMargin, float MessageSpacing)
        {
            SizeF[] MessageSize = new SizeF[Messages.Length];
            SizeF TotalMessageSize = new SizeF();
            SizeF PopupSize = new SizeF();
            PointF[] MessagePosition = new PointF[Messages.Length];
            PointF PopupPosition = new PointF();

            // Calculate sizes of all the messages and total size of all the messages.
            for (int i = 0; i < Messages.Length; i++)
            {
                MessageSize[i] = Painter.MeasureString(Messages[i].Text, Messages[i].Style);
                TotalMessageSize.Width = Math.Max(MessageSize[i].Width, TotalMessageSize.Width);
                TotalMessageSize.Height += MessageSize[i].Height;
            }
            TotalMessageSize.Height += MessageSpacing * (Messages.Length - 1);

            // Calculate the size of the popup.
            PopupSize.Width = TotalMessageSize.Width + (MessageMargin * 2);
            PopupSize.Height = TotalMessageSize.Height + (MessageMargin * 2);

            // Calculate the X position of the popup.
            if (PopupAlignment.PopupHorizontalAlignment == HorizontalAlignment.Left)
            {
                PopupPosition.X = 0;
            }
            else if (PopupAlignment.PopupHorizontalAlignment == HorizontalAlignment.Right)
            {
                PopupPosition.X = BufferSize.Width - PopupSize.Width;
            }
            else // Center
            {
                PopupPosition.X = (BufferSize.Width / 2) - (PopupSize.Width / 2);
            }

            // Calculate the Y position of the popup.
            if (PopupAlignment.PopupVerticalAlignment == VerticalAlignment.Top)
            {
                PopupPosition.Y = 0;
            }
            else if (PopupAlignment.PopupVerticalAlignment == VerticalAlignment.Bottom)
            {
                PopupPosition.Y = BufferSize.Height - PopupSize.Height;
            }
            else // Middle
            {
                PopupPosition.Y = (BufferSize.Height / 2) - (PopupSize.Height / 2);
            }

            // Calculate the positions of the messages.
            for (int i = 0; i < Messages.Length; i++)
            {
                // Horizontal position
                if (PopupAlignment.MessageHorizontalAlignment == HorizontalAlignment.Left)
                {
                    MessagePosition[i].X = PopupPosition.X + MessageMargin;
                }
                else if (PopupAlignment.MessageHorizontalAlignment == HorizontalAlignment.Right)
                {
                    MessagePosition[i].X = (PopupPosition.X + PopupSize.Width) -
                        (MessageSize[i].Width + MessageMargin);
                }
                else // Center
                {
                    MessagePosition[i].X = PopupPosition.X + (PopupSize.Width / 2) -
                        (MessageSize[i].Width / 2);
                }
                // Vertical position
                if (i == 0)
                {
                    MessagePosition[i].Y = PopupPosition.Y + MessageMargin;
                }
                else
                {
                    MessagePosition[i].Y = MessagePosition[i - 1].Y + MessageSize[i - 1].Height +
                        MessageSpacing;
                }
            }

            // Darken background
            Painter.FillRectangle(
                Style.PopupShadingColor, 
                0, 0, 
                BufferSize.Width, 
                BufferSize.Height);

            // DrawGrid popup background
            Painter.FillRectangle(
                Style.PopupBackgroundColor,
                PopupPosition.X,
                PopupPosition.Y,
                PopupSize.Width,
                PopupSize.Height
            );

            // DrawGrid messages
            for (int i = 0; i < Messages.Length; i++)
            {
                Painter.DrawString(
                    Messages[i].Text,
                    Messages[i].Style,
                    Messages[i].Color,
                    MessagePosition[i]);
            }
        }
        // Used to build popups using DrawPopup()
        private struct PopupMessage
        {
            public String Text;
            public Font Style;
            public SolidBrush Color;
        }
        private struct Alignments
        {
            public HorizontalAlignment PopupHorizontalAlignment;
            public VerticalAlignment PopupVerticalAlignment;
            public HorizontalAlignment MessageHorizontalAlignment;
            public VerticalAlignment MessageVerticalAlignment;
        }
        private enum HorizontalAlignment
        {
            Center,
            Left,
            Right
        }
        private enum VerticalAlignment
        {
            Top,
            Middle,
            Bottom
        }

        /// <summary>
        /// Calculates the amount of space we have to draw a grid after fitting
        /// all the other elements on the screen.
        /// </summary>
        /// <returns>The size of the space we have to draw the grid.</returns>
        private static Size CalculateGridSpace()
        {
            Size GridSpace = new Size(
                BufferSize.Width - (Style.ElementMargin * 2),
                BufferSize.Height -
                    ((int)Math.Ceiling(Math.Max(
                        LogoSize.Height,
                        HelpPromptSize.Height
                    )) +
                    (int)Math.Ceiling(Math.Max(
                        GenerationCountSize.Height,
                        GridSettingsSize.Height
                    )) + (Style.ElementMargin * 2))
            );
            return GridSpace;
        }

        /// <summary>
        /// Calculates the maximum number of rows we can fit in the current grid
        /// space with the current cell size setting.
        /// </summary>
        /// <returns>The maximum number of rows that can fit in the grid space.</returns>
        public static int CalculateMaxRows()
        {
            int MaxRows = (CalculateGridSpace().Height - 1) / (Setting.CellSize + 1);
            return MaxRows;
        }

        /// <summary>
        /// Calculates the maximum number of columns we can fit in the current grid
        /// space with the current cell size setting.
        /// </summary>
        /// <returns>The maximum number of columns that can fit in the grid space.</returns>
        public static int CalculateMaxColumns()
        {
            int MaxColumns = (CalculateGridSpace().Width - 1) / (Setting.CellSize + 1);
            return MaxColumns;
        }

        /// <summary>
        /// Calculates the maximum cell size we can fit in the current grid space
        /// with the current rows and columns settings.
        /// </summary>
        /// <returns>The maximum pixel size of the cells that can fit in the grid space.</returns>
        public static int CalculateMaxCellSize()
        {
            int MaxCellSize = Math.Min(
                ((CalculateGridSpace().Height - 1) / Setting.Rows) - 1,
                ((CalculateGridSpace().Width - 1) / Setting.Columns) - 1
                );
            return MaxCellSize;
        }

        /// <summary>
        /// Determines the default number of rows and columns based on our default
        /// cell size of 20px.
        /// </summary>
        private static void SetDefaultGridSize()
        {
            Setting.Rows = CalculateMaxRows();
            Setting.Columns = CalculateMaxColumns();
        }

        /// <summary>
        /// Maximizes the grid in regards to rows and columns.
        /// DEVELOPER ONLY
        /// </summary>
        public static void MaximizeGridSize()
        {
            Setting.CellSize = 5;
            Setting.Rows = CalculateMaxRows();
            Setting.Columns = CalculateMaxColumns();
            ValidGrid = false;
        }

        /// <summary>
        /// Sets a very large cell size and them fits as many rows and columns
        /// as possible on the screen.
        /// DEVELOPER ONLY
        /// </summary>
        public static void MinimizeGridSize()
        {
            Setting.CellSize = 100;
            Setting.Rows = CalculateMaxRows();
            Setting.Columns = CalculateMaxColumns();
            ValidGrid = false;
        }
    }
}
