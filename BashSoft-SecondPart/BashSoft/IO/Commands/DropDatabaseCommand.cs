namespace BashSoft.IO.Commands
{
    using Exceptions;
    using Judge;
    using Repository;

    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, Tester tester, StudentRepository repository, IOManager manager)
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.Repository.UnloadData();
            OutputWriter.DisplaySuccessfulMessage("Database dropped!");
        }
    }
}