﻿namespace BashSoft.SimpleJudge
{
    using System;
    using System.IO;

    public static class Tester
    {
        public static void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");
            string mismatchPath = GetMismatchPath(expectedOutputPath);

            string[] actualOutputLines = File.ReadAllLines(userOutputPath);
            string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

            bool hasMismatch;
            string[] mismatches =
                GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

            PrintOutput(mismatches, hasMismatch, mismatchPath);
            OutputWriter.WriteMessageOnNewLine("Files read!");
        }

        private static void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (hasMismatch)
            {
                foreach (string line in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(line);
                }

                File.WriteAllLines(mismatchPath, mismatches);
                return;
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
            }
        }

        private static string[] GetLinesWithPossibleMismatches(string[] actualOutputLines, string[] expectedOutputLines,
            out bool hasMismatch)
        {
            hasMismatch = false;
            string output = string.Empty;

            string[] mismatches = new string[actualOutputLines.Length];
            OutputWriter.WriteMessageOnNewLine("Comparing files...");

            for (int i = 0; i < actualOutputLines.Length; i++)
            {
                string actualLine = actualOutputLines[i];
                string expectedLine = expectedOutputLines[i];

                if (actualLine != expectedLine)
                {
                    output = $"Mismatch at line {i} -- expected: \"{expectedLine}\", actual: \"{actualLine}\"";
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }
                mismatches[i] = output;
            }

            return mismatches;
        }

        private static string GetMismatchPath(string expectedOutputPath)
        {
            int indexOfSlash = expectedOutputPath.LastIndexOf('\\');
            string directoryPath = expectedOutputPath.Substring(0, indexOfSlash);
            string finalPath = directoryPath + @"\Mismatches.txt";
            return finalPath;
        }
    }
}