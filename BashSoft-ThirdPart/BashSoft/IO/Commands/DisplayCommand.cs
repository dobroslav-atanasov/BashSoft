namespace BashSoft.IO.Commands
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using Contracts;
    using Exceptions;

    [Alias("display")]
    public class DisplayCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
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
                    ISimpleOrderedBag<IStudent> studentList = this.repository.GetAllStudentsSorted(studentComparator);
                    OutputWriter.DisplayStudentMessage(studentList.JoinWith(Environment.NewLine));
                    break;
                case "courses":
                    IComparer<ICourse> courseComparator = this.CreateCourseComparator(sortType);
                    ISimpleOrderedBag<ICourse> courseList = this.repository.GetAllCoursesSorted(courseComparator);
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