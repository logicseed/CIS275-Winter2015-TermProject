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
        // Program-state fields
        private bool isAutoStepping = false;
        private bool hasShownSplash = false;

        private bool isConfirmingExit = false;

        private bool Increase = true;
        private bool Decrease = false;
    }
}
