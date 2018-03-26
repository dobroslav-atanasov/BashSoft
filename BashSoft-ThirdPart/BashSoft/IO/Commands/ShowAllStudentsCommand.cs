﻿namespace BashSoft.IO.Commands
{
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;

    public class ShowAllStudentsCommand : Command
    {
        public ShowAllStudentsCommand(string input, string[] data, IContentComparer tester, IDatabase repository, IDirectoryManager manager) 
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.Repository.GetAllStudents();
        }
    }
}