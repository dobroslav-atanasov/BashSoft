﻿namespace BashSoft.IO.Commands
{
    using System.Diagnostics;
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;
    using StaticData;

    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, IContentComparer tester, IDatabase repository,
            IDirectoryManager manager)
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = SessionData.currentPath + "\\" + fileName;
            process.Start();
        }
    }
}