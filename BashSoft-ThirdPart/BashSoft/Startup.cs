namespace BashSoft
{
    using IO;
    using Repository;
    using Judge;

    public class Startup
    {
        public static void Main()
        {
            Tester tester = new Tester();
            IOManager manager = new IOManager();
            StudentRepository repository = new StudentRepository(new RepositoryFilter(), new RepositorySorter());
            CommandInterpreter interpreter = new CommandInterpreter(tester, repository, manager);
            InputReader reader = new InputReader(interpreter);

            reader.StartReadingCommands();
        }
    }
}