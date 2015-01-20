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
        private bool isAutoStepping = false;
        private bool hasShownSplash = false;

        private byte rows = 20;
        private byte columns = 30;
        private byte cellSize = 20;

        private const int SPLASH_HEIGHT = 600;
        private const int SPLASH_WIDTH = 800;

        private LifeManager lifeManager = new LifeManager();

        private PrivateFontCollection customFonts = new PrivateFontCollection();

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

        }

        

        private void Program_Paint(object sender, PaintEventArgs e)
        {
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

            // Calculate the size of the grid.
            // We want a pixel between each cell and a pixel surrounding
            // all the cells.
            int gridWidth = (cellSize * columns) // Space for life
                + columns; // Space for borders
            int gridHeight = (cellSize * rows) // Space for life
                + rows; // Space for borders

            // Calculate grid position.
            Point gridPosition = new Point(
                (this.Width - gridWidth)/2,
                (this.Height - gridHeight)/2);

            // Draw surrounding rectangle.
            e.Graphics.DrawRectangle(cellPen, gridPosition.X, gridPosition.Y,
                gridWidth, gridHeight);

            // Draw horizontal lines.
            for(int i = 1; i < rows; i++)
            {
                e.Graphics.DrawLine(cellPen, 
                    gridPosition.X,
                    gridPosition.Y + i + (i * cellSize),
                    gridPosition.X + gridWidth,
                    gridPosition.Y + i + (i * cellSize));
            }

            // Draw vertical lines.
            for (int i = 1; i < columns; i++)
            {
                e.Graphics.DrawLine(cellPen, 
                    gridPosition.X + i + (i * cellSize),
                    gridPosition.Y,
                    gridPosition.X + i + (i * cellSize),
                    gridPosition.Y + gridHeight);
            }
        }

        private void DrawLife(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Calculate the size of the grid.
            // We want a pixel between each cell and a pixel surrounding
            // all the cells.
            int gridWidth = (cellSize * columns) // Space for life
                + columns; // Space for borders
            int gridHeight = (cellSize * rows) // Space for life
                + rows; // Space for borders

            // Calculate grid position.
            Point gridPosition = new Point(
                (this.Width - gridWidth) / 2,
                (this.Height - gridHeight) / 2);

            // Draw life rectangles
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if (lifeManager.GetElement(i+1, j+1) > 0)
                    {
                        g.FillRectangle(lifeBrush[lifeManager.GetElement(i+1, j+1)],
                            gridPosition.X + 1 + ((cellSize+1) * j),
                            gridPosition.Y + 1 + ((cellSize+1) * i),
                            cellSize, cellSize);
                    }
                }
            }
        }
    }
}
