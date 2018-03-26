namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetStudentsScoresFromCourse(string courseName, string studentName);

        void GetAllStudentsFromCourse(string courseName);

        void GetAllCourses();

        void GetAllStudents();
    }
}