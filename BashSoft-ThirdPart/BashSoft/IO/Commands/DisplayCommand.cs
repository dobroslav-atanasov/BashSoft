namespace BashSoft.IO.Commands
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;

    public class DisplayCommand : Command
    {
        public DisplayCommand(string input, string[] data, IContentComparer tester, IDatabase repository,
            IDirectoryManager manager)
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string entityToDisplay = this.Data[1].ToLower();
            string sortType = this.Data[2].ToLower();

            switch (entityToDisplay)
            {
                case "students":
                    IComparer<IStudent> studentComparator = this.CreateStudentComparator(sortType);
                    ISimpleOrderedBag<IStudent> studentList = this.Repository.GetAllStudentsSorted(studentComparator);
                    OutputWriter.DisplayStudentMessage(studentList.JoinWith(Environment.NewLine));
                    break;
                case "courses":
                    IComparer<ICourse> courseComparator = this.CreateCourseComparator(sortType);
                    ISimpleOrderedBag<ICourse> courseList = this.Repository.GetAllCoursesSorted(courseComparator);
                    OutputWriter.DisplayCourseMessage(courseList.JoinWith(Environment.NewLine));
                    break;
                default:
                    throw new InvalidCommandException(this.Input);
            }
        }

        private IComparer<IStudent> CreateStudentComparator(string sortType)
        {
            switch (sortType)
            {
                case "ascending":
                    return Comparer<IStudent>.Create((firstStudent, secondStudent) =>
                        firstStudent.CompareTo(secondStudent));
                case "descending":
                    return Comparer<IStudent>.Create((firstStudent, secondStudent) =>
                        secondStudent.CompareTo(firstStudent));
                default:
                    throw new InvalidCommandException(this.Input);
            }
        }

        private IComparer<ICourse> CreateCourseComparator(string sortType)
        {
            switch (sortType)
            {
                case "ascending":
                    return Comparer<ICourse>.Create((firstCourse, secondCourse) => firstCourse.CompareTo(secondCourse));
                case "descending":
                    return Comparer<ICourse>.Create((firstCourse, secondCourse) => secondCourse.CompareTo(firstCourse));
                default:
                    throw new InvalidCommandException(this.Input);
            }
        }
    }
}