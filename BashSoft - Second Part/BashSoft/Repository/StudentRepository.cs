namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using IO;
    using Models;
    using StaticData;

    public class StudentRepository
    {
        private bool isDataInitialized;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;
        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            this.courses = new Dictionary<string, Course>();
            this.students = new Dictionary<string, Student>();
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.courses = null;
            this.students = null;
            this.isDataInitialized = false;
        }

        private void ReadData(string fileName)
        {
            string pattern = @"([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";

            string path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                string[] allInputLines = File.ReadAllLines(path);

                for (int i = 0; i < allInputLines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[i]) && Regex.IsMatch(allInputLines[i], pattern))
                    {
                        Match match = Regex.Match(allInputLines[i], pattern);
                        string courseName = match.Groups[1].Value;
                        string username= match.Groups[2].Value;
                        string scoresString = match.Groups[3].Value;

                        try
                        {
                            int[] scores = scoresString.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                            if (scores.Any(s => s > 100 || s < 0))
                            {
                                //OutputWriter.DisplayMessage(ExceptionMessages.InvalidScore);
                                throw new ArgumentException(ExceptionMessages.InvalidScore);
                            }
                            if (scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayMessage(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(username))
                            {
                                this.students.Add(username, new Student(username));
                            }
                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.students[username];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);
                            course.EnrollStudent(student);
                        }
                        catch (FormatException fe)
                        {
                            OutputWriter.DisplayMessage(fe.Message + $"at line: {i}");
                        }
                    }
                }
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidPath);
                return;
            }

            this.isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    //OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
                    throw new ArgumentException(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                //OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentName)
        {
            if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentName))
            {
                return true;
            }
            else
            {
                //OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
                throw new ArgumentException(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (KeyValuePair<string, Student> studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentsScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public void GetStudentsScoresFromCourse(string courseName, string studentName)
        {
            if (this.IsQueryForStudentPossible(courseName, studentName))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentName, this.courses[courseName].StudentsByName[studentName].MarksByCourseName[courseName]));
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName
                    .ToDictionary(st => st.Key, st => st.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName
                    .ToDictionary(st => st.Key, st => st.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}