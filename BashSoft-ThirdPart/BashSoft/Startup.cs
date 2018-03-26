namespace BashSoft
{
    using Contracts;
    using IO;
    using Repository;
    using Judge;

    public class Startup
    {
        public static void Main()
        {
            IContentComparer tester = new Tester();
            IDirectoryManager manager = new IOManager();
            IDataFilter filter = new RepositoryFilter();
            IDataSorter sorter = new RepositorySorter();
            IDatabase repository = new StudentRepository(filter, sorter);
            IInterpreter interpreter = new CommandInterpreter(tester, repository, manager);
            IReader reader = new InputReader(interpreter);

            reader.StartReadingCommands();
        }
    }
}