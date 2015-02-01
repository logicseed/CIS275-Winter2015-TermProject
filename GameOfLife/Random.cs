/*
 * 
 * The Game of Life
 * 
 * Copyright (C) 2015 Marc King <mjking@umich.edu>
 * 
 */

using System;

namespace GameOfLife
{
    /// <summary>
    /// This is a static class so that random numbers can be generated
    /// without instantiated the Random class in other code.
    /// </summary>
    static class Random
    {
        static System.Random RandomObject = new System.Random();
        static Object threadLock = new object();

        /// <summary>
        /// Returns a random integer from min to max.
        /// </summary>
        /// <param name="min">The lowest possible random integer.</param>
        /// <param name="max">The highest possible random integer.</param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            lock (threadLock)
            {
                return RandomObject.Next(min, max + 1);
            }
        }
    }
}
