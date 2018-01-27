namespace BashSoft
{
    using System.Collections.Generic;
    using System.IO;

    public static class IOManager
    {
        public static void TraverseFolder(string path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentity = path.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(path);

            while (subFolders.Count > 0)
            {
                string currentPath = subFolders.Dequeue();
                int identention = currentPath.Split('\\').Length - initialIdentity;
                OutputWriter.WriteMessageOnNewLine($"{new string('-', identention)}{currentPath}");

                foreach (string directoryPath in Directory.GetDirectories(currentPath))
                {
                    subFolders.Enqueue(directoryPath);
                }
            }
        }
    }
}