using System.Linq.Expressions;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
/* NOTES
 * author: @Jesper-Andersson
 * license: Unlicense
 * 
 * STARTUP:
 *  Make Fullscreen.
 *  
 * OVERVIEW window:
 *  Displays all notes numbered with date and "title"
 *  Commands:
 *      help
 *      new (open new blank NOTE window)
 *      open <number/title string>
 *      delete <number/title string>
 *      rename <number/title string>
 *      duplicate <number/title string>
 *      exit
 * NOTE window:
 *  1. Prompt to enter title
 *  2. Display Title in the top left, creation Date top right.
 *  3. Enter text or commands
 *  4. Line numbers
 *  Commands:
 *      /save   (save as Date+Title.txt)
 *      /edit   <line number>
 *      /copy   <line number>
 *      /cut    <line number>
 *      /paste  <line number>
 *      /rename <new title>
 *      /exit   (prompt "Want to save?: (Y/N)) 
*/

namespace Notes
{
    internal class Program
    {
        static string notesFolderName = Directory.GetCurrentDirectory() + @"/Notes/";
        static string windowTitle = "Notes";
        

        static void Main(string[] args)
        {
            Initialize();
        }

        static void Initialize()
        {
            Console.Title = windowTitle;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.Clear();

            //Create "Notes" folder in current path if it doesn't exist.
            if (!Directory.Exists(notesFolderName))
            {
                Directory.CreateDirectory(notesFolderName);
                Console.WriteLine($"Created directory: " + notesFolderName);;
                Console.ReadLine();
                Note();
            }
            else
            {
                Overview();
            }
            Console.ReadLine();
        }
        static void Note()
        {
            Console.WriteLine("Please enter title for note: ");
            string noteTitle = Console.ReadLine();
            string[] text = new string[] {};
            DateTime dateTime = DateTime.Now;

            if (noteTitle == null)
            {
                noteTitle = "Untitled";   
            }

            Console.Clear();
            bool inputActive = true;
            
            //loop to get and save input and check for commands.
            while (inputActive)
            {
                Console.WriteLine(noteTitle + "                                          " + dateTime.ToString());
                string input = Console.ReadLine();
                if (input.ElementAt(0) == '/')
                {
                    if (input.Contains("/save")){
                        inputActive = false;
                        Save(dateTime, noteTitle, text);
                    }
                    else 
                    {
                        text[text.Length] = input;
                    }
                }
                else 
                {
                    text[text.Length] = input;
                }

            }
            Overview();
        }

        static void Overview()
        {
            if (Console.ReadLine().Contains("new"))
            {
                Note();
            }
        }

        static void ReadNotes()
        {
            if (Directory.EnumerateFiles(notesFolderName).Count() != 0)
            {
                
            }
        }
        static void Save(DateTime dateTime, string title, string[] text)
        {
            // 31-12-2022-23:59|Title.txt
            string noteFileName = $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year}|{dateTime.Hour}:{dateTime.Minute}|{title}.txt";
            string noteFilePath = notesFolderName + noteFileName;
            
            for (int i = 0; i < text.Length; i++)
            {
                File.AppendText(noteFilePath).WriteLine(text[i]);
            }
        }
    }
}