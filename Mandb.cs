using System;

namespace grapeFruitRebuild
{
    public class Mandb
    {
        public static void Man(string command)
        {
            string usage = "";
            string description = "";
            bool valid = true;

            //"""""mandb"""""
            switch (command)
            {
                case "echo":
                    usage = "echo <input>";
                    description = "\"Echoes\" back text after the command";
                    break;

                case "clear":
                    usage = "clear";
                    description = "Clears screen";
                    break;

                case "time":
                    usage = "time";
                    description = "Displays current time based on RTC";
                    break;

                case "help":
                case "commands":
                    usage = "{help | commands} [<group>]";
                    description = "Displays information text for help categories OR\ndisplays commands of category";
                    break;

                case "throwex":
                    usage = "throwex";
                    description = "Throws an exception to test current exception handling";
                    break;

                case "env":
                    usage = "env";
                    description = "Lists the environment variables and their values";
                    break;

                case "envedit":
                    usage = "envedit";
                    description = "Starts the environment variable editor";  
                    break;

                case "shutdown":
                    usage = "shutdown";
                    description = "Shuts down the system (asks for confirmation)";
                    break;

                case "reboot":
                    usage = "reboot";
                    description = "Reboots the system (asks for confirmation)";
                    break;

                case "ls":
                case "dir":
                    usage = "{ls | dir} [<path>]";
                    description = "Lists contents of current working directory OR\nthe contents of the specified path";
                    break;

                case "la":
                    usage = "la [<path>]";
                    description = "Outputs verbose list of contents of current working directory OR the contents of the specified path";
                    break;

                case "cd":
                    usage = "cd [<path>]";
                    description = "Changes directory to specified path OR\nif no parameter is set, changes directory to FS root";
                    break;

                case "pwd":
                    usage = "pwd";
                    description = "Prints current working directory path";
                    break;

                case "touch":
                    usage = "touch <path>";
                    description = "Creates a null terminated empty file at specified path";
                    break;

                case "cat":
                    usage = "cat <filename>";
                    description = "Outputs contents of file";
                    break;

                case "mkdir":
                case "md":
                    usage = "{mkdir | md} <directory name>";
                    description = "Creates a directory with the specified name OR\nif name has \\, it will be created at the specified path";
                    break;

                case "copy":
                case "cp":
                    usage = "{copy | cp} <source> <target>";
                    description = "Copies source file to target path";
                    break;

                case "move":
                case "mv":
                    usage = "{move | mv} <source> <target>";
                    description = "Moves source file to target path";
                    break;

                case "ping":
                    usage = "ping <IPv4 address>";
                    description = "Sends 4 ICMP echo requests to the specified address\nExample ping 1.1.1.1";
                    break;

                case "dnsping":
                    usage = "dnsping <domain name>";
                    description = "Sends 4 ICMP echo requests to the\nresolved address of the specified domain name\nExample: dnsping archlinux.org";
                    break;

                case "resolvedns":
                    usage = "resolvedns <domain name>";
                    description = "Resolves the IPv4 address of the specified domain name and prints it to the console";
                    break;

                case "ip":
                    usage = "ip";
                    description = "Prints current IP address to console";
                    break;

                case "nwif":
                    usage = "nwif";
                    description = "Prints information about network interface to console";
                    break;

                case "whatis":
                    usage = "whatis <command>";
                    description = "Outputs information about specified command";
                    break;

                case "nano":
                    usage = "nano [<filepath>]";
                    description = "Open GrapeFruitNano text editor with or without a file open";
                    break;

                case "rm":
                    usage = "rm <path> [-r | -d]";
                    description = "Remove file OR directory at specified path\n-r: delete directory recursively\n-d: delete directory if it's empty";
                    break;

                case "debugmenu":
                    usage = "debugmenu";
                    description = "Start the system's debug menu";
                    break;

                default:
                    valid = false;
                    break;
            }

            if (valid)
                Console.WriteLine("Usage: " + usage + "\n" + description);
            else
                Console.WriteLine("Nothing appropriate");
        }
    }
}
