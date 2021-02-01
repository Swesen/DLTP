using System;
using System.Collections.Generic;
using System.IO;

namespace Inlämningsuppgift_1___Adressbok
{
    internal class Program
    {
        private class Person
        {
            public string name, address, telephone, email;

            public Person(string name, string address, string telephone, string email)
            {
                this.name = name;
                this.address = address;
                this.telephone = telephone;
                this.email = email;
            }

            public string GetInfo()
            {
                return $"{name}, {address}, {telephone}, {email}";
            }
        }

        private static string addressBookFileLocation = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\addressbook.txt";
        private static List<Person> addressBook = new List<Person>();

        private static void Main(string[] args)
        {
            LoadAddressBook();

            while (UserInteraction())
            {
                Console.WriteLine("\nTryck på valfri knapp för att fortsätta.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static bool UserInteraction()
        {
            Console.WriteLine(
                "Välj vad du vill göra med adressboken. Tillgängliga kommandon:\n" +
                "'ny' Lägg till en ny person i din adressbok.\n" +
                "'ändra' Lista och välje ne person att ändra från din adressbok.\n" +
                "'ta bort' Lista och välj en person att ta bort från din adressbok.\n" +
                "'visa' Listar innehållet i din adressbok.\n" +
                "'spara' Sparar adressboken.\n" +
                "'avlsuta' Avslutar programmet, välj om du vill spara eller ej.\n");

            switch (Console.ReadLine().ToLower())
            {
                case "ny":
                    AddPerson();
                    break;

                case "ändra":
                    ChangePersonInfo();
                    break;

                case "ta bort":
                    RemovePerson();
                    break;

                case "visa":
                    PrintAddressBookList();
                    break;

                case "spara":
                    SaveAddressBook();
                    break;

                case "avsluta":
                    Quit();
                    return false;

                default:
                    Console.WriteLine($"Känner inte igen kommandot!");
                    break;
            }
            return true;
        }

        private static void ChangePersonInfo()
        {
            PrintNumberedAddressBookList();

            Console.Write("Ange numret du vill ändra: ");
            string input = Console.ReadLine();

            // Make sure that input is numerical and that it is within addressBook index
            if (int.TryParse(input, out int result) && result - 1 < addressBook.Count)
            {
                int index = result - 1;
                do
                {
                    Console.WriteLine($"\n" +
                        $"Vad vill du ändra?\n" +
                        $"namn: {addressBook[index].name}\n" +
                        $"adress: {addressBook[index].address}\n" +
                        $"telefon: {addressBook[index].telephone}\n" +
                        $"email: {addressBook[index].email}");

                    string choise = Console.ReadLine().ToLower();
                    switch (choise)
                    {
                        case "namn":
                            Console.Write("Skriv i nytt namn: ");
                            addressBook[index].name = Console.ReadLine();
                            break;

                        case "adress":
                            Console.Write("Skriv i ny adress: ");
                            addressBook[index].address = Console.ReadLine();
                            break;

                        case "telefon":
                            Console.Write("Skriv i nytt telefonnummer: ");
                            addressBook[index].telephone = Console.ReadLine();
                            break;

                        case "email":
                            Console.Write("Skriv i ny email: ");
                            addressBook[index].email = Console.ReadLine();
                            break;

                        default:
                            Console.WriteLine($"Kommando {choise} känns inte igen!");
                            break;
                    }

                    Console.WriteLine(
                        "Vill du ändra något mer?\n" +
                        "'J'a/'N'ej");
                } while (Char.ToLower(Console.ReadKey().KeyChar) == 'j');
            }
        }

        private static void PrintAddressBookList()
        {
            Console.Clear();
            Console.WriteLine("Visar adressboken:");
            foreach (var person in addressBook)
            {
                Console.WriteLine(person.GetInfo());
            }
        }

        private static void PrintNumberedAddressBookList()
        {
            Console.Clear();
            for (int i = 0; i < addressBook.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {addressBook[i].GetInfo()}");
            }
        }

        private static void AddPerson()
        {
            Console.WriteLine("Lägg till en person:");
            Console.Write("Namn: ");
            string name = Console.ReadLine();
            Console.Write("Adress: ");
            string address = Console.ReadLine();
            Console.Write("Telefon: ");
            string telephone = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            addressBook.Add(new Person(name, address, telephone, email));
        }

        private static void RemovePerson()
        {
            PrintNumberedAddressBookList();

            Console.Write("Ange numret du vill ta bort: ");
            string input = Console.ReadLine();

            // Make sure that input is numerical and that it is within addressBook index
            if (int.TryParse(input, out int result) && result - 1 < addressBook.Count)
            {
                Console.WriteLine($"Ta bort: {addressBook[result - 1].GetInfo()}?");
                Console.WriteLine("'J'a/'N'ej");

                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (Char.ToLower(key))
                {
                    case 'j':
                        addressBook.RemoveAt(result - 1);
                        Console.WriteLine("Togs bort!");
                        break;

                    default:
                        Console.WriteLine("Togs ej bort!");
                        break;
                }
            }
        }

        private static void LoadAddressBook()
        {
            StreamReader reader;

            try
            {
                reader = new StreamReader(addressBookFileLocation);
            }
            catch (Exception)
            {
                Console.WriteLine($"Ingen adressbok hittad på plats: {addressBookFileLocation}\n");
                return;
            }

            while (!reader.EndOfStream)
            {
                string[] data = reader.ReadLine().Split(';');
                addressBook.Add(new Person(data[0], data[1], data[2], data[3]));
            }
            reader.Close();
        }

        private static void SaveAddressBook()
        {
            StreamWriter writer = new StreamWriter(addressBookFileLocation);

            foreach (var person in addressBook)
            {
                // If type Person was more complicated I would make a more dynamic way of doing this.
                writer.WriteLine($"{person.name};{person.address};{person.telephone};{person.email}");
            }

            writer.Close();
            Console.WriteLine("Adressbok sparad: " + addressBookFileLocation);
        }

        private static void Quit()
        {
            char input;
            // Make sure that the user makes a valid choise, to prevent accidental keystrokes from deleting changes.
            do
            {
                Console.Clear();
                Console.WriteLine(
                    "Spara addressbok innan du avslutar?\n" +
                    "'J'a/'N'ej");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
            } while (!(input == 'j' || input == 'n'));

            if (Char.ToLower(input) == 'j')
            {
                SaveAddressBook();
            }
        }
    }
}