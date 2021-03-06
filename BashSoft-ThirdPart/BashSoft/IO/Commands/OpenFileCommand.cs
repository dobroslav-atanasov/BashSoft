﻿namespace BashSoft.IO.Commands
{
    using System.Diagnostics;
    using Attributes;
    using Exceptions;
    using StaticData;

    [Alias("open")]
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data)
            : base(input, data)
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