using System;
using ButtonGrid_Git.Constants;

namespace ButtonGrid_Git.Utility.RandomNumber
{
    public static class RandomNumberGenerator
    {
        static Random nextRandomNumber = new Random();

        public static int GetNextRandomNumberBetween1and5()
        {
            return nextRandomNumber.Next(1, 6);
        }

        public static int GetNextRandomNumberBetween0andSQUARE_SIDE_LENGTH()
        {
            return nextRandomNumber.Next(0, Convert.ToInt32(VariableConstants.SQUARE_SIDE_LENGTH));
        }
    }
}
