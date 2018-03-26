namespace BashSoft.Exceptions
{
    using System;

    public class DataException : Exception
    {
        private const string DataNotInitialized =
            "The data structure must be initialized first in order to make any operations with it.";

        public DataException()
            : base(DataNotInitialized)
        {
        }

        public DataException(string message)
            : base(message)
        {
        }
    }
}