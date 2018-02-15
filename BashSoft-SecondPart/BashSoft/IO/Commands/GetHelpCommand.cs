namespace BashSoft.IO.Commands
{
    using Exceptions;
    using Judge;
    using Repository;

    public class GetHelpCommand : Command
    {
        public GetHelpCommand(string input, string[] data, Tester tester, StudentRepository repository, IOManager manager) 
            : base(input, data, tester, repository, manager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.DisplayHelp();
        }

        private void DisplayHelp()
        {
            OutputWriter.DisplayHelpMessage($"{new string('-', 110)}");
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                          open file - open {path}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                     make directory - mkdir {path}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                 traverse directory - ls {depth}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                    comparing files - cmp {path1} {path2}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "          change directory relative - cdrel {relative path}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "          change directory absolute - cdabs {absolute path}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "            read students data base - readdb {path}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "show all students in current course - show {courseName}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "     show all courses order by name - showac"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "   show all students order by score - showas"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "          filter students by course - filter {courseName} {excelent/average/poor} {take} {2/5/all}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "           order students in course - order {courseName} {ascending/descending} {take} {20/10/all}"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                      drop database - dropdb"));
            OutputWriter.DisplayHelpMessage(string.Format("|{0, -108}|", "                           get help - help"));
            OutputWriter.DisplayHelpMessage($"{new string('-', 110)}");
            OutputWriter.WriteEmptyLine();
        }
    }
}