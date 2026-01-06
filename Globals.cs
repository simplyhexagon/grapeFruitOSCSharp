using System;

using Cosmos.System.FileSystem;
using System.IO;
using Cosmos.HAL;
using Cosmos.Core;
using Cosmos.System.FileSystem.FAT;
using Cosmos.System.FileSystem.ISO9660;

#pragma warning disable CA2211
#pragma warning disable IDE0059

namespace grapeFruitRebuild
{
    public class Globals
    {
        public const string build = "0.16.3 Alpha";
        public const string osname = "grapeFruitOS";
        public const bool devBuild = true;

        //Virtual devices
        public static CosmosVFS vFS;
        public static FileSystem fatFileSystem;
        public static FatFileSystemFactory fatFSFactory;
        public static ISO9660FileSystem opticalDiscFS;
        public static DriveInfo drive;
        public static NetworkDevice nic;

        //ENV
        public static string currentuser;
        public static string hostname;
        public static string workingdir;
        public static string oldpwd;

        public static bool swapYZ;
        public static bool safeDelete;

        public static void Printsysteminfo()
        {
            Console.WriteLine(osname + " " + build);
            float usedpercent = 0f;
            if (CPU.CanReadCPUID() != 0)
            {
                Console.WriteLine($"CPU: {CPU.GetCPUBrandString()}");
                usedpercent = GCImplementation.GetUsedRAM() / (CPU.GetAmountOfRAM() * 1048576);
            }
            else
            {
                usedpercent = GCImplementation.GetUsedRAM() / (GCImplementation.GetAvailableRAM() * 1048576);
            }

            string realUsedPercent = "";
            if (usedpercent < 1)
                realUsedPercent = "<1%";
            else
                realUsedPercent = usedpercent.ToString();

            Console.WriteLine($"Memory usage: {GCImplementation.GetUsedRAM()} / {GCImplementation.GetAvailableRAM() * 1048576} bytes ({realUsedPercent})");
        }

        public static void env()
        {
            Console.WriteLine("devbuild: " + Globals.devBuild);
            Console.WriteLine("currentuser: " + Globals.currentuser);
            Console.WriteLine("hostname: " + Globals.hostname);
            Console.WriteLine("workingdir: " + Globals.workingdir);
            Console.WriteLine("oldpwd: " + Globals.oldpwd);
            Console.WriteLine("swapYZ: " + Globals.swapYZ);
            Console.WriteLine("safeDelete: " + Globals.safeDelete);
        }

        public static void envEdit()
        {
            Console.WriteLine("Choose variable:\n");
            Console.WriteLine("1. currentuser\n2. hostname\n3. workingdir\n4. oldpwd\n5. swapYZ\n6. safeDelete");
            Console.WriteLine("\nUse the number row to choose");
            Console.Write("> ");
            string input = "";
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine();
                    Console.WriteLine("Key: currentuser\nType: String\n\nCurrent Value: " + currentuser);
                    Console.Write("New value: ");
                    input = Console.ReadLine();
                    if(input != "")
                        currentuser = input;

                    Console.WriteLine("currentuser was updated to " + currentuser);
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine();
                    Console.WriteLine("Key: hostname\nType: String\n\nCurrent Value: " + hostname);
                    Console.Write("New value: ");
                    input = Console.ReadLine();
                    if (input != "")
                        hostname = input;

                    Console.WriteLine("hostname was updated to " + hostname);
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine();
                    Console.WriteLine("Key: workingdir\nType: String\n\nCurrent Value: " + workingdir);
                    Console.Write("New value: ");
                    input = Console.ReadLine();
                    if (input != "")
                        workingdir = input;

                    Console.WriteLine("workingdir was updated to " + workingdir);
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine();
                    Console.WriteLine("Key: oldpwd\nType: String\n\nCurrent Value: " + oldpwd);
                    Console.Write("New value: ");
                    input = Console.ReadLine();
                    if (input != "")
                        oldpwd = input;

                    Console.WriteLine("oldpwd was updated to " + oldpwd);
                    break;
                case ConsoleKey.D5:
                    Console.WriteLine("\nswapYZ was " + swapYZ);
                    swapYZ = !swapYZ;
                    Console.WriteLine("swapYZ is " + swapYZ);
                    break;
                case ConsoleKey.D6:
                    Console.WriteLine("\nsafeDelete was " + safeDelete);
                    safeDelete = !safeDelete;
                    Console.WriteLine("safeDelete is " + safeDelete);
                    break;
                default:
                    break;
            }
        }
    }
}
