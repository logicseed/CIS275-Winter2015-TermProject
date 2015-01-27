using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    internal static class Setting
    {
        public static int Rows = 1;
        public static int Columns = 1;
        public static int CellSize = 50;
        public static int LifeChance = 50;
        public static int MinimumRows = 3;
        public static int MinimumColumns = 3;
        public static int MinimumCellSize = 5;
        public static int MinimumLifeChance = 5;
        public static int MaximumLifeChance = 95;
    }
}
