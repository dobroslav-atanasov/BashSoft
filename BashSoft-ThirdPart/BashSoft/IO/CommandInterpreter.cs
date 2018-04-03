namespace BashSoft.IO
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Commands;
    using Contracts;
    using Exceptions;
    using Repository;
    using Judge;

    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer tester;
        private IDatabase repository;
        private IDirectoryManager manager;

        public CommandInterpreter(IContentComparer tester, IDatabase repository, IDirectoryManager manager)
        {
            this.tester = tester;
            this.repository = repository;
            this.manager = manager;
        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayMessage(ex.Message);
            }
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            object[] parametersForConstruction = new object[] { input, data };

            Type typeOfCommand = Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(t => t.GetCustomAttributes(typeof(AliasAttribute)).Where(a => a.Equals(command)).ToArray().Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            Command instance = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstruction);

            FieldInfo[] fieldsOfCommand = typeOfCommand.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo[] fieldsOfInterpreter = typeOfInterpreter.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo commandField in fieldsOfCommand)
            {
                Attribute attribute = commandField.GetCustomAttribute(typeof(InjectAttribute));
                if (attribute != null)
                {
                    if (fieldsOfInterpreter.Any(f => f.FieldType == commandField.FieldType))
                    {
                        commandField.SetValue(instance, fieldsOfInterpreter.First(f => f.FieldType == commandField.FieldType).GetValue(this));
                    }
                }
            }

            return instance;
        }
    }
}