namespace BashSoft.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Exceptions;
    using IO;

    public class RepositorySorter : IDataSorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                this.PrintStudents(studentsWithMarks.OrderBy(s => s.Value)
                    .Take(studentsToTake)
                    .ToDictionary(st => st.Key, st => st.Value));
            }
            else if (comparison == "descending")
            {
                this.PrintStudents(studentsWithMarks.OrderByDescending(s => s.Value)
                    .Take(studentsToTake)
                    .ToDictionary(st => st.Key, st => st.Value));
            }
            else
            {
                throw new InvalidStudentSorterException();
            }
        }

        private void PrintStudents(Dictionary<string, double> sortedStudents)
        {
            foreach (KeyValuePair<string, double> student in sortedStudents)
            {
                OutputWriter.PrintStudent(student);
            }
        }
    }
}