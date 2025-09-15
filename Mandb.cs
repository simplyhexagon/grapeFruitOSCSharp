using System;

namespace grapeFruitRebuild
{
    public class Mandb
    {
        public static void Man(string command)
        {
            //"""""mandb"""""
            switch (command)
            {
                case "echo":
                    Console.WriteLine("Usage: echo <text>");
                    Console.WriteLine("\"Echoes\" back text after the command");
                    break;

                case "clear":
                    Console.WriteLine("Usage: clear");
                    Console.WriteLine("Clears screen");
                    break;

                case "time":
                    Console.WriteLine("Usage: time");
                    Console.WriteLine("Displays current time based on RTC");
                    break;

                case "help":
                case "commands":
                    Console.WriteLine("Usage: help <or> commands");
                    Console.WriteLine("Displays the list of currently available commands");
                    break;

                case "throwex":
                    Console.WriteLine("Usage: throwex");
                    Console.WriteLine("Throws an exception to test current exception handling");
                    break;

                case "shutdown":
                    Console.WriteLine("Usage: shutdown");
                    Console.WriteLine("Shuts down the system (asks for confirmation)");
                    break;

                case "reboot":
                    Console.WriteLine("Usage: reboot");
                    Console.WriteLine("Reboots the system (asks for confirmation)");
                    break;

                case "ls":
                case "dir":
                    Console.WriteLine("Usage: ls/dir <or> ls/dir <path>");
                    Console.WriteLine("Lists contents of current working directory or the contents of the specified path");
                    break;

                case "la":
                    Console.WriteLine("Usage: la <or> la <path>");
                    Console.WriteLine("Outputs a verbose listing of the current working directory or of the specified path");
                    break;

                case "cd":
                    Console.WriteLine("Usage: cd <path>");
                    Console.WriteLine("Changes directory to specified path");
                    break;

                case "pwd":
                    Console.WriteLine("Usage: pwd");
                    Console.WriteLine("Prints current working directory");
                    break;

                case "touch":
                    Console.WriteLine("Usage: touch <filename>");
                    Console.WriteLine("Creates empty file with the specified name");
                    break;

                case "cat":
                    Console.WriteLine("Usage: cat <filename>");
                    Console.WriteLine("List contents of file");
                    break;

                case "mkdir":
                case "md":
                    Console.WriteLine("Usage: mkdir/md <dirname>");
                    Console.WriteLine("Creates a directory with the specified name");
                    Console.WriteLine("If there are \\-s included, it will create a new directory at the path");
                    break;

                case "copy":
                case "cp":
                    Console.WriteLine("Usage: copy/cp <source> <target>");
                    Console.WriteLine("Copies a file to the specified target");
                    break;

                case "move":
                case "mv":
                    Console.WriteLine("Usage: move/mv <source> <target>");
                    Console.WriteLine("Moves a file to the specified target");
                    break;

                //case "gfdisk":
                //    Console.WriteLine("Usage: gfdisk");
                //    Console.WriteLine("Launches GrapeFruit Disk Utility");
                //    break;

                //case "ping":
                //    Console.WriteLine("Usage: ping <IPv4 address>");
                //    Console.WriteLine("Example: ping 1.1.1.1");
                //    Console.WriteLine("Sends 4 ICMP echo requests to the specified address");
                //    break;

                //case "dnsping":
                //    Console.WriteLine("Usage: dnsping <Domain name>");
                //    Console.WriteLine("Example: dnsping archlinux.org");
                //    Console.WriteLine("Sends 4 ICMP echo requests to the specified address");
                //    break;

                //case "trydhcp":
                //    Console.WriteLine("Usage: trydhcp");
                //    Console.WriteLine("Attempts to reset DHCP configuration, or set it, if previously unsuccesful");
                //    break;

                case "whatis":
                    Console.WriteLine("Usage: whatis <command>");
                    Console.WriteLine("Information about command specified");
                    break;

                //case "kblayout":
                //    Console.WriteLine("Usage: kblayout");
                //    Console.WriteLine("Prompts user to set keyboard layout from displayed list");
                //    break;

                //case "resolvedns":
                //    Console.WriteLine("Usage: resolvedns <domain name>");
                //    Console.WriteLine("Resolve IPv4 address of domain manually (could be useful)");
                //    break;

                case "nano":
                    Console.WriteLine("Usage: nano // nano <file path>");
                    Console.WriteLine("GrapeFruitNano file editor (similar to nano on *nix systems)");
                    break;

                case "rm":
                    Console.WriteLine("Usage: rm <path>");
                    Console.WriteLine("Removes file at path");
                    Console.WriteLine("Can take arguments -r and -d");
                    Console.WriteLine("- rm <path>: removes regular file at path");
                    Console.WriteLine("- rm -d <path>: removes empty directory at path");
                    Console.WriteLine("- rm -r <path>: removes directory recursively at path");
                    break;

                //case "desktop":
                //    Console.WriteLine("Usage: desktop");
                //    Console.WriteLine("Launches Desktop Mode (IN DEVELOPMENT!)");
                //    break;

                //case "keytest":
                //    Console.WriteLine("Usage: keytest");
                //    Console.WriteLine("Launches keycode testing program");
                //    break;

                default:
                    Console.WriteLine("Nothing appropriate");
                    break;
            }
        }
    }
}
