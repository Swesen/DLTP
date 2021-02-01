using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    /* CLASS: Person
     * PURPOSE: Used to store contact info about one person.
     */
    class Person
    {
        public string name, address, telephone, email;

        /* METHOD: Person
        * PURPOSE: Constructs a new person from the input parameters.
        * PARAMETERS: Name: The name of the person. Address: The address of the person. Telephone: The telephone number of the person. Email: The e-mail of the person.
        * RETURN VALUE: Person object
        */
        public Person(string Name, string Address, string Telephone, string Email)
        {
            name = Name;
            address = Address;
            telephone = Telephone;
            email = Email;
        }

        /* METHOD: Person
         * PURPOSE: Asks the user to enter the parameters to construct a new Person.
         * RETURN VALUE: Person object
         */
        public Person()
        {
            Console.Write("Namn: ");
            name = Console.ReadLine();

            Console.Write("Adress: ");
            address = Console.ReadLine();

            Console.Write("Telefon: ");
            telephone = Console.ReadLine();

            Console.Write("Email: ");
            email = Console.ReadLine();
        }

        /* METHOD: EditValue
         * PURPOSE: Edits the variable corresponding to the valueName and replaces it with newValue.
         * PARAMETERS: valueName: The Swedish name of the variable to edit. newValue: The value that will replace the variable to edit. 
         * RETURN VALUE: none
         */
        public void EditValue(string valueName, string newValue)
        {
            switch (valueName)
            {
                case "namn":
                    name = newValue;
                    break;

                case "adress":
                    address = newValue;
                    break;

                case "telefon":
                    telephone = newValue;
                    break;

                case "email":
                    email = newValue;
                    break;

                default:
                    break;
            }
        }

        /* METHOD: Print
        * PURPOSE: Prints the contact info of the person to the console.
        * RETURN VALUE: none
        */
        public void Print()
        {
            Console.WriteLine($"{name}, {address}, {telephone}, {email}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Contacts = new List<Person>();

            LoadAddressFile(Contacts);

            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();

                // I prefer to use switch wherever possible.
                switch (command)
                {
                    case "sluta":
                        Console.WriteLine("Hej då!");
                        break;

                    case "ny":
                        AddPerson(Contacts);
                        break;

                    case "ta bort":
                        RemovePerson(Contacts);
                        break;

                    case "visa":
                        for (int i = 0; i < Contacts.Count(); i++)
                        {
                            Contacts[i].Print();
                        }
                        break;

                    case "ändra":
                        EditPerson(Contacts);
                        break;

                    default:
                        Console.WriteLine("Okänt kommando: {0}", command);
                        break;
                }
            } while (command != "sluta");
        }

        /* METHOD: EditPerson (static)
        * PURPOSE: Asks the user for a name and a value to edit in the List<Person>.
        * PARAMETERS: Contacts: reference to the list to edit.
        * RETURN VALUE: none
        */
        private static void EditPerson(List<Person> Contacts)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string editContactWithName = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Contacts.Count(); i++)
            {
                if (Contacts[i].name == editContactWithName) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", editContactWithName);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string infoToEdit = Console.ReadLine();

                Console.Write("Vad vill du ändra {0} på {1} till: ", infoToEdit, editContactWithName);
                string newValue = Console.ReadLine();

                switch (infoToEdit)
                {
                    case "namn": Contacts[found].name = newValue; break;
                    case "adress": Contacts[found].address = newValue; break;
                    case "telefon": Contacts[found].telephone = newValue; break;
                    case "email": Contacts[found].email = newValue; break;
                    default: break;
                }
            }
        }

        /* METHOD: RemovePerson (static)
        * PURPOSE: Asks the user for a name to remove from the List<Person>.
        * PARAMETERS: Contacts: reference to the list to edit.
        * RETURN VALUE: none
        */
        private static void RemovePerson(List<Person> Contacts)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string nameToRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Contacts.Count(); i++)
            {
                if (Contacts[i].name == nameToRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameToRemove);
            }
            else
            {
                Contacts.RemoveAt(found);
            }
        }

        /* METHOD: AddPerson (static)
        * PURPOSE: User gets prompted to enter the values to add a new Person to List<Person>.
        * PARAMETERS: Contacts: reference to the list to add a new Person to.
        * RETURN VALUE: none
        */
        private static void AddPerson(List<Person> Contacts)
        {
            Console.WriteLine("Lägger till ny person");
            Contacts.Add(new Person());
        }

        /* METHOD: LoadAddressFile (static)
        * PURPOSE: Loads the address.lis file .
        * PARAMETERS: Contacts: reference to the list to edit.
        * RETURN VALUE: none
        */
        private static void LoadAddressFile(List<Person> Contacts)
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();

                    string[] word = line.Split('#');

                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Contacts.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}
