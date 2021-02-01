using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_1
{
    class Program
    {
        class Task
        {
            public string date, entry;
            public char status;
            public Task(string date, char status, string entry)
            {
                this.date = date;
                this.status = status;
                this.entry = entry;
            }
        }

        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.Clear();
                input = Console.ReadLine();

                string[] command = input.Split(' ');

                switch (command[0])
                {
                    case "quit":
                        break;

                    case "load":
                        bool fileOpen = LoadList(command, out List<Task> list);

                        while (fileOpen)
                        {
                            Console.WriteLine("Current list:");
                            PrintList(list);

                            input = Console.ReadLine();

                            command = input.Split(' ');
                            switch (command[0])
                            {
                                case "move":
                                case "delete":
                                case "add":
                                case "set":
                                    break;

                                case "save":
                                    SaveList(command, list);
                                    fileOpen = false;
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;

                    default:
                        break;
                }
            } while (input != "quit");
        }

        private static void SaveList(string[] command, List<Task> list)
        {
            throw new NotImplementedException();
        }

        private static void PrintList(List<Task> list)
        {
            Console.WriteLine("N  date   S titel");
            Console.WriteLine("----------------------------------------------------------------");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(i + 1 + ":");

                Console.CursorLeft = 3;
                Console.Write(list[i].date);

                Console.CursorLeft = 10;
                Console.Write(list[i].status);

                Console.CursorLeft = 12;
                Console.WriteLine(list[i].entry);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        private static bool LoadList(string[] command, out List<Task> toDoList)
        {
            toDoList = new List<Task>();

            if (command.Length == 2)
            {
                StreamReader reader;
                try
                {
                    Console.Write($"Loading {command[1]} ");
                    reader = new StreamReader(command[1]);
                }
                catch (Exception)
                {
                    Console.WriteLine($"File: {command[1]} not found!");
                    return false;
                }

                while (!reader.EndOfStream)
                {
                    Console.Write(".");
                    string[] line = reader.ReadLine().Split(';');
                    toDoList.Add(new Task(line[0], line[1][0], line[2]));
                }

                Console.WriteLine(" Loaded!");
            }

            return false;
        }
    }
}
