﻿namespace BashSoft.Models
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;

    public class Course : ICourse
    {
        public const int NumberOfTasksOnExam = 7;
        public const int MaxScoreOnExamTask = 100;

        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public Course(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get { return this.studentsByName; }
        }

        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, this.Name);
            }
            this.studentsByName.Add(student.Username, student);
        }

        public int CompareTo(ICourse other)
        {
            int result = this.Name.CompareTo(other.Name);
            return result;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}