namespace BashSoft
{
    using System;
    using System.Diagnostics;
    using SimpleJudge;

    public static class CommandInterpreter
    {
        public static void InterpredCommand(string input)
        {
            string[] data = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
                    // Filter functionality
                    break;
                case "order":
                    // Order functionality
                    break;
                case "decOrder":
                    // DecOrder functionality
                    break;
                case "download":
                    // Download functionality
                    break;
                case "downloadAsynch":
                    // DownloadAsynch functionality
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private static void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                StudentsRepository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string studentName = data[2];
                StudentsRepository.GetStudentsScoresFromCourse(courseName, studentName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryGetHelp(string input, string[] data)
        {
            if (data.Length == 1)
            {
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 130)}");
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "make directory - mkdir: path "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "traverse directory - ls: depth "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "comparing files - cmp: path1 path2"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "change directory - changeDirREl:relative path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "change directory - changeDir:absolute path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "read students data base - readDb: path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "download file - download: path of file (saved in current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -128}|", "get help – help"));
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 130)}");
                OutputWriter.WriteEmptyLine();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                StudentsRepository.InitializeData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                IOManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relativePath = data[1];
                IOManager.ChangeCurrentDirectoryRelative(relativePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TyrCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstPath = data[1];
                string secondPath = data[2];

                Tester.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                IOManager.TraverseFolder(0);
            }
            else if (data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(data[1], out depth);
                if (hasParsed)
                {
                    IOManager.TraverseFolder(depth);
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

        private static void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string directoryName = data[1];
                IOManager.CreateDirectoryInCurrentFolder(directoryName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private static void TryOpenFile(string input, string[] data)
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

        private static void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayMessage($"The command '{input}' is invalid!");
        }
    }
}