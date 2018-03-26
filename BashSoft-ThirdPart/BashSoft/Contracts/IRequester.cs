﻿namespace BashSoft.Contracts
{
    using System.Collections.Generic;

    public interface IRequester
    {
        void GetStudentsScoresFromCourse(string courseName, string studentName);

        void GetAllStudentsFromCourse(string courseName);

        void GetAllCourses();

        void GetAllStudents();

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);

        ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp);
    }
}