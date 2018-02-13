namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using IO;
    using StaticData;

    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData(string fileName)
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        private static void ReadData(string fileName)
        {
            string pattern = @"([A-Z][a-zA-z+#]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)";

            string path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                string[] allInputLines = File.ReadAllLines(path);

                for (int i = 0; i < allInputLines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[i]) && Regex.IsMatch(allInputLines[i], pattern))
                    {
                        Match match = Regex.Match(allInputLines[i], pattern);
                        string course = match.Groups[1].Value;
                        string student = match.Groups[2].Value;
                        int studentScore;
                        bool hasParseStudentScore = int.TryParse(match.Groups[3].Value, out studentScore);

                        if (hasParseStudentScore && studentScore >= 0 && studentScore <= 100)
                        {
                            if (!studentsByCourse.ContainsKey(course))
                            {
                                studentsByCourse[course] = new Dictionary<string, List<int>>();
                            }
                            if (!studentsByCourse[course].ContainsKey(student))
                            {
                                studentsByCourse[course][student] = new List<int>();
                            }
                            studentsByCourse[course][student].Add(studentScore);
                        }
                    }
                }
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidPath);
                return;
            }

            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            return false;
        }

        private static bool IsQueryForStudentPossible(string courseName, string studentName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentName))
            {
                return true;
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (KeyValuePair<string, List<int>> student in studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(student);
                }
            }
        }

        public static void GetStudentsScoresFromCourse(string courseName, string studentName)
        {
            if (IsQueryForStudentPossible(courseName, studentName))
            {
                OutputWriter.PrintStudent(
                    new KeyValuePair<string, List<int>>(studentName, studentsByCourse[courseName][studentName]));
            }
        }

        public static void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                RepositoryFilters.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);
            }
        }

        public static void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                RepositorySorters.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);
            }
        }
    }
}