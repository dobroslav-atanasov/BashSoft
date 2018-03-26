namespace BashSoft.IO
{
    using System.Collections.Generic;
    using System.IO;
    using Contracts;
    using Exceptions;
    using StaticData;

    public class IOManager : IDirectoryManager
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
                catch (AccessException)
                {
                    throw new AccessException();
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
            catch (InvalidFileNameException)
            {
                throw new InvalidFileNameException();
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
                catch (InvalidPathException)
                {
                    throw new InvalidPathException();
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
                throw new InvalidPathException();
            }

            SessionData.currentPath = absolutePath;
        }
    }
}