using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace JustCopy
{
    class Copy
    {
        private string _sourcePath;
        private string _targetPath;
        private string _separator = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";

        // Constructor that takes 3 parameters
        public Copy(string sourcePath, string targetPath, string copyType)
        {
            _sourcePath = sourcePath;
            _targetPath = targetPath;

            // The "copyType" is simply a string in this case that doesn't mean anything.
            // However, a "concurrent" option for example, could be a potential feature for faster copying with the use of threads.
            if (copyType == "recursive")
            {
                RecursiveCopy(_sourcePath, _targetPath);
            }
        }

        // A copy method that recursively copies all the contents
        // of a given directory - including any sub-directories.
        public void RecursiveCopy(string sourcePath, string targetPath)
        {
            Console.WriteLine("\nRunning Recursive Copy...");

            try
            {
                DirectoryInfo dir = null;

                // Check if empty input was provided
                if(!string.IsNullOrWhiteSpace(sourcePath))
                {
                    // Create an instance of the DirectoryInfo Class to copy the directory and its sub-directories
                    dir = new DirectoryInfo(sourcePath);

                    // Check if the source target exists
                    if (!dir.Exists)
                    {
                        Console.WriteLine($"The source directory \"{sourcePath}\" doesn't exist.\nPlease, try again by adding a correct path.");
                    }
                    else
                    {
                        // Check if the source directory already exists in the target path
                        if (System.IO.Directory.Exists(targetPath))
                        {
                            Console.WriteLine($"{targetPath} already exists!\n");

                            // If the target directory exists, change the target
                            // name to create another copy without replacing the existing one
                            ChangeTargetName(targetPath);

                            // Take the new target path name from the local property
                            targetPath = _targetPath;

                            Console.WriteLine($"Creating directory: {targetPath}\n");
                            Directory.CreateDirectory(targetPath);

                        }
                        // Check if empty input was provided for the target path
                        else if(string.IsNullOrWhiteSpace(targetPath))
                        {
                            Console.WriteLine($"\nThe provided target directory path \"{targetPath}\" is empty.\nPlease, try again by adding a correct path.\n");
                        }

                        // Gets a list of the directories in the source folder
                        DirectoryInfo[] dirs = dir.GetDirectories();

                        // If the target directory doesn't exist, create it.
                        Directory.CreateDirectory(targetPath);

                        // Get the files in the directory and copy them to the new location.
                        FileInfo[] files = dir.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            string filePath = Path.Combine(targetPath, file.Name);
                            Console.WriteLine($"Copying {file.Name}\n\tfrom {sourcePath}");
                            file.CopyTo(filePath, false);
                        }

                        // If copying subdirectories, copy them and their contents to new location.
                        foreach (DirectoryInfo subDir in dirs)
                        {
                            string subDirPath = Path.Combine(targetPath, subDir.Name);
                            Console.WriteLine($"\nCopying {subDir.Name}\n\tfrom {sourcePath}");
                            // Recursive call for sub-direcories
                            RecursiveCopy(subDir.FullName, subDirPath);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\nThe provided source directory path \"{sourcePath}\" is empty.\nPlease, try again by adding a correct path.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        // Gets the path of a given directory and changes its name
        // to create a copy
        private void ChangeTargetName(string targetPath)
        {
            try
            {
                // Get the target's folder name and its parent
                string targetPathName = new DirectoryInfo(targetPath).Name;
                DirectoryInfo parentDirectory = new DirectoryInfo(targetPath).Parent;

                // Get the paths of the parent directory of the target folder
                DirectoryInfo[] directories = parentDirectory.GetDirectories(".");
                List<string> dirCopies = new List<string>();

                // Regular expression that looks for patterns that include
                // the target folder and any copies of it.
                // For example, "Example Folder - Copy", "Example Folder 1", etc.
                string pattern = @"\b" + targetPathName + @"[A-Za-z0-9\s@\W|_]*$";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

                // Find any matches for the regex and add the names in the list
                foreach (DirectoryInfo directory in directories)
                {
                    if (rgx.IsMatch(directory.Name))
                    {
                        dirCopies.Add(directory.Name);
                    }
                }

                bool newCopyNameFound = false;
                int num = 1;
                string newDirName = "";

                // Iterate the list of folder names until you find
                // a new one that doesn't exist in the target directory
                while (!newCopyNameFound)
                {
                    // Try the pattern: "- Copy" first f there is no second copy of the target directory.
                    // For example, if the target folder "Example Folder" already exists, change the name to "Example Folder - Copy"
                    newDirName = $"{targetPathName} - Copy";

                    foreach (string dirCopy in dirCopies)
                    {
                        // If the initial pattern exists, then add a number and
                        // increase it accordingly. For example, if "Example Folder - Copy"
                        // exists, change the name to "Example Folder - Copy (2)" etc.
                        if (dirCopy == newDirName)
                        {
                            num++;
                            newDirName = $"{targetPathName} - Copy ({num})";
                        }
                        else
                        {
                            newCopyNameFound = true;
                        }
                    }
                }

                // Create the full path of the new target name and assign it to
                // the targetPath property
                targetPath = $@"{parentDirectory.FullName}{_separator}{newDirName}";
                _targetPath = targetPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
