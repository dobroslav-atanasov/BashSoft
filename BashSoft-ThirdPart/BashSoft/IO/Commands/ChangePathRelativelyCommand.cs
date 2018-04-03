namespace BashSoft.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Alias("cdrel")]
    public class ChangePathRelativelyCommand : Command
    {
        [Inject]
        private IDirectoryManager manager;

        public ChangePathRelativelyCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string relativePath = this.Data[1];
            this.manager.ChangeCurrentDirectoryRelative(relativePath);
        }
    }
}