namespace BashSoft.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Alias("show")]
    public class ShowCourseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ShowCourseCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string studentName = this.Data[2];
                this.repository.GetStudentsScoresFromCourse(courseName, studentName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}