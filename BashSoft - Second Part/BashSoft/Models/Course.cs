namespace BashSoft.Models
{
    using System.Collections.Generic;
    using IO;
    using StaticData;

    public class Course
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public string name;
        public Dictionary<string, Student> studentsByName;

        public Course(string name)
        {
            this.name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

        public void EnrollStudent(Student student)
        {
            if (this.studentsByName.ContainsKey(student.username))
            {
                OutputWriter.DisplayMessage(string.Format(ExceptionMessages.StudentAlreadyEnrollInGivenCourse, student.username, this.name));
                return;
            }
            this.studentsByName.Add(student.username, student);
        }
    }
}