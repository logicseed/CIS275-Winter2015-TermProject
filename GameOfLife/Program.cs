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
        // Program-state fields
        private bool isAutoStepping = false;
        private bool hasShownSplash = false;

        // Additional program-state fields that monitor the state
        // of certain fields that require calculations. The calculations
        // are only performed if the they are considered invalid due to
        // other program changes.
        // Example: Once the game starts, the grid cannot be resized so
        // it is safe to use the last known GridSize and GridPosition.
        private bool isGridSizeValid = false;
        private bool isGridPositionValid = false;

        // Matrix fields
        private byte rows = 20;
        private byte columns = 30;
        private byte cellSize = 20;
        private byte lifeChance = 50;

        private const int SPLASH_HEIGHT = 600;
        private const int SPLASH_WIDTH = 800;

        private LifeManager lifeManager = new LifeManager();

        private PrivateFontCollection customFonts = new PrivateFontCollection();

        // The screen buffer is used to build the screen before displaying it.
        private Bitmap screenBuffer;

        private Size gridSize;
        private PointF gridPosition;

        private Size GridSize
        {
            get
            {
                if(!isGridSizeValid)
                {
                    // Calculate the size of the grid.
                    // We want a pixel between each cell and a pixel surrounding
                    // all the cells.
                    gridSize = new Size(
                        (cellSize * columns) + columns, // Width
                        (cellSize * rows) + rows // Height
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
                if(!isGridPositionValid)
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

        

        public Program()
        {
            InitializeComponent();
            Cursor.Hide();
            InitializeColors();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);

            // Load custom fonts.
            customFonts.AddFontFile("GameOfLife-Regular.ttf");
            customFonts.AddFontFile("GameOfLife-Bold.ttf");

            // Initialize the screen buffer bitmap.
            screenBuffer = new Bitmap(this.Width, this.Height);

        }

        

        private void Program_Paint(object sender, PaintEventArgs e)
        {

            // Have everything draw into a buffer and return it.
            // This should prevent the flickering and also make it so 
            // we don't have to send the arguments along for the trip.

            



            if(!hasShownSplash)
            {
                DrawSplashScreen(sender, e);
            }
            else
            {
                DrawMainScreen(sender, e);
            }
        }

        private void DrawSplashScreen(object sender, PaintEventArgs e)
        {
            //Graphics g = Graphics.FromImage(screenBuffer);
            Graphics g = e.Graphics;

            // Draw background.
            g.FillRectangle(uiBrush[2], 0, 0, this.Width, this.Height);

            // Draw splash screen graphic.
            Bitmap splashImage = new Bitmap(Properties.Resources.splash);
            g.DrawImage(splashImage,
                (this.Width-SPLASH_WIDTH)/2,
                (this.Height-SPLASH_HEIGHT)/2);
            splashImage.Dispose();

            // Draw press-any-key text.
            //g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            //String text = "PRESS ANY KEY TO CONTINUE";
            String text = Properties.Resources.SplashScreen;
            Font font = new Font(customFonts.Families[0], 16, FontStyle.Bold);
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, uiBrush[0],
                (this.Width - size.Width) / 2,
                (this.Height - size.Height));


        }

        private void DrawMainScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(uiBrush[1], 0, 0,
                this.Width, this.Height);

            DrawGrid(sender, e);
            if(lifeManager.GameRunning)
            {
                DrawLife(sender, e);
            }
        }

        private void DrawGrid(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw surrounding rectangle.
            e.Graphics.DrawRectangle(cellPen, GridPosition.X, GridPosition.Y,
                GridSize.Width, GridSize.Height);

            // Draw horizontal lines.
            for(int i = 1; i < rows; i++)
            {
                e.Graphics.DrawLine(cellPen, 
                    GridPosition.X,
                    GridPosition.Y + i + (i * cellSize),
                    GridPosition.X + GridSize.Width,
                    GridPosition.Y + i + (i * cellSize));
            }

            // Draw vertical lines.
            for (int i = 1; i < columns; i++)
            {
                e.Graphics.DrawLine(cellPen, 
                    GridPosition.X + i + (i * cellSize),
                    GridPosition.Y,
                    GridPosition.X + i + (i * cellSize),
                    GridPosition.Y + GridSize.Height);
            }
        }

        private void DrawLife(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw life rectangles
            for(int i = 1; i <= rows; i++)
            {
                for(int j = 1; j <= columns; j++)
                {
                    if (lifeManager.GetElement(i, j) > 0)
                    {
                        g.FillRectangle(lifeBrush[lifeManager.GetElement(i, j)],
                            GridPosition.X + ((cellSize+1) * (j-1)),
                            GridPosition.Y + ((cellSize+1) * (i-1)),
                            cellSize, cellSize);
                    }
                }
            }
        }

        private void InvalidateGrid()
        {
            isGridSizeValid = false;
            isGridPositionValid = false;
        }
    }
}
