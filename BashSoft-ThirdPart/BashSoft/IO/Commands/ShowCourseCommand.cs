namespace BashSoft.IO.Commands
{
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;

    public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, IContentComparer tester, IDatabase repository,
            IDirectoryManager manager)
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.Repository.GetAllStudentsFromCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string studentName = this.Data[2];
                this.Repository.GetStudentsScoresFromCourse(courseName, studentName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}