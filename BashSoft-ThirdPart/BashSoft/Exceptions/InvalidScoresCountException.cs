namespace BashSoft.Exceptions
{
    using System;

    public class InvalidScoresCountException : Exception
    {
        private const string InvalidNumberOfScores =
            "The number of scores for the given course is greater than the possible.";

        public InvalidScoresCountException()
            : base(InvalidNumberOfScores)
        {
        }

        public InvalidScoresCountException(string message)
            : base(message)
        {
        }
    }
}