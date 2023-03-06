using LAP_RekursivDateisystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Runtime.Intrinsics.X86;
using File = LAP_RekursivDateisystem.DAL.Models.File;

namespace LAP_RekursivDateisystem.DAL
{
    public class Recursive : IRecursive
    {
        private bool printFile = false;
        private int AmountFiles = 0;
        private int AmountDirectories = 0;
        private string spaces = " ";
        private List<Print> dirSize = new List<Print>();
        private void AddFileToDB(string name, DateTime creationDate, int size, string directory)
        {
            using (var context = new DatabaseContext())
            {
                var dto = new File()
                {
                    Name = name,
                    CreationDate = creationDate,
                    Size = size,
                    DirectoryID = directory
                };
                context.Add(dto);
                context.SaveChanges();
            }
        }
        private void AddDirToDB(string name, DateTime creationDate, int size)
        {
            using (var context = new DatabaseContext())
            {
                var dto = new Models.Directory()
                {
                    Name = name,
                    CreationDate = creationDate,
                    Size = size
                };
                context.Add(dto);
                context.SaveChanges();
            }
        }
        public void DeleteDatabase()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.ExecuteSqlRaw("Delete From Files;");
                context.Database.ExecuteSqlRaw("Delete From Directories;");
            }
        }


        public void PrintBiggestDir(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            DirectoryInfo biggestDir = directory.EnumerateDirectories()
                .OrderByDescending(dir => dir.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length))
                .FirstOrDefault();

            Console.WriteLine("          -{0} ({1})", biggestDir.FullName, GetSizeString(GetDirSize(biggestDir)));
        }
        private long GetDirSize(DirectoryInfo dir)
        {
            return dir.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
    

        public void PrintTop10(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            var directories = directory.GetDirectories("*", SearchOption.AllDirectories)
                .OrderByDescending(dir => GetDirSizeTop10(dir))
                .Take(10)
                .ToList();

            int x = 1;
            Console.WriteLine();
            foreach (var dir in directories)
            {

                Console.WriteLine($"          {x}. {dir.Name} ({GetSizeString(GetDirSizeTop10(dir))})");
                x++;
            }
        }
        private long GetDirSizeTop10(DirectoryInfo directory)
        {
            return directory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }

        private string GetSizeString(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }
 


        public void GetFilesLazy(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            AddDirToDB(directory.FullName, directory.CreationTime, 0);

            foreach (string filePath in System.IO.Directory.GetFiles(path))
            {

                FileInfo file = new FileInfo(filePath);
                long length = new System.IO.FileInfo(filePath).Length;

                AddFileToDB(file.Name, file.CreationTime, (int)length, path);
                AmountFiles++;


            }
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(path))
            {
                GetFilesLazy(subdirectoryPath);
                AmountDirectories++;
            }

        }

        public void GetFiles(string path)
        {
            if(System.IO.Directory.GetFiles(path).Length != 0)
            {
                foreach (string filePath in System.IO.Directory.GetFiles(path))
                {
                    if (System.IO.Directory.GetFiles(path) != null)
                    {
                        if (printFile == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("           Files:");
                            Console.ForegroundColor = ConsoleColor.White;
                            printFile = true;
                        }
                        FileInfo fileInfo = new FileInfo(filePath);
                        Console.WriteLine("            - " + fileInfo.Name);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("            - No files in directory.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(path))
            {
                printFile = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("         Subdirectory: " + subdirectoryPath);
                Console.ForegroundColor = ConsoleColor.White;
                GetFiles(subdirectoryPath);
            }
        }

        public int GetAmountDir()
        {
            int x = AmountDirectories;
            AmountDirectories = 0;
            return x;
        }

        public int GetAmountFiles()
        {
            return AmountFiles;
        }
    }
}