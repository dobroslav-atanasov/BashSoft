﻿namespace BashSoft.IO.Commands
{
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;
    using Judge;
    using Models;
    using Repository;

    public class ShowAllCoursesCommand : Command
    {
        public ShowAllCoursesCommand(string input, string[] data, IContentComparer tester, IDatabase repository, IDirectoryManager manager) 
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.Repository.GetAllCourses();
        }
    }
}