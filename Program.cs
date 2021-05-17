using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JustCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get correct path separator based on the platform's OS e.g. Windows or Linux
            string separator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";

            // Get the source and target paths from the TEST directory for the brief intro
            string currentPath = Directory.GetCurrentDirectory();
            string sampleSource = $@"{currentPath}{separator}TESTING{separator}TEST";
            string sampleTarget = $@"{currentPath}{separator}TESTING{separator}Target{separator}TEST";

            // Brief intro to quickly get started
            Console.WriteLine("\nWelcome to JustCopy!");
            Console.WriteLine($"\nTo quickly test this app simply copy:\n\nFor source path:\n\n{sampleSource}\n\nFor target path:\n\n{sampleTarget}");
            Console.WriteLine("\n\n...Or add custom paths!");

            // Prompt user to add source and target paths
            Console.WriteLine("\nPlease enter the full path of the folder you want to copy:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("\nPlease enter the full path of the target directory:");
            string targetPath = Console.ReadLine();

            // Using diagnostics to record the elapsed time of copying
            Stopwatch execTime = new Stopwatch();
            execTime.Start();

            // Create an intance of the Copy class to execute the copying functionality
            Copy cp = new Copy(sourcePath, targetPath, "recursive");

            execTime.Stop();
            Console.WriteLine($"\nCopy operation finished in: {execTime.ElapsedMilliseconds} ms");
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}
