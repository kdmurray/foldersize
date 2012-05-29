using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace FolderSize
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayHeader();

            if (args.Length < 1)
            {
                DisplayUsage();
                return;
            }

            string path = args[0];

            DirectoryInfo di = new DirectoryInfo(path);

            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                Console.WriteLine(dir.Name.PadRight(30, ' ') + GetFolderSize(dir).ToString().PadLeft(12,' '));
            }

            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                Console.WriteLine(file.Name.PadRight(30, ' ') + file.Length.ToString().PadLeft(12, ' '));
            }

        }

        private static void DisplayHeader()
        {
            Console.WriteLine("FolderSize " + GetVersionNumber());
            Console.WriteLine("");
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("Error: Must specify a path.");
            Console.WriteLine("USAGE: FolderSize path");
            Console.WriteLine("\n\n");
        }

        private static string GetVersionNumber()
        {
            StringBuilder ver = new StringBuilder();
            ver.Append("v");
            ver.Append(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString());
            ver.Append(".");
            ver.Append(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString());
            ver.Append(".");
            ver.Append(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString());

            return ver.ToString();
        }

        static long GetFolderSize(DirectoryInfo inf)
        {
            long folderSize = 0;
            DirectoryInfo[] dirs = inf.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                folderSize += GetFolderSize(dir);
            }

            FileInfo[] files = inf.GetFiles();
            foreach (FileInfo file in files)
            {
                folderSize += file.Length;
            }

            return folderSize;
        }
    }
}