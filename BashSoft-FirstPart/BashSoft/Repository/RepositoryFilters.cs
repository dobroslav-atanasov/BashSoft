namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using StaticData;

    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> database, string filter, int studentsToTake)
        {
            if (filter == "excellent")
            {
                FilterAndTake(database, x => x >= 5, studentsToTake);
            }
            else if (filter == "average")
            {
                FilterAndTake(database, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (filter == "poor")
            {
                FilterAndTake(database, x => x < 3.5, studentsToTake);
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private static void FilterAndTake(Dictionary<string, List<int>> database, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counter = 0;
            foreach (KeyValuePair<string, List<int>> student in database)
            {
                if (counter == studentsToTake)
                {
                    break;
                }

                double averageScore = student.Value.Average();
                double percentage = averageScore / 100.0;
                double mark = percentage * 4 + 2;
                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(student);
                    counter++;
                }
            }
        }
    }
}