namespace BashSoft.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using StaticData;

    public class RepositorySorter
    {
        //public void OrderAndTake(Dictionary<string, List<int>> database, string comparison, int studentsToTake)
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
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidComparisonQuery);
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