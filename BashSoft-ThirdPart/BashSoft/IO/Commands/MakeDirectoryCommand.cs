namespace BashSoft.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command
    {
        [Inject]
        private IDirectoryManager manager;

        public MakeDirectoryCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string directoryName = this.Data[1];
            this.manager.CreateDirectoryInCurrentFolder(directoryName);
        }
    }
}