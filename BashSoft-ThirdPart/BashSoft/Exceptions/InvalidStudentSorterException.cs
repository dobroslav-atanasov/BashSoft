namespace BashSoft.Exceptions
{
    using System;

    public class InvalidStudentSorterException : Exception
    {
        private const string InvalidStudentSorter = "The given sorter is not one of the following: ascending/descending";

        public InvalidStudentSorterException()
            : base(InvalidStudentSorter)
        {
        }

        public InvalidStudentSorterException(string message)
            : base(message)
        {
        }
    }
}