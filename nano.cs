using grapeFruitOSCSharp.Filesystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace grapeFruitRebuild
{
    public  class nano
    {
        public class Nanomain
        {
            const string verNum = "0.4";
            static string fileName = "";
            static string displayedFileName = "";
            static string[] splitFileName = { };
            static int cursorX, cursorY;

            static List<string> text;

            static bool saveFileFooter;

            public static void Main(string file = "")
            {
                //Passing file parameter to internal "fileName" to avoid confusion
                fileName = file;
                cursorX = 0;
                cursorY = 0;

                text = new List<string>();
                if (fileName == "")
                {
                    text.Add("");
                }
                else
                {

                    //Checking for absolute path
                    if (!fileName.Contains(@":\"))
                    {
                        fileName = Globals.workingdir + fileName;
                    }

                    //Checking for file
                    if (File.Exists(fileName))
                        text = LoadFile(fileName);
                    else
                    {
                        //File does not exist, we'll create it later
                        text.Add("");
                    }
                }

                while (true)
                {
                    if (fileName != "")
                    {
                        if (fileName.Length <= 25)
                            displayedFileName = fileName;
                        else
                        {
                            splitFileName = fileName.Split('\\');
                            displayedFileName = splitFileName[splitFileName.Length - 1];
                        }
                    }
                    else
                        displayedFileName = "<unnamed>";


                    Render(text, cursorX, cursorY, saveFileFooter);
                    if (saveFileFooter)
                    {
                        saveFileFooter = false;
                    }

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
                    {
                        // Quit the editor
                        Console.Clear();
                        break;
                    }
                    if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.O)
                    {
                        // Save the file
                        SaveFile2(text, fileName);
                    }

                    #region Input logic
                    switch (key.Key)
                    {
                        case ConsoleKey.Enter:
                            // Insert a new line
                            text.Insert(cursorY + 1, text[cursorY][cursorX..]);
                            text[cursorY] = text[cursorY][..cursorX];
                            cursorY++;
                            cursorX = 0;
                            break;
                        case ConsoleKey.Backspace:
                            // Delete the character to the left of the cursor
                            if (cursorX > 0)
                            {
                                text[cursorY] = text[cursorY].Remove(cursorX - 1, 1);
                                cursorX--;
                            }
                            else if (cursorY > 0)
                            {
                                int lineLength = text[cursorY - 1].Length;
                                text[cursorY - 1] += text[cursorY];
                                text.RemoveAt(cursorY);
                                cursorY--;
                                cursorX = lineLength;
                            }
                            break;
                        case ConsoleKey.Delete:
                            // Delete the character at the cursor
                            if (cursorX < text[cursorY].Length)
                            {
                                text[cursorY] = text[cursorY].Remove(cursorX, 1);
                            }
                            else if (cursorY < text.Count - 1)
                            {
                                text[cursorY] += text[cursorY + 1];
                                text.RemoveAt(cursorY + 1);
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            // Move the cursor left
                            if (cursorX > 0)
                            {
                                cursorX--;
                            }
                            else if (cursorY > 0)
                            {
                                cursorY--;
                                cursorX = text[cursorY].Length;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            // Move the cursor right
                            if (cursorX < text[cursorY].Length)
                            {
                                cursorX++;
                            }
                            else if (cursorY < text.Count - 1)
                            {
                                cursorY++;
                                cursorX = 0;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            // Move the cursor up
                            if (cursorY > 0)
                            {
                                cursorY--;
                                cursorX = Math.Min(cursorX, text[cursorY].Length);
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            // Move the cursor down
                            if (cursorY < text.Count - 1)
                            {
                                cursorY++;
                                cursorX = Math.Min(cursorX, text[cursorY].Length);
                            }
                            break;
                        case ConsoleKey.End:
                            //Move cursor to end of line
                            if (cursorX < text[cursorY].Length)
                            {
                                cursorX = text[cursorY].Length;
                            }
                            break;
                        case ConsoleKey.Home:
                            //Move cursor to beginning of line
                            cursorX = 0;
                            break;
                        default:
                            if (key.KeyChar >= ' ')
                            {
                                // Insert a character at the cursor
                                string line = text[cursorY];
                                text[cursorY] = line[..cursorX] + key.KeyChar + line[cursorX..];
                                cursorX++;
                                /*selectionStartX = cursorX;
                                selectionStartY = cursorY;*/
                            }
                            break;
                    }
                    #endregion
                }
            }



            static void Render(List<string> text, int cursorX, int cursorY, bool saved = false)
            {
                Console.Clear();
                //Writing window header
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("GrapeFruit Nano " + verNum + " | Editing: " + displayedFileName);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(cursorX, cursorY);

                for (int i = 0; i < text.Count; i++)
                {
                    Console.SetCursorPosition(0, i + 1);
                    Console.Write(text[i]);
                }

                //Writing small footer
                if (!saved)
                {
                    Console.SetCursorPosition(0, 24);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Ctrl-O: Save // Ctrl-X: Exit instantly");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(0, 24);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Saved file as " + fileName);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.SetCursorPosition(cursorX, cursorY + 1);
            }

            static void SaveFile2(List<string> text, string filePathParam)
            {
                //Trying to make the code a bit cleaner

                //Check if filePathParam is empty
                if(filePathParam == "")
                {
                    //Nicer save dialog
                    Console.SetCursorPosition(0, 24);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("                                      ");
                    Console.SetCursorPosition(0, 24);
                    Console.Write("Enter file name: ");
                    fileName = Console.ReadLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    if (!fileName.Contains(@":\"))
                    {
                        fileName = Globals.workingdir + fileName;
                    }
                }
                else
                {
                    fileName = filePathParam;
                }

                //Output filename is now set, check if it exists
                if(!File.Exists(fileName))
                {
                    //If the file does not exist, create it and add a null term so the system doesn't crash
                    FS.Touch(fileName);
                }

                //File path set, file created/already exists, write content
                StreamWriter writer = new(fileName);
                foreach (string line in text)
                {
                    writer.WriteLine(line);
                }
                writer.Close();

                splitFileName = fileName.Split('\\');
                displayedFileName = splitFileName[splitFileName.Length - 1];

                saveFileFooter = true;
            }

            static List<string> LoadFile(string path)
            {
                List<string> temp = new();
                string[] fileContent = File.ReadAllLines(path);

                foreach (string line in fileContent)
                    temp.Add(line);

                return temp;

            }
        }
    }
}
