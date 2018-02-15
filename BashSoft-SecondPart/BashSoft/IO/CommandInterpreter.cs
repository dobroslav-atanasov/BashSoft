namespace BashSoft.IO
{
    using System;
    using Commands;
    using Exceptions;
    using Repository;
    using Judge;

    public class CommandInterpreter
    {
        private Tester tester;
        private StudentRepository repository;
        private IOManager manager;

        public CommandInterpreter(Tester tester, StudentRepository repository, IOManager manager)
        {
            this.tester = tester;
            this.repository = repository;
            this.manager = manager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = data[0].ToLower();

            try
            {
                Command command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayMessage(ex.Message);
            }
        }

        private Command ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    return new OpenFileCommand(input, data, this.tester, this.repository, this.manager);
                case "mkdir":
                    return new MakeDirectoryCommand(input, data, this.tester, this.repository, this.manager);
                case "ls":
                    return new TraverseFoldersCommand(input, data, this.tester, this.repository, this.manager);
                case "cmp":
                    return new CompareFilesCommand(input, data, this.tester, this.repository, this.manager);
                case "cdrel":
                    return new ChangePathRelativelyCommand(input, data, this.tester, this.repository, this.manager);
                case "cdabs":
                    return new ChangePathAbsoluteCommand(input, data, this.tester, this.repository, this.manager);
                case "readdb":
                    return new ReadDatabaseCommand(input, data, this.tester, this.repository, this.manager);
                case "help":
                    return new GetHelpCommand(input, data, this.tester, this.repository, this.manager);
                case "show":
                    return new ShowCourseCommand(input, data, this.tester, this.repository, this.manager);
                case "showac":
                    return new ShowAllCoursesCommand(input, data, this.tester, this.repository, this.manager);
                case "showas":
                    return new ShowAllStudentsCommand(input, data, this.tester, this.repository, this.manager);
                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, this.tester, this.repository, this.manager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, this.tester, this.repository, this.manager);
                case "dropdb":
                    return new DropDatabaseCommand(input, data, this.tester, this.repository, this.manager);
                default:
                    throw new InvalidCommandException(input);
            }
        }
    }
}