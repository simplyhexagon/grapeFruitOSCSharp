using System;
using System.Collections.Generic;

namespace grapeFruitRebuild
{
    public class Command
    {
        static string[] splitpwd = { };
        public static void Main()
        {
            //Making the prompt pretty
            splitpwd = Globals.workingdir.Split("\\");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{Globals.currentuser}@{Globals.hostname} ");

            //Making the prompt pretty
            if (splitpwd.Length == 2)
                Console.Write($"{Globals.workingdir}]> ");
            else if (splitpwd.Length > 2)
                Console.Write($"{splitpwd[splitpwd.Length - 2]}]> ");

            Console.ForegroundColor = ConsoleColor.White;
            string command = Console.ReadLine();
            ParseInput(command);
        }

        private static void ParseInput(string input)
        {
            List<string> arguments = new List<string>();
            bool insideQuotes = false;
            int start = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    insideQuotes = !insideQuotes;
                }

                if (input[i] == ' ' && !insideQuotes)
                {
                    // If not inside quotes, add the current part to the list
                    arguments.Add(input.Substring(start, i - start));
                    start = i + 1; // Move the start index to the next character
                }
            }

            // Add the last part (or the only part if there are no spaces)
            arguments.Add(input.Substring(start));

            //Force trimming quotes from every damn entry
            //Not elegant and probably not effective
            for (int i = 0; i < arguments.Count; i++)
            {
                arguments[i] = arguments[i].Trim('"');
            }

