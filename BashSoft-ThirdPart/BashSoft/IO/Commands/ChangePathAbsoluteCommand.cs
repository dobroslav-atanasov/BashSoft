namespace BashSoft.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Alias("cdabs")]
    public class ChangePathAbsoluteCommand : Command
    {
        [Inject]
        private IDirectoryManager manager;

        public ChangePathAbsoluteCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string absolutePath = this.Data[1];
            this.manager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    }
}