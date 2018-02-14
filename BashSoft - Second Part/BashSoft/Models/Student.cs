namespace BashSoft.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using IO;
    using StaticData;

    public class Student
    {
        private string username;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;
        
        public Student(string username)
        {
            this.Username = username;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string Username
        {
            get { return this.username; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(this.username), ExceptionMessages.NullOrEmptyValue);
                }
                this.username = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses
        {
            get { return this.enrolledCourses; }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get { return this.marksByCourseName; }
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                // Change the Exception
                throw new Exception();
            }
            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                // Change the Exception
                throw new Exception();
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                // Change the Exception
                throw new Exception();
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