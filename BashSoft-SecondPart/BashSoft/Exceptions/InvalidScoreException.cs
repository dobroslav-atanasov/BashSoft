namespace BashSoft.Exceptions
{
    using System;

    public class InvalidScoreException : Exception
    {
        private const string InvalidScore = "The number for the score you've entered is not in the range of 0 - 100.";

        public InvalidScoreException()
            : base(InvalidScore)
        {
        }

        public InvalidScoreException(string message)
            : base(message)
        {
        }
    }
}