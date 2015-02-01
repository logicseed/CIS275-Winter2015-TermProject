/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

namespace GameOfLife
{
    internal static class Setting
    {
        public static int Rows = 1;
        public static int Columns = 1;
        public static int CellSize = 50;
        public static int LifeChance = 50;
        public const int MinimumRows = 3;
        public const int MinimumColumns = 3;
        public const int MinimumCellSize = 5;
        public const int MinimumLifeChance = 5;
        public const int MaximumLifeChance = 95;
    }
}
