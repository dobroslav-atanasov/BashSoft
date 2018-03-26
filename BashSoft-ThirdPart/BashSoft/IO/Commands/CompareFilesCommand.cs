﻿namespace BashSoft.IO.Commands
{
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer tester, IDatabase repository,
            IDirectoryManager manager)
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string firstPath = this.Data[1];
            string secondPath = this.Data[2];
            this.Tester.CompareContent(firstPath, secondPath);
        }
    }
}