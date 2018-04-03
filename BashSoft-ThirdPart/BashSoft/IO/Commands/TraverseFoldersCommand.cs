namespace BashSoft.IO.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;
    using StaticData;

    [Alias("ls")]
    public class TraverseFoldersCommand : Command
    {
        [Inject]
        private IDirectoryManager manager;

        public TraverseFoldersCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1)
            {
                this.manager.TraverseFolder(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(this.Data[1], out depth);
                if (hasParsed)
                {
                    this.manager.TraverseFolder(depth);
                }
                else
                {
                    OutputWriter.DisplayMessage(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}