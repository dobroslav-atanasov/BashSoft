namespace BashSoft.Exceptions
{
    using System;

    public class CourseNotFoundException : Exception
    {
        private const string CourseNotFound = "Course does not exist.";

        public CourseNotFoundException()
            : base(CourseNotFound)
        {
        }

        public CourseNotFoundException(string message)
            : base(message)
        {
        }
    }
}