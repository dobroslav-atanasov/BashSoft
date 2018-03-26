namespace BashSoft.IO.Commands
{
    using System;
    using Contracts;
    using Exceptions;
    using Judge;
    using Repository;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        private IContentComparer tester;
        private IDatabase repository;
        private IDirectoryManager manager;

        protected Command(string input, string[] data, IContentComparer tester, IDatabase repository, IDirectoryManager manager)
        {
            this.Input = input;
            this.Data = data;
            this.tester = tester;
            this.repository = repository;
            this.manager = manager;
        }

        protected string Input
        {
            get { return this.input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.input = value;
            }
        }

        protected string[] Data
        {
            get { return this.data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                this.data = value;
            }
        }

        protected IContentComparer Tester
        {
            get { return this.tester; }
        }

        protected IDatabase Repository
        {
            get { return this.repository; }
        }

        protected IDirectoryManager Manager
        {
            get { return this.manager; }
        }

        public abstract void Execute();
    }
}