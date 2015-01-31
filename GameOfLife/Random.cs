/*
 * The Game of Life - Marc King
 * Programmed for CIS275 - Winter 2015
 * 
 * Random.cs
 * 
 * Allows static access to the System.Random class.
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
