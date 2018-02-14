﻿namespace BashSoft.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using StaticData;

    public class IOManager
    {
        public void TraverseFolder(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentity = SessionData.currentPath.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count > 0)
            {
                string currentPath = subFolders.Dequeue();
                int identention = currentPath.Split('\\').Length - initialIdentity;
                OutputWriter.WriteMessageOnNewLine($"{new string('-', identention)}{currentPath}");

                if (depth - identention < 0)
                {
                    break;
                }

                try
                {
                    foreach (string file in Directory.GetFiles(currentPath))
                    {
                        int indexOfSlash = file.LastIndexOf("\\");
                        string fileName = file.Substring(indexOfSlash);
                        OutputWriter.WriteMessageOnNewLine($"{new string('-', indexOfSlash)}{fileName}");
                    }

                    foreach (string directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayMessage(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = SessionData.currentPath + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("indexOfLastSlash", ExceptionMessages.InvalidDestination);
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += $"\\{relativePath}";
                this.ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new DirectoryNotFoundException(ExceptionMessages.InvalidPath);
            }

            SessionData.currentPath = absolutePath;
        }
    }
}