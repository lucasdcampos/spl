using SPL_Interpreter;
using System;
using System.IO;

class Program
{
    public static double Version = 0.1;

    static void Main(string[] args)
    {
        HandleArguments(args);
    }

    static void HandleArguments(string[] args)
    {
        if (args.Length > 0)
        {
            switch (args[0])
            {
                case "run":
                    if (args.Length > 1)
                    {
                        try
                        {
                            string filename = File.ReadAllText(args[1]);
                            Interpreter.Interpret(filename);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"error: unable to read file '{args[1]}'");
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("error: 'run' requires a filename argument");
                    }
                    break;
                case "version":
                    Console.WriteLine(Version.ToString());
                    break;
                case "help":
                    Console.WriteLine("List of available commands:");
                    Console.WriteLine("run {filename} : Executes the code in the specified file.");
                    Console.WriteLine("version : Displays the current version of the interpreter.");
                    Console.WriteLine("help : Displays this list of available commands.");
                    Console.WriteLine("exit : Exits the interpreter.");
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"error: unknown command '{args[0]}'\nType 'help' for the list of commands!");
                    break;
            }
        }
        else
        {
            Console.WriteLine($"SPL Interpreter {Version}\n Type 'help' for the list of commands");
            string[] _args = Console.ReadLine().Split(" ");

            HandleArguments(_args);
        }
    }
}
