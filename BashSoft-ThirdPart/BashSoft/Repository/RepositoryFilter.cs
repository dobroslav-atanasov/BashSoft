namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;
    using IO;

    public class RepositoryFilter : IDataFilter
    {
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string filter, int studentsToTake)
        {
            if (filter == "excellent")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
            }
            else if (filter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (filter == "poor")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 3.5, studentsToTake);
            }
            else
            {
                throw new InvalidStudentFilterException();
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counter = 0;
            foreach (KeyValuePair<string, double> studentMark in studentsWithMarks)
            {
                if (counter == studentsToTake)
                {
                    break;
                }
                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counter++;
                }
            }
        }
    }
}