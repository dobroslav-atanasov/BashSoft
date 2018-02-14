namespace BashSoft.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using StaticData;

    public class Student
    {
        public string username;
        public Dictionary<string, Course> enrolledCourses;
        public Dictionary<string, double> marksByCourseName;
        
        public Student(string username)
        {
            this.username = username;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.name))
            {
                OutputWriter.DisplayMessage(string.Format(ExceptionMessages.StudentAlreadyEnrollInGivenCourse, this.username, course.name));
                return;
            }
            this.enrolledCourses.Add(course.name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayMessage(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidNumberOfScores);
                return;
            }

            this.marksByCourseName.Add(courseName, this.CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentage = scores.Sum() / (double) (Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            double mark = percentage * 4 + 2;
            return mark;
        }
    }
}