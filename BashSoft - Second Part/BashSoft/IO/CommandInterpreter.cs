namespace BashSoft.IO
{
    using System;
    using System.Diagnostics;
    using Repository;
    using Judge;
    using StaticData;

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
            string[] data = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string command = data[0];
            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TyrCompareFiles(input, data);
                    break;
                case "cdRel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "dropdb":
                    TryDropDatabase(input, data);
                    break;
                //case "download":
                //    Download functionality
                //    break;
                //case "downloadAsynch":
                //    DownloadAsynch functionality
                //    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void TryDropDatabase(string input, string[] data)
        {
            if (data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
                return;
            }

            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }

        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2];
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity,
            string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayMessage(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }

        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2];
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity,
            string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayMessage(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayMessage(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }

        private void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string studentName = data[2];
                this.repository.GetStudentsScoresFromCourse(courseName, studentName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryGetHelp(string input, string[] data)
        {
            if (data.Length == 1)
            {
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 130)}");
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "make directory - mkdir: path "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "traverse directory - ls: depth "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "comparing files - cmp: path1 path2"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "change directory - changeDirREl:relative path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "change directory - changeDir:absolute path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "read students data base - readDb: path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "show all students in current course - show: {courseName}"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                    "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
                //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                //    "download file - download: path of file (saved in current directory)"));
                //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|",
                //    "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "get help – help"));
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 130)}");
                OutputWriter.WriteEmptyLine();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                this.repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                this.manager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relativePath = data[1];
                this.manager.ChangeCurrentDirectoryRelative(relativePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TyrCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstPath = data[1];
                string secondPath = data[2];

                this.tester.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                this.manager.TraverseFolder(0);
            }
            else if (data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(data[1], out depth);
                if (hasParsed)
                {
                    this.manager.TraverseFolder(depth);
                }
                else
                {
                    OutputWriter.DisplayMessage(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string directoryName = data[1];
                this.manager.CreateDirectoryInCurrentFolder(directoryName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayMessage($"The command '{input}' is invalid!");
        }
    }
}