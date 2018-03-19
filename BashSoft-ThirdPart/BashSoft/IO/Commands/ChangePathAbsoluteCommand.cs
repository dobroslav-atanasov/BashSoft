namespace BashSoft.IO.Commands
{
    using Exceptions;
    using Judge;
    using Repository;

    public class ChangePathAbsoluteCommand : Command
    {
        public ChangePathAbsoluteCommand(string input, string[] data, Tester tester, StudentRepository repository, IOManager manager) 
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string absolutePath = this.Data[1];
            this.Manager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    }
}