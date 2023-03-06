using LAP_RekursivDateisystem.DAL;
using LAP_RekursivDateisystem.DAL.Models;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

public class Program
{
    private static string? path;
    private static Recursive recursive = new Recursive();
    private static bool pathValid = false;
    private static void Main(string[] args)
    {


        while(pathValid == false)
        {
            Console.Clear();

            Console.WriteLine("   ********************************************");
            Console.WriteLine("   *           file system analysis           *");
            Console.WriteLine("   ********************************************");

            Console.Write("   Path to analyse: ");
            path = Console.ReadLine();
            if (System.IO.Directory.Exists(path))
            {
                pathValid = true;
                Console.Write("\n\n                   Loading...");

                recursive.DeleteDatabase();
                recursive.GetFilesLazy(path);

                bool showMenu = true;
                while (showMenu)
                {
                    showMenu = MainMenu();
                }
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n       {path} is not valid!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }



    }

    private static bool MainMenu()
    {
        Console.Clear();
        Console.WriteLine("   ********************************************");
        Console.WriteLine("   *           file system analysis           *");
        Console.WriteLine("   ********************************************");
        Console.WriteLine("   * Choose an option:                        *");
        Console.WriteLine("   * 1 - print path structrue                 *");
        Console.WriteLine("   * 2 - folder of the month                  *");
        Console.WriteLine("   * 3 - 10 biggest directories               *");
        Console.WriteLine("   * 4 - Amount of dirs/files                 *");
        Console.WriteLine("   * 5 - Exit                                 *");
        Console.WriteLine("   ********************************************");
        Console.Write("\r\n     Select an option: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n       Structure of path: " + path + "\n");
                Console.ForegroundColor = ConsoleColor.White;
                recursive.GetFiles(path);
                Console.ReadLine();
                return true;
            case "2":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n       Folder of the month:\n");
                Console.ForegroundColor = ConsoleColor.White;
                recursive.PrintBiggestDir(path);
                Console.ReadLine();
                return true;
            case "3":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n       10 biggest folder:\n");
                Console.ForegroundColor = ConsoleColor.White;
                recursive.PrintTop10(path);
                Console.ReadLine();
                return true;
            case "4":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n       Amount of directories:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("          -" + recursive.GetAmountDir().ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("       Amount of Files:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("          -" + recursive.GetAmountFiles().ToString());
                Console.ReadLine();
                return true;
            case "5":
                return false;
            default:
                return true;

        }
    }
}