            Process(arguments);
        }

        private static void Process(List<string> splitinput)
        {
            switch (splitinput[0])
            {
                case "echo":
                    Echo(splitinput);
                    break;

                case "clear":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    break;

                case "time":
                    Time();
                    break;

                case "help":
                case "commands":
                    if (splitinput.Count > 1)
                        Commands(splitinput[1]);
                    else
                        Commands();
                    break;

                case "env":
                    Globals.env();
                    break;

                case "envedit":
                    Globals.envEdit();
                    break;

                case "throwex":
                    throw new Exception("Manually triggered exception");

                case "shutdown":
                    Shutdown();
                    break;

                case "reboot":
                    Reboot();
                    break;

                case "ls":
                case "dir":
                    if (splitinput.Count > 1)
                        FS.List(splitinput[1]);
                    else
                        FS.List("");
                    break;

                case "la":
                    if (splitinput.Count > 1)
                        FS.VerboseList(splitinput[1]);
                    else
                        FS.VerboseList("");
                    break;

                case "cd":
                    if (splitinput.Count > 1)
                        FS.Chdir(splitinput[1]);
                    else
                        //Switching back to FS Root
                        FS.Chdir(@"0:\");
                    break;

                case "pwd":
                    Console.WriteLine(Globals.workingdir);
                    break;

                case "touch":
                    if (splitinput.Count > 1)
                        FS.Touch(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "cat":
                    if (splitinput.Count > 1)
                        FS.Cat(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "mkdir":
                case "md":
                    if (splitinput.Count > 1)
                        FS.Mkdir(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "copy":
                case "cp":
                    if (splitinput.Count > 2)
                        FS.Copy(splitinput[1], splitinput[2]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "move":
                case "mv":
                    if (splitinput.Count > 2)
                        FS.Move(splitinput[1], splitinput[2]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "whatis":
                    if (splitinput.Count > 1)
                        Mandb.Man(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "nano":
                    if (splitinput.Count > 1)
                        nano.Nanomain.Main(splitinput[1]);
                    else
                        nano.Nanomain.Main();
                    break;

                case "rm":
                    if (splitinput.Count > 1)
                    {
                        if (Globals.safeDelete)
                        {
                            if (Choice())
                            {
                                Console.Write('\n');
                                FS.Remove(splitinput);
                            }
                            else
                                return;

                        }
                        else
                            FS.Remove(splitinput);
                    }
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "debugmenu":
                    debugmenu.main();
                    break;

                case "ping":
                    if (splitinput.Count > 1)
                        nw.Ping(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "dnsping":
                    if (splitinput.Count > 1)
                        nw.Dnsping(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "resolvedns":
                    if (splitinput.Count > 1)
                        nw.Resolvedns(splitinput[1]);
                    else
                        Console.WriteLine("Not enough parameters");
                    break;

                case "ip":
                    nw.ip();
                    break;

                case "nwif":
                    nw.nwif();
                    break;

                default:
                    Console.WriteLine("> unknown command");
                    break;
            }
        }

        static void Commands(string category = "")
        {
            switch (category)
            {
                case "":
                default:
                    Console.WriteLine("Commands have been organised into categories");
                    Console.WriteLine("To access categories, type in the following:");
                    Console.WriteLine("\t- help <category>");
                    Console.WriteLine("\t- commands <category>");

                    Console.WriteLine("\nAvailable categories:");
                    Console.WriteLine("\t- system");
                    Console.WriteLine("\t- network OR nw");
                    Console.WriteLine("\t- debug");
                    Console.WriteLine("\t- fs");
                    break;

                case "system":
                    Console.WriteLine("Available commands in \"system\" category:\n");
                    Console.WriteLine("help/commands <category> - shows command list in category");
                    Console.WriteLine("echo <message> - prints to screen");
                    Console.WriteLine("clear - clears screen");
                    Console.WriteLine("time - shows current time (RTC)");
                    Console.WriteLine("shutdown - turns off computer (asks for confirmation)");
                    Console.WriteLine("reboot - reboots computer (asks for confirmation)");
                    Console.WriteLine("whatis <command> - information about command");
                    break;

                case "network":
                case "nw":
                    Console.WriteLine("Available commands in \"network\" category:\n");
                    Console.WriteLine("ping <address> - pings IPv4 address");
                    Console.WriteLine("dnsping <domain name> - pings domain");
                    Console.WriteLine("resolvedns <domain name> - resolve domain to IPv4 manually");
                    Console.WriteLine("ip - prints current IP address to console");
                    Console.WriteLine("nwif - prints information of current active network interface");
                    break;

                case "fs":
                    Console.WriteLine("Available commands in \"fs\" category:\n");
                    Console.WriteLine("ls/dir - list directory contents");
                    Console.WriteLine("la - verbose listing of directory contents");
                    Console.WriteLine("rm - remove file or directory (-d) recursively (-r)");
                    Console.WriteLine("touch <filename> - create empty file with specified name");
                    Console.WriteLine("cat <filename> - read file contents to screen");
                    Console.WriteLine("mkdir/md <name> - creates directory with name");
                    Console.WriteLine("copy/cp <source> <target> - copies file from source to target (if source exists)");
                    Console.WriteLine("move/mv <source> <target> - moves file from source to target (if source exists)");
                    Console.WriteLine("nano <filename> - text editor");
                    break;

                case "debug":
                    Console.WriteLine("throwex - throws test exception");
                    Console.WriteLine("env - Lists environment variables");
                    Console.WriteLine("envedit  - Environment variable editor");
                    Console.WriteLine("debugmenu - Launch debug menu");
                    break;
            }
        }

        static void Echo(List<string> input)
        {
            string output = "";
            foreach (string line in input)
            {
                output += line + ' ';
            }
            Console.WriteLine(output);
        }

        static void Time()
        {
            string timeout_ = Cosmos.HAL.RTC.Year.ToString() + '/' + Cosmos.HAL.RTC.Month.ToString() + '/' + Cosmos.HAL.RTC.DayOfTheMonth.ToString() + ' ' + Cosmos.HAL.RTC.Hour.ToString() + ':' + Cosmos.HAL.RTC.Minute.ToString() + ':' + Cosmos.HAL.RTC.Second.ToString();

            Console.WriteLine(timeout_);
        }

        static void Shutdown()
        {
            if (Choice())
                Cosmos.System.Power.Shutdown();
        }

        static void Reboot()
        {
            if (Choice())
                Cosmos.System.Power.Reboot();
        }

        static bool Choice(bool primaryYes=true)
        {
            redochoice:

            if (primaryYes)
                Console.Write("Are you sure? (Y/n)");
            else
                Console.Write("Are you sure? (y/N)");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    Console.Write('\n');
                    if (primaryYes)
                        return true;
                    else 
                        return false;
                case ConsoleKey.Y:
                    if (!Globals.swapYZ)
                        return true;
                    else
                        goto redochoice;
                case ConsoleKey.N:
                    Console.Write('\n');
                    return false;

                case ConsoleKey.Z:
                    if (Globals.swapYZ)
                        return true;
                    else
                        goto redochoice;

                default:
                    Console.Write('\n');
                    goto redochoice;
            }
        }
    }
}
