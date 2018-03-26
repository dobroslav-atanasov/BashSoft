namespace BashSoft.Exceptions
{
    using System;

    public class AccessException : Exception
    {
        private const string UnauthorizedAccess =
            "The folder/file you are trying to get access needs a higher level of rights than you currently have.";

        public AccessException()
            : base(UnauthorizedAccess)
        {
        }

        public AccessException(string message)
            : base(message)
        {
        }
    }
}