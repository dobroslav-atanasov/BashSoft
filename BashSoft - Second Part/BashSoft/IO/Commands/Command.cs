namespace BashSoft.IO.Commands
{
    using System;
    using Exceptions;
    using Judge;
    using Repository;

    public abstract class Command
    {
        private string input;
        private string[] data;
        private Tester tester;
        private StudentRepository repository;
        private IOManager manager;

        protected Command(string input, string[] data, Tester tester, StudentRepository repository, IOManager manager)
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

        protected Tester Tester
        {
            get { return this.tester; }
        }

        protected StudentRepository Repository
        {
            get { return this.repository; }
        }

        protected IOManager Manager
        {
            get { return this.manager; }
        }

        public abstract void Execute();
    }
}