namespace BashSoft.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using StaticData;

    public static class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> database, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                PrintStudents(database.OrderBy(s => s.Value.Sum()).Take(studentsToTake)
                    .ToDictionary(st => st.Key, st => st.Value));
            }
            else if (comparison == "descending")
            {
                PrintStudents(database.OrderByDescending(s => s.Value.Sum()).Take(studentsToTake)
                    .ToDictionary(st => st.Key, st => st.Value));
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static void PrintStudents(Dictionary<string, List<int>> sortedStudents)
        {
            foreach (KeyValuePair<string, List<int>> student in sortedStudents)
            {
                OutputWriter.PrintStudent(student);
            }
        }
    }
}