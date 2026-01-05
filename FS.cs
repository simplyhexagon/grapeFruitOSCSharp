using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;

namespace grapeFruitRebuild
{
    public class FS
    {
        public static void LookForFileSystems()
        {

            try
            {
                Logger.Log(1, "Looking for storage devices...");
                BlockDevice[] devices = BlockDevice.Devices.ToArray();
                foreach (BlockDevice device in devices)
                {
                    Console.WriteLine("Blockdevice: " + device.ToString() + '\n');
                }
                //Logger.Log(1, "Looking for ISO9660 drives...");

            }
            catch (Exception e)
            {
                ErrorScreen.SpecifiedError(e);
            }

        }
        //For future use
        /*public static void ListBlockDevs()
        {
            //lsblk
            if (Globals.vFS != null)
            {
                Console.WriteLine("NAME\t\tSIZE\t\tTYPE\tMOUNT");
                List<Disk> vfsDisks = Globals.vFS.GetDisks();
                Disk disk = null;
                string diskType = "";
                for(short i = 0; i < vfsDisks.Count; i++)
                {
                    //Get all the data of said disk
                    disk = vfsDisks[i];

                    //Write disk data first
                    //Name
                    Console.Write("dev" + i);
                    //Size
                    Console.Write("\t\t");
                    Console.Write(disk.Size);
                    //Type
                    Console.Write("\t\t");
                    //Format internal string
                    diskType = disk.GetType().ToString().Substring(6).ToUpper();
                    Console.Write("\t"+ diskType + "\n");
                   
                }
            }
            else
            {
                Logger.Log(2, "Virtual Filesystem not initialised");
            }
        }*/
        public static void List(string path = "")
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "Virtual Filesystem not initialised");
            }
            else
            {
                path = path == "" ? Globals.workingdir : path;

                if (path != "" && !path.Contains(@":\"))
                    path = Globals.workingdir + path;

                if (Directory.Exists(path))
                {
                    string[] filePaths = Directory.GetFiles(path);
                    Console.WriteLine("\nDirectory listing of " + path);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var d in Directory.GetDirectories(path))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        if (dirName.Contains(' '))
                            Console.Write("\"" + dirName + "\"" + " ");
                        else
                            Console.Write(dirName + " ");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    for (int i = 0; i < filePaths.Length; ++i)
                    {
                        string path_ = filePaths[i];
                        Console.Write(Path.GetFileName(path_) + " ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nTotal: " + $"{Globals.drive.TotalSize}" + " b / Free: " + $"{Globals.drive.AvailableFreeSpace}" + " b\n");
                }
                else
                {
                    Console.Write("Directory ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\"" + path + "\"");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" not found!");
                }

            }
        }

        public static void VerboseList(string path = "")
        {
            if(Globals.vFS != null) 
            {
                if(path == "")
                    path = Globals.workingdir;

                if (!path.Contains(@":\"))
                    path = Globals.workingdir + path;


                if (Directory.Exists(path))
                {
                    //string[] filePaths = Directory.GetFiles(path);
                    Console.WriteLine("\nVerbose listing of " + path);
                    Console.WriteLine("\ntype\t\tname\tsize");
                    Console.ForegroundColor = ConsoleColor.Yellow;   
                    foreach (var d in Directory.GetDirectories(path))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Console.Write("dir\t\t");

                        if (dirName.Contains(' '))
                            Console.Write("\"" + dirName + "\"" + " ");
                        else
                            Console.Write(dirName + " ");

                        Console.Write("\n");
                    }
                    

                    Console.ForegroundColor = ConsoleColor.Blue;

                    foreach(var file in Directory.GetFiles(path))
                    {
                        Console.Write("file\t");
                        Console.Write(Path.GetFileName(file) + "\t");
                        byte[] content = File.ReadAllBytes(path + "\\" + file);

                        int fsize = content.Length;

                        //Get file size
                        if (fsize < 1000)
                            Console.Write(fsize + "B");
                        else if ((fsize / 1000) < 1000)
                            Console.Write(fsize / 1000 + "KB");
                        else if (((fsize / 1000) / 1000)  < 1000)
                            Console.Write((fsize / 1000) / 1000 + "MB");
                        Console.Write("\n");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nTotal: " + Globals.drive.TotalSize + " b / Free: " + Globals.drive.AvailableFreeSpace + " b\n");
                }
                else
                {
                    Console.Write("Directory ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\"" + path + "\"");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" not found!");
                }
            }
            else
            {
                Logger.Log(2, "Virtual Filesystem not initialised");
            }
        }

        public static void Touch(string filename)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Touch: Virtual Filesystem is not initialised!");
            }
            else
            {
                if (!filename.Contains(@":\"))
                    filename = Globals.workingdir + filename;

                try
                {
                    if (!File.Exists(filename))
                    {
                        File.Create(filename);

                        //Adding a zero character to the file, so nano doesn't crash when opening it
                        StreamWriter writer = new(filename);
                        writer.Write('\0');
                        writer.Close();
                    }
                    else
                        Console.WriteLine("File already exists");
                }
                catch (FileNotFoundException)
                {
                    File.Create(filename);
                }
            }
        }

        public static void Cat(string path)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Cat: Virtual Filesystem is not initialised!");
            }
            else
            {
                if (!path.Contains(@":\"))
                    path = Globals.workingdir + path;

                try
                {
                    string[] fileContent = File.ReadAllLines(path);
                    foreach (var i in fileContent)
                        Console.WriteLine(i);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("cat: File Not Found");
                }
            }

        }

        public static void Mkdir(string path)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Mkdir: Virtual Filesystem is not initialised!");
            }
            else
            {
                if (!path.Contains(@":\"))
                    path = Globals.workingdir + path;

                if (Directory.Exists(path))
                    Console.WriteLine("mkdir: directory already exists");
                else
                    Directory.CreateDirectory(path);
            }
        }

        public static void Chdir(string path)
        {
            if (Globals.vFS == null)
                Logger.Log(2, "FS.Chdir: Virtual Filesystem is not initialised!");
            else
            {
                try
                {
                    switch (path)
                    {
                        case "..":
                            string[] splitpath = Globals.workingdir.Split(@"\");

                            //We can't do this:
                            //splitpath = splitpath.SkipLast(1).ToArray();
                            //so instead we'll convert it by hand
                            List<string> temp = new();
                            for (int i = 0; i < splitpath.Length - 2; i++)
                                temp.Add(splitpath[i]);

                            Globals.workingdir = "";
                            for (int i = 0; i < temp.Count; i++)
                                Globals.workingdir += temp[i] + "\\";

                            //Very "hack-like" solution but it's almost 12am, I'll fix it later
                            break;

                        case "$OLDPWD":
                        case "-":
                            //Check if we can even go back to that directory
                            if (Directory.Exists(Globals.oldpwd))
                            {
                                //Using a "tuple" to swap values, as suggested by VS
                                (Globals.oldpwd, Globals.workingdir) = (Globals.workingdir, Globals.oldpwd);
                            }
                            else
                                Console.WriteLine("FS.Chdir: Directory does not exist!");
                            break;
                        default:
                            path = path.Contains(@":\") ? path : Globals.workingdir + path + '\\';

                            if (Directory.Exists(path))
                            {
                                Globals.oldpwd = Globals.workingdir;
                                Globals.workingdir = path;
                            }
                            else
                            {
                                //Ensuring the user cannot go to invalid path
                                Globals.oldpwd = Globals.workingdir;

                                //Telling user about wrong path
                                Console.Write("Directory ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("\"" + path + "\"");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(" not found!");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    ErrorScreen.SpecifiedError(e);
                }
            }

        }
        //Reworked function to support -d and -r
        public static void Remove(List<string> args)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Remove: Virtual Filesystem is not initialised!");
            }
            else
            {
                string path;
                //First, check if params starts with -d or -r
                if (args[1] == "-d")
                {
                    path = args[2];
                    if (!path.Contains(@":\"))
                        path = Globals.workingdir + path;

                    //Check if path is directory
                    if (!Directory.Exists(path) && File.Exists(path))
                    {
                        Console.WriteLine("FS.Remove: Target is a file");
                        return;
                    }

                    //Check if directory is empty
                    if (Directory.Exists(path) && Directory.GetDirectories(path) == Array.Empty<string>() && Directory.GetFiles(path) == Array.Empty<string>())
                        Directory.Delete(path, false);
                    else
                    {
                        Console.WriteLine("FS.Remove: Directory not empty");
                        return;
                    }

                }
                if (args[1] == "-r")
                {
                    path = args[2];
                    if (!path.Contains(@":\"))
                        path = Globals.workingdir + path;

                    //Check if path is directory
                    if (!Directory.Exists(path) && File.Exists(path))
                    {
                        Console.WriteLine("FS.Remove: Target is a file");
                        return;
                    }

                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
                else
                {
                    path = args[1];

                    if (!path.Contains(@":\"))
                        path = Globals.workingdir + path;

                    //Check if path is a file
                    if (Directory.Exists(path) && !File.Exists(path))
                    {
                        Console.WriteLine("FS.Remove: Target is a directory");
                        return;
                    }
                    else
                    {
                        if (File.Exists(path))
                            File.Delete(path);
                        else
                            Console.WriteLine("FS.Remove: File does not exist");
                    }
                }
            }
        }

        public static void Copy(string source, string target)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Copy: Virtual Filesystem is not initialised!");
            }
            else
            {
                if (!source.Contains(@":\"))
                    source = Globals.workingdir + source;

                if (!target.Contains(@":\"))
                    target = Globals.workingdir + target;

                if (File.Exists(source))
                {
                    //It seems like File.Copy is "native code", so we have to do it by hand
                    //File.Copy(source, target);
                    try
                    {
                        byte[] sourceFileContent = File.ReadAllBytes(source);
                        File.Create(target).Close();
                        File.WriteAllBytes(target, sourceFileContent);
                    }
                    catch (Exception e)
                    {
                        ErrorScreen.SpecifiedError(e);
                    }
                }
                else
                    Console.WriteLine("FS.Copy: Source file does not exist");
            }
        }

        public static void Move(string source, string target)
        {
            if (Globals.vFS == null)
            {
                Logger.Log(2, "FS.Move: Virtual Filesystem is not initialised!");
            }
            else
            {
                if (!source.Contains(@":\"))
                    source = Globals.workingdir + source;

                if (!target.Contains(@":\"))
                    target = Globals.workingdir + target;

                if (File.Exists(source))
                {
                    //It seems like File.Move is "native code", so we have to do it by hand
                    //File.Move(source, target);
                    try
                    {
                        byte[] sourceFileContent = File.ReadAllBytes(source);
                        File.Create(target).Close();
                        File.WriteAllBytes(target, sourceFileContent);
                        File.Delete(source);

                    }
                    catch (Exception e)
                    {
                        ErrorScreen.SpecifiedError(e);
                    }
                }

                else
                    Console.WriteLine("FS.Move: Source file does not exist");
            }
        }
    }
}
