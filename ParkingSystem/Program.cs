using System;
using ParkingSystem.Common;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;

namespace ParkingSystem
{
    class Program
    {
        static bool showMenu;
        static void Main(string[] args)

        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Welcome to the Parking System!\n" +
                            "Choose an option:");
            showMenu = true;

            while (showMenu)
            {
                Console.WriteLine("1 - Register a Customer");
                Console.WriteLine("2 - Register a Vehicle");
                Console.WriteLine("3 - Vehicle Entry");
                Console.WriteLine("4 - Vehicle Exit");
                Console.WriteLine("5 - List of Customers");
                Console.WriteLine("6 - List of Vehicles");
                Console.WriteLine("7 - System Settings");
                Console.WriteLine("0 - Close");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        registerCustomer();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Escolha Novamente.");
                        Console.ReadKey();
                        break;
                }

            }
        }

        static void registerCustomer()
        {
            Console.Clear();

            Console.WriteLine("--- Register a Customer ---");
            Console.WriteLine("Type the customer name");
            Console.Write("Name: ");
            string customerName = Console.ReadLine();

            while (string.IsNullOrEmpty(customerName) || customerName.Length < 3)
            {
                Console.WriteLine($"\"{customerName}\" is invalid. The name must have at least 3 characters. Try Again.");
                Console.Write("Name: ");
                customerName = Console.ReadLine();
            }

            Console.WriteLine("Type the customer document");
            Console.Write("Document:");
            string document = Console.ReadLine();

            while (!validateDocument(document))
            {
                Console.WriteLine($"The Document {document} is invalid, type again");
                Console.Write("Document: ");
                document = Console.ReadLine();
            }

            Console.WriteLine("Type the customer Phone Number");
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            while (!validatePlate(phoneNumber))
            {
                Console.WriteLine($"The Phone {phoneNumber} is invalid, type again");
                Console.Write("Phone Number: ");
                phoneNumber = Console.ReadLine();
            }
        }

        static bool validatePlate(string phoneNumber)
        {
            string validPhone = @"^\(?\d{2}\)?\s?(\d{1})?\d{4}-?\d{4}$";
            return Regex.IsMatch(phoneNumber, validPhone);
        }

        static bool validateDocument(string documentNumber)
        {
            string validDocument = @"^\d{3}.?\d{3}.?\d{3}-?\d{2}$";
            return Regex.IsMatch(documentNumber, validDocument);
        }
    }
}