namespace BashSoft.IO.Commands
{
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;

    public class ChangePathRelativelyCommand : Command
    {
        public ChangePathRelativelyCommand(string input, string[] data, IContentComparer tester, IDatabase repository, IDirectoryManager manager) 
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);

            }

            string relativePath = this.Data[1];
            this.Manager.ChangeCurrentDirectoryRelative(relativePath);
        }
    }
}