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
        /// <summary>
        /// This bit vector holds a bit string of states for the program.
        /// States are primarily used to indicate to the program which
        /// interface elements to paint and which keys to accept.
        /// </summary>
        private BitVector32 State = new BitVector32(0);

        // Creates masks for each of the state bit flags
        private static int GameRunning = BitVector32.CreateMask();
        private static int AutoStepping = BitVector32.CreateMask(GameRunning);
        private static int ShowSplash = BitVector32.CreateMask(AutoStepping);
        private static int ShowConfirmExit = BitVector32.CreateMask(ShowSplash);
        private static int ShowHelp = BitVector32.CreateMask(ShowConfirmExit);
        private static int ShowCredits = BitVector32.CreateMask(ShowHelp);
        private static int FirstDisplay = BitVector32.CreateMask(ShowCredits);

        private void InitializeStates()
        {
            State[ShowSplash] = true;
        }
    }
}
