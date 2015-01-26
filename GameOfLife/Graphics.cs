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
    partial class Program
    {
        #region Graphics Fields

        // Additional program-state fields that monitor the state
        // of certain fields that require calculations. The calculations
        // are only performed if the they are considered invalid due to
        // other program changes.
        // Example: Once the game starts, the grid cannot be resized so
        // it is safe to use the last known GridSize and GridPosition.
        private bool isGridSizeValid = false;
        private bool isGridPositionValid = false;
        private bool hasCachedGrid = false;
        private Bitmap cachedGrid;

        


        private Graphics screenPainter;
        // The screen buffer is used to build the screen before displaying it.
        private Bitmap screenBuffer;

        private Size gridSize;
        private PointF gridPosition;


        private bool haveMeasuredGridSettings = false;
        private bool haveMeasuredGenerationCount = false;
        private PointF generationCountTitlePosition;
        private PointF generationCountValuePosition;
        private PointF rowCountTitlePosition;
        private PointF rowCountValuePosition;
        private PointF columnCountTitlePosition;
        private PointF columnCountValuePosition;
        private PointF cellSizeTitlePosition;
        private PointF cellSizeValuePosition;
        private PointF lifeChanceTitlePosition;
        private PointF lifeChanceValuePosition;

        private SizeF totalLogoSize;
        private SizeF totalHelpPromptSize;
        private SizeF totalGenerationCountSize;
        private SizeF totalSettingsSize;
        

        private static Point origin = new Point(0,0);

        #endregion Graphics Fields

        #region Image Constants

        private static Size SplashSize = new Size(800, 600);
        private static Size LogoSize = new Size(100, 100);
        private static Size popupMargin = new Size(20, 20);
        private static Size elementMargin = new Size(10, 10);
        private static Size elementSpacing = new Size(5, 5);

        #endregion Image Constants

        #region Graphics Properties

        private Size GridSize
        {
            get
            {
                if (!isGridSizeValid)
                {
                    // Calculate the size of the grid.
                    // We want a pixel between each cell and a pixel surrounding
                    // all the cells.
                    gridSize = new Size(
                        (gridCellSize * gridColumns) + gridColumns, // Width
                        (gridCellSize * gridRows) + gridRows // Height
                    );

                    // Claim gridSize is valid.
                    isGridSizeValid = true;

                    return gridSize;
                }
                else
                {
                    return gridSize;
                }
            }
        }

        private PointF GridPosition
        {
            get
            {
                if (!isGridPositionValid)
                {
                    // The grid position is based on the size of the screen.
                    // We attempt to center the grid vertically and horizontally.
                    // To overcome grid flickering due to anti-aliasing, we add
                    // 0.5f to each value; see Book2 page 32 for explanation.
                    gridPosition = new PointF(
                        (float)Math.Floor((this.Width - GridSize.Width) / 2.0f) + 0.5f,
                        (float)Math.Floor((this.Height - GridSize.Height) / 2.0f) + 0.5f
                    );

                    // Claim gridPosition is valid.
                    isGridPositionValid = true;

                    return gridPosition;
                }
                else
                {
                    return gridPosition;
                }
            }
        }

        #endregion Graphics Properties 

        // We override the OnPaintBackground function to allow the paint event
        // to handle all the graphics. This prevents screen flickering.
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        private void Program_Paint(object sender, PaintEventArgs e)
        {
            BuildScreenBuffer();
            e.Graphics.DrawImage(screenBuffer, origin);
        }

        private void BuildScreenBuffer()
        {
            // Initialize the screen buffer bitmap.
            screenBuffer = new Bitmap(this.Width, this.Height);

            // Initialize the screen painter graphics object.
            screenPainter = Graphics.FromImage(screenBuffer);
            screenPainter.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (State[ShowSplash])
            {
                DrawSplashScreen();
            }
            else
            {
                DrawMainScreen();
            }

            if(State[ShowConfirmExit])
            {
                DrawExitConfirmation();
            }
            else if (State[ShowHelp])
            {
                DrawHelpScreen();
            }
            else if (State[ShowCredits])
            {
                DrawCreditsScreen();
            }
            else if (State[ShowOutcome])
            {
                DrawCompletionMessage();
            }
        }

        

        private void InvalidateGrid()
        {
            isGridSizeValid = false;
            isGridPositionValid = false;
        }

        #region DrawGrid Screen Element Methods

        private void DrawSplashScreen()
        {
            // DrawGrid background.
            screenPainter.FillRectangle(darkBackgroundBrush,
                0, 0, this.Width, this.Height);

            // DrawGrid splash screen graphic.
            Bitmap splashImage = new Bitmap(Properties.Resources.Splash);
            screenPainter.DrawImage(splashImage,
                (this.Width - splashImage.Width) / 2,
                (this.Height - splashImage.Height) / 2);
            splashImage.Dispose();

            // DrawGrid press-any-key text.
            String text = Properties.Resources.SplashScreen;
            SizeF size = screenPainter.MeasureString(text, promptFont);
            screenPainter.DrawString(text, promptFont, blackBrush,
                (this.Width - size.Width) / 2,
                (this.Height - size.Height));


        }

        private void DrawMainScreen()
        {
            screenPainter.FillRectangle(backgroundBrush, 0, 0,
                this.Width, this.Height);

            DrawLogo();

            DrawHelpPrompt();

            DrawGeneration();

            DrawGridSettings();

            if (State[FirstDisplay])
            {
                SetDefaultGridSize();
                State[FirstDisplay] = false;
                DrawMainScreen();
                return;
            }

            DrawGrid();
            if (Life.GameRunning)
            {
                DrawLife();
            }
        }

        private void DrawGrid()
        {
            
            // make this draw an image after the game starts running to
            // try to make it more efficient

            Pen currentPen;

            if(Life.GameRunning)
            {
                currentPen = cellPen;
            }
            else
            {
                currentPen = darkCellPen;
            }

            if (Life.GameRunning && hasCachedGrid)
            {
                screenPainter.DrawImage(cachedGrid, GridPosition);
            }
            else
            {
                cachedGrid = new Bitmap(GridSize.Width + 1, GridSize.Height + 1);
                Graphics gridPainter = Graphics.FromImage(cachedGrid);
                
                // DrawGrid surrounding rectangle.
                screenPainter.DrawRectangle(currentPen, GridPosition.X, GridPosition.Y,
                    GridSize.Width, GridSize.Height);


                // DrawGrid horizontal lines.
                for (int i = 1; i < gridRows; i++)
                {
                    screenPainter.DrawLine(currentPen,
                        GridPosition.X,
                        GridPosition.Y + i + (i * gridCellSize),
                        GridPosition.X + GridSize.Width,
                        GridPosition.Y + i + (i * gridCellSize));
                }



                // DrawGrid vertical lines.
                for (int i = 1; i < gridColumns; i++)
                {
                    screenPainter.DrawLine(currentPen,
                        GridPosition.X + i + (i * gridCellSize),
                        GridPosition.Y,
                        GridPosition.X + i + (i * gridCellSize),
                        GridPosition.Y + GridSize.Height);
                }

              

                screenPainter.DrawImage(cachedGrid, GridPosition);
            }
        }

        private void DrawLife()
        {
            // DrawGrid life rectangles
            for (int i = 1; i <= gridRows; i++)
            {
                for (int j = 1; j <= gridColumns; j++)
                {
                    if (Life.GetElement(i, j) > 0)
                    {
                        screenPainter.FillRectangle(
                            lifeBrush[Life.GetElement(i, j)],
                            GridPosition.X + ((gridCellSize + 1) * (j - 1)),
                            GridPosition.Y + ((gridCellSize + 1) * (i - 1)),
                            gridCellSize, gridCellSize
                        );
                    }
                }
            }
        }

        private void DrawShadedBackground()
        {
            screenPainter.FillRectangle(shadeBrush, 0, 0, this.Width, this.Height);
        }

        

        private void DrawLogo()
        {
            // DrawGrid logo graphic at upper left corner.
            Bitmap logoImage = new Bitmap(Properties.Resources.Logo);
            screenPainter.DrawImage(logoImage, 0, 0);
            totalLogoSize = new SizeF(
                logoImage.Width,
                logoImage.Height +
                    elementMargin.Height
            );
            logoImage.Dispose();
        }

        private void DrawHelpPrompt()
        {

            String text = Properties.Resources.HelpPrompt;
            SizeF helpPromptSize = screenPainter.MeasureString(
                Properties.Resources.HelpPrompt,
                promptFont
            );
            screenPainter.DrawString(
                Properties.Resources.HelpPrompt,
                promptFont,
                blackBrush,
                (this.Width - helpPromptSize.Width) - elementMargin.Width,
                elementMargin.Height
            );

            totalHelpPromptSize = new SizeF(
                elementMargin.Width +
                    helpPromptSize.Width +
                    elementMargin.Width,
                elementMargin.Height +
                    helpPromptSize.Height +
                    elementMargin.Height
            );

        }

        /// <summary>
        /// Paints the generation count title and value in the lower left
        /// corner of the screen. The text is styled by settings in the
        /// Program.Styles.cs file.
        /// </summary>
        private void DrawGeneration()
        {
            // Because fonts and language cannot be changed during runtime
            // we'll only calculate the position of these texts once.
            if(!haveMeasuredGenerationCount)
            {
                // Measure generation count title
                SizeF generationCountTitleSize = screenPainter.MeasureString(
                    Properties.Resources.GenerationTitle,
                    generationTitleFont
                );

                // Measure generation count value
                SizeF generationCountValueSize = screenPainter.MeasureString(
                    "888888888",
                    generationValueFont
                );

                // Calculate positions
                generationCountTitlePosition = new PointF(
                    elementMargin.Width * 2,
                    this.Height - generationCountTitleSize.Height - elementMargin.Height
                );
                generationCountValuePosition = new PointF(
                    (elementMargin.Width * 2) + generationCountTitleSize.Width,
                    generationCountTitlePosition.Y
                );

                // Record total size for determining maximum grid values
                totalGenerationCountSize = new SizeF(
                    generationCountTitleSize.Width + generationCountValueSize.Width
                        + elementMargin.Width * 4,
                    Math.Max(generationCountTitleSize.Height, generationCountValueSize.Width)
                        + elementMargin.Height * 4
                );

                // Let the program know that the positions have been calculated
                haveMeasuredGenerationCount = true;
            }
            
            // Paint generation count title
            screenPainter.DrawString(
                Properties.Resources.GenerationTitle,
                generationTitleFont,
                blackBrush,
                generationCountTitlePosition
            );

            // Paint generation count value
            String generationCountValue;
            if(Life.GameRunning)
            {
                generationCountValue = "" + Life.CurrentGeneration;
            }
            else
            {
                generationCountValue = Properties.Resources.GenerationEmpty;
            }
            screenPainter.DrawString(
                generationCountValue,
                generationValueFont,
                blackBrush,
                generationCountValuePosition
            );
        }

        private void DrawGridSettings()
        {
            // Calculate sizes
            // calculate width of titles

            // Because fonts and language cannot be changed during runtime
            // we'll only calculate the position of these texts once.
            if(!haveMeasuredGridSettings)
            {
                // Measure grid settings titles
                SizeF rowCountTitleSize = screenPainter.MeasureString(
                    Properties.Resources.RowCountTitle,
                    gridSettingTitleFont
                );
                SizeF columnCountTitleSize = screenPainter.MeasureString(
                    Properties.Resources.ColumnCountTitle,
                    gridSettingTitleFont
                );
                SizeF cellSizeTitleSize = screenPainter.MeasureString(
                    Properties.Resources.CellSizeTitle,
                    gridSettingTitleFont
                );
                SizeF lifeChanceTitleSize = screenPainter.MeasureString(
                    Properties.Resources.LifeChanceTitle,
                    gridSettingTitleFont
                );

                // Measure generation count value
                SizeF rowCountValueSize = screenPainter.MeasureString(
                    "8888 (" + Properties.Resources.Max + " 8888)",
                    gridSettingValueFont
                );
                SizeF columnCountValueSize = screenPainter.MeasureString(
                    "8888 (" + Properties.Resources.Max + " 8888)",
                    gridSettingValueFont
                );
                SizeF cellSizeValueSize = screenPainter.MeasureString(
                    "8888px (" + Properties.Resources.Max + " 8888px)",
                    gridSettingValueFont
                );
                SizeF lifeChanceValueSize = screenPainter.MeasureString(
                    "888%",
                    gridSettingValueFont
                );

                // Calculate positions
                rowCountTitlePosition = new PointF(
                    this.Width -
                        (Math.Max(rowCountTitleSize.Width, columnCountTitleSize.Width) +
                        Math.Max(rowCountValueSize.Width, columnCountValueSize.Width) +
                        elementSpacing.Width +
                        Math.Max(cellSizeTitleSize.Width, lifeChanceTitleSize.Width) +
                        Math.Max(cellSizeValueSize.Width, lifeChanceValueSize.Width) +
                        elementMargin.Width),
                    this.Height -
                        (Math.Max(
                            Math.Max(rowCountTitleSize.Height, rowCountValueSize.Height),
                            Math.Max(cellSizeTitleSize.Height, cellSizeValueSize.Height)) +
                        elementSpacing.Height +
                        Math.Max(
                            Math.Max(columnCountTitleSize.Height, columnCountValueSize.Height),
                            Math.Max(lifeChanceTitleSize.Height, lifeChanceValueSize.Height)) +
                        elementMargin.Height)
                );
                rowCountValuePosition = new PointF(
                    rowCountTitlePosition.X + rowCountTitleSize.Width,
                    rowCountTitlePosition.Y
                );
                columnCountTitlePosition = new PointF(
                    rowCountTitlePosition.X,
                    this.Height -
                        (Math.Max(
                            Math.Max(columnCountTitleSize.Height, columnCountValueSize.Height),
                            Math.Max(lifeChanceTitleSize.Height, lifeChanceValueSize.Height)) +
                        elementMargin.Height)
                );
                columnCountValuePosition = new PointF(
                    columnCountTitlePosition.X + columnCountTitleSize.Width,
                    columnCountTitlePosition.Y
                );
                cellSizeTitlePosition = new PointF(
                    this.Width -
                        (Math.Max(cellSizeTitleSize.Width, lifeChanceTitleSize.Width) +
                        Math.Max(cellSizeValueSize.Width, lifeChanceValueSize.Width) +
                        elementMargin.Width),
                    rowCountTitlePosition.Y
                );
                cellSizeValuePosition = new PointF(
                    cellSizeTitlePosition.X + cellSizeTitleSize.Width,
                    rowCountTitlePosition.Y
                );
                lifeChanceTitlePosition = new PointF(
                    cellSizeTitlePosition.X,
                    columnCountTitlePosition.Y
                );
                lifeChanceValuePosition = new PointF(
                    lifeChanceTitlePosition.X + lifeChanceTitleSize.Width,
                    columnCountTitlePosition.Y
                );

                // store total size
                totalSettingsSize = new SizeF(
                    elementMargin.Width +
                        Math.Max(rowCountTitleSize.Width, columnCountTitleSize.Width) +
                        Math.Max(rowCountValueSize.Width, columnCountValueSize.Width) +
                        elementSpacing.Width +
                        Math.Max(cellSizeTitleSize.Width, lifeChanceTitleSize.Width) +
                        Math.Max(cellSizeValueSize.Width, lifeChanceValueSize.Width) +
                        elementMargin.Width,
                    elementMargin.Height +
                        Math.Max(
                            Math.Max(rowCountTitleSize.Height, rowCountValueSize.Height),
                            Math.Max(cellSizeTitleSize.Height, cellSizeValueSize.Height)) +
                        elementSpacing.Height +
                        Math.Max(
                            Math.Max(columnCountTitleSize.Height, columnCountValueSize.Height),
                            Math.Max(lifeChanceTitleSize.Height, lifeChanceValueSize.Height)) +
                        elementMargin.Height
                );

                // Let the program know that the positions have been calculated
                haveMeasuredGenerationCount = true;
            }

            // Place titles and values
            screenPainter.DrawString(
                Properties.Resources.RowCountTitle,
                gridSettingTitleFont,
                blackBrush,
                rowCountTitlePosition
            );

            screenPainter.DrawString(
                Properties.Resources.ColumnCountTitle,
                gridSettingTitleFont,
                blackBrush,
                columnCountTitlePosition
            );

            screenPainter.DrawString(
                Properties.Resources.CellSizeTitle,
                gridSettingTitleFont,
                blackBrush,
                cellSizeTitlePosition
            );

            screenPainter.DrawString(
                Properties.Resources.LifeChanceTitle,
                gridSettingTitleFont,
                blackBrush,
                lifeChanceTitlePosition
            );

            screenPainter.DrawString(
                gridRows + " (" + Properties.Resources.Max +
                    " " + CalculateMaxRows() + ")",
                gridSettingValueFont,
                blackBrush,
                rowCountValuePosition
            );

            screenPainter.DrawString(
                gridColumns + " (" + Properties.Resources.Max +
                    " " + CalculateMaxColumns() + ")",
                gridSettingValueFont,
                blackBrush,
                columnCountValuePosition
            );

            screenPainter.DrawString(
                gridCellSize + "px (" + Properties.Resources.Max +
                    " " + CalculateMaxCellSize() + "px)",
                gridSettingValueFont,
                blackBrush,
                cellSizeValuePosition
            );

            screenPainter.DrawString(
                lifeChance + "%",
                gridSettingValueFont,
                blackBrush,
                lifeChanceValuePosition
            );
        }





        /*
         * POPUP CODE
         */

        private struct PopupMessage
        {
            public String Text;
            public Font Style;
            public SolidBrush Color;
        }

        private struct Alignment
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
        /// Will draw a popup on the screen with a shaded background over the existing
        /// screen elements and a bright background beneath the popup message.
        /// </summary>
        /// <param name="Messages">An array of messages displayed in the popup.</param>
        /// <param name="PopupAlignment">Where the popup will be drawn.</param>
        /// <param name="MessageMargin">The margin between the messages and the edge of the popup.</param>
        /// <param name="MessageSpacing">The space between each individual message.</param>
        private void DrawPopup(PopupMessage[] Messages, Alignment PopupAlignment, SizeF MessageMargin, float MessageSpacing)
        {
            SizeF[] MessageSize = new SizeF[Messages.Length];
            SizeF TotalMessageSize = new SizeF();
            SizeF PopupSize = new SizeF();
            PointF[] MessagePosition = new PointF[Messages.Length];
            PointF PopupPosition = new PointF();

            // Calculate sizes of all the messages and total size of all the messages.
            for (int i = 0; i < Messages.Length; i++ )
            {
                MessageSize[i] = screenPainter.MeasureString(Messages[i].Text, Messages[i].Style);
                TotalMessageSize.Width = Math.Max(MessageSize[i].Width, TotalMessageSize.Width);
                TotalMessageSize.Height += MessageSize[i].Height;
            }
            TotalMessageSize.Height += MessageSpacing * (Messages.Length - 1);

            // Calculate the size of the popup.
            PopupSize.Width = TotalMessageSize.Width + (MessageMargin.Width * 2);
            PopupSize.Height = TotalMessageSize.Height + (MessageMargin.Height * 2);

            // Calculate the X position of the popup.
            if (PopupAlignment.PopupHorizontalAlignment == HorizontalAlignment.Left)
            {
                PopupPosition.X = 0;
            }
            else if (PopupAlignment.PopupHorizontalAlignment == HorizontalAlignment.Right)
            {
                PopupPosition.X = this.Width - PopupSize.Width;
            }
            else // Center
            {
                PopupPosition.X = (this.Width / 2) - (PopupSize.Width / 2);
            }

            // Calculate the Y position of the popup.
            if (PopupAlignment.PopupVerticalAlignment == VerticalAlignment.Top)
            {
                PopupPosition.Y = 0;
            }
            else if (PopupAlignment.PopupVerticalAlignment == VerticalAlignment.Bottom)
            {
                PopupPosition.Y = this.Height - PopupSize.Height;
            }
            else // Middle
            {
                PopupPosition.Y = (this.Height / 2) - (PopupSize.Height / 2);
            }

            // Calculate the positions of the messages.
            for (int i = 0; i < Messages.Length; i++)
            {
                // Horizontal position
                if (PopupAlignment.MessageHorizontalAlignment == HorizontalAlignment.Left)
                {
                    MessagePosition[i].X = PopupPosition.X + MessageMargin.Width;
                }
                else if (PopupAlignment.MessageHorizontalAlignment == HorizontalAlignment.Right)
                {
                    MessagePosition[i].X = (PopupPosition.X + PopupSize.Width) -
                        (MessageSize[i].Width + MessageMargin.Width);
                }
                else // Center
                {
                    MessagePosition[i].X = PopupPosition.X + (PopupSize.Width / 2) -
                        (MessageSize[i].Width / 2);
                }
                // Vertical position
                if (i == 0)
                {
                    MessagePosition[i].Y = PopupPosition.Y + MessageMargin.Height;
                }
                else
                {
                    MessagePosition[i].Y = MessagePosition[i - 1].Y + MessageSize[i - 1].Height +
                        MessageSpacing;
                }
            }

            // Darken background
            screenPainter.FillRectangle(shadeBrush, 0, 0, this.Width, this.Height);

            // DrawGrid popup background
            screenPainter.FillRectangle(
                backgroundBrush, 
                PopupPosition.X, 
                PopupPosition.Y, 
                PopupSize.Width, 
                PopupSize.Height
            );

            // DrawGrid messages
            for (int i = 0; i < Messages.Length; i++)
            {
                screenPainter.DrawString(
                    Messages[i].Text, 
                    Messages[i].Style, 
                    Messages[i].Color, 
                    MessagePosition[i]);
            }
        }

        private void DrawExitConfirmation()
        {
            // Build message array
            PopupMessage[] messages = new PopupMessage[2];
            messages[0].Text = Properties.Resources.ExitConfirmation;
            messages[0].Style = promptFont;
            messages[0].Color = blackBrush;
            messages[1].Text = Properties.Resources.ExitCancel;
            messages[1].Style = subPromptFont;
            messages[1].Color = blackBrush;

            // Specify alignments
            Alignment alignment = new Alignment();
            alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            // Specify margins
            SizeF margins = new SizeF(20, 20);

            DrawPopup(messages, alignment, margins, 5);
        }

        private void DrawHelpScreen()
        {
            // Build message array
            PopupMessage[] messages = new PopupMessage[16];
            messages[0].Text = Properties.Resources.HelpTitle;
            messages[0].Style = promptFont;
            messages[0].Color = blackBrush;
            messages[1].Text = Properties.Resources.HelpMainControls;
            messages[1].Style = subPromptFont;
            messages[1].Color = blackBrush;
            messages[2].Text = Properties.Resources.HelpExitGame;
            messages[2].Style = promptFont;
            messages[2].Color = blackBrush;
            messages[3].Text = Properties.Resources.HelpNextStep;
            messages[3].Style = promptFont;
            messages[3].Color = blackBrush;
            messages[4].Text = Properties.Resources.HelpAutoStep;
            messages[4].Style = promptFont;
            messages[4].Color = blackBrush;
            messages[5].Text = Properties.Resources.HelpEndGame;
            messages[5].Style = promptFont;
            messages[5].Color = blackBrush;
            messages[6].Text = Properties.Resources.HelpShowCredits;
            messages[6].Style = promptFont;
            messages[6].Color = blackBrush;
            messages[7].Text = Properties.Resources.HelpBeforeGame;
            messages[7].Style = subPromptFont;
            messages[7].Color = blackBrush;
            messages[8].Text = Properties.Resources.HelpIncreaseRows;
            messages[8].Style = promptFont;
            messages[8].Color = blackBrush;
            messages[9].Text = Properties.Resources.HelpDecreaseRows;
            messages[9].Style = promptFont;
            messages[9].Color = blackBrush;
            messages[10].Text = Properties.Resources.HelpIncreaseColumns;
            messages[10].Style = promptFont;
            messages[10].Color = blackBrush;
            messages[11].Text = Properties.Resources.HelpDecreaseColumns;
            messages[11].Style = promptFont;
            messages[11].Color = blackBrush;
            messages[12].Text = Properties.Resources.HelpIncreaseCellSize;
            messages[12].Style = promptFont;
            messages[12].Color = blackBrush;
            messages[13].Text = Properties.Resources.HelpDecreaseCellSize;
            messages[13].Style = promptFont;
            messages[13].Color = blackBrush;
            messages[14].Text = Properties.Resources.HelpIncreaseLifeChance;
            messages[14].Style = promptFont;
            messages[14].Color = blackBrush;
            messages[15].Text = Properties.Resources.HelpDecreaseLifeChance;
            messages[15].Style = promptFont;
            messages[15].Color = blackBrush;

            // Specify alignments
            Alignment alignment = new Alignment();
            alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            alignment.MessageHorizontalAlignment = HorizontalAlignment.Left;

            // Specify margins
            SizeF margins = new SizeF(20, 20);

            DrawPopup(messages, alignment, margins, 5);
        }

        private void DrawCreditsScreen()
        {
            // Build message array
            PopupMessage[] messages = new PopupMessage[2];
            messages[0].Text = Properties.Resources.HelpTitle;
            messages[0].Style = promptFont;
            messages[0].Color = blackBrush;
            messages[1].Text = Properties.Resources.HelpMainControls;
            messages[1].Style = subPromptFont;
            messages[1].Color = blackBrush;
            
            // Specify alignments
            Alignment alignment = new Alignment();
            alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            alignment.PopupVerticalAlignment = VerticalAlignment.Middle;
            alignment.MessageHorizontalAlignment = HorizontalAlignment.Left;

            // Specify margins
            SizeF margins = new SizeF(20, 20);

            DrawPopup(messages, alignment, margins, 5);
        }

        private void DrawCompletionMessage()
        {
            // Build message array
            PopupMessage[] messages = new PopupMessage[1];
            if (Life.Extinction)
            {
                messages[0].Text = Properties.Resources.GameExtinction;
                messages[0].Color = extinctionBrush;
            }
            else if (Life.Stabilization)
            {
                messages[0].Text = Properties.Resources.GameStabilization;
                messages[0].Color = stabilizationBrush;
            }
            messages[0].Style = gameCompleteFont;

            // Specify alignments
            Alignment alignment = new Alignment();
            alignment.PopupHorizontalAlignment = HorizontalAlignment.Center;
            alignment.PopupVerticalAlignment = VerticalAlignment.Bottom;
            alignment.MessageHorizontalAlignment = HorizontalAlignment.Center;

            // Specify margins
            SizeF margins = new SizeF(20, 20);

            DrawPopup(messages, alignment, margins, 5);
        }

        #endregion DrawGrid Screen Element Methods
    }
}
