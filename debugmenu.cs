using System;

using Cosmos.System.FileSystem;
using System.IO;
using Cosmos.HAL;
using Cosmos.Core;
using Cosmos.System.FileSystem.FAT;
using Cosmos.System.FileSystem.ISO9660;
using System.Collections.Generic;

#pragma warning disable CA2211
#pragma warning disable IDE0059

namespace grapeFruitRebuild
{
    public class debugmenu
    {
        static string input = "";
        public static void main()
        {
            Console.WriteLine("- - - Debug Menu - - -\n");
            Console.WriteLine("Available commands: env, sysinfo, vdevs, log, exit");
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "env":
                        env();
                        break;
                    case "sysinfo":
                        Globals.Printsysteminfo();
                        break;
                    case "vdevs":
                        vdevs();
                        break;
                    case "log":
                        log();
                        break;
                    case "help":
                    case "?":
                        Console.WriteLine("Available commands: env, sysinfo, vdevs, log, exit");
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            } while (input != "exit");
        }

        private static void env()
        {
            Console.WriteLine("devbuild: " + Globals.devBuild);
            Console.WriteLine("currentuser: " + Globals.currentuser);
            Console.WriteLine("hostname: " + Globals.hostname);
            Console.WriteLine("workingdir: " + Globals.workingdir);
            Console.WriteLine("oldpwd: " + Globals.oldpwd);
            Console.WriteLine("swapYZ: " + Globals.swapYZ);
            Console.WriteLine("safeDelete: " + Globals.safeDelete);
        }
        private static void vdevs()
        {
            Console.WriteLine("\nVirtual devices\n");

            Console.WriteLine("CosmosVFS disks:");
            List<Disk> vfsDisks = Globals.vFS.GetDisks();
            foreach (Disk disk in vfsDisks) {
                disk.DisplayInformation();
                Console.Write("\nContinue . . .");
                Console.ReadKey();
                Console.Write('\n');
            }

            Console.WriteLine("NIC info");
            Console.WriteLine("Name: " + Globals.nic.Name);
            Console.WriteLine("Name ID: " + Globals.nic.NameID);
            Console.WriteLine("MAC: " + Globals.nic.MACAddress);
            Console.WriteLine("Card Type: " + Globals.nic.CardType);

            Console.Write("\nContinue . . .");
            Console.ReadKey();
            Console.Write('\n');
        }
        private static void log()
        {
            byte type = 0;
            Console.Write("\nType (int): ");
            type = Convert.ToByte(Console.ReadLine());
            string msg = "";
            Console.Write("msg? (str): ");
            msg = Console.ReadLine();
            Logger.Log(type, msg);
        }
    }
}
