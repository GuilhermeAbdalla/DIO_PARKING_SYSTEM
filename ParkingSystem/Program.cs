using System.Text.RegularExpressions;

namespace ParkingSystem
{
    class Program
    {
        static bool showMenu;
        static void Main()

        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Welcome to the Parking System!\n" +
                            "Choose an option:");
            showMenu = true;

            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("1 - Register a Customer");
                Console.WriteLine("2 - Register a Vehicle");
                Console.WriteLine("3 - Vehicle Entry");
                Console.WriteLine("4 - Vehicle Exit");
                Console.WriteLine("5 - List of Customers");
                Console.WriteLine("6 - List of Vehicles");
                Console.WriteLine("7 - System Settings");
                Console.WriteLine("0 - Close");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterCustomer();
                        break;
                    case "2":
                        RegisterVehicle();
                        break;
                    case "3":
                        RegisterVehicleEntry();
                        break;
                    case "4":
                        RegisterVehicleExit();
                        break;
                    case "5":
                        ListCustomers();
                        break;
                    case "6":
                        ListVehicles();
                        break;
                    case "7":
                        SetSystemSettings();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Option. Try Again.");
                        Console.ReadKey();
                        break;
                }

            }
        }

        static void RegisterCustomer()
        {
            Console.Clear();

            Console.WriteLine("--- Register a Customer ---");
            Console.WriteLine("Type the customer name");
            Console.Write("Name: ");
            string? customerName = Console.ReadLine();

            while (string.IsNullOrEmpty(customerName) || customerName.Length < 3)
            {
                Console.WriteLine($"\"{customerName}\" is invalid. The name must have at least 3 characters. Try Again.");
                Console.Write("Name: ");
                customerName = Console.ReadLine();
            }

            Console.WriteLine("Type the customer document");
            Console.Write("Document:");
            string? customerDocument = Console.ReadLine();

            while (!validateDocument(customerDocument))
            {
                Console.WriteLine($"The Document {customerDocument} is invalid, type again");
                Console.Write("Document: ");
                customerDocument = Console.ReadLine();
            }

            Console.WriteLine("Type the customer Phone Number");
            Console.Write("Phone Number: ");
            string? customerPhoneNumber = Console.ReadLine();

            while (!validatePlate(customerPhoneNumber))
            {
                Console.WriteLine($"The Phone {customerPhoneNumber} is invalid, type again");
                Console.Write("Phone Number: ");
                customerPhoneNumber = Console.ReadLine();
            }

            Console.WriteLine("Type the customer's email");
            Console.Write("Email: ");
            string? customerEmail = Console.ReadLine();

            while (!validateEmail(customerEmail))
            {
                Console.WriteLine($"The Email {customerEmail} is invalid, type again");
                Console.Write("Email: ");
                customerEmail = Console.ReadLine();
            }

            ParkingManager.registerCustomer(customerName, customerDocument, customerPhoneNumber, customerEmail);
        }

        static void RegisterVehicle()
        {
            Console.Clear();

            Console.WriteLine("--- Register a Vehicle ---");
            Console.WriteLine("Type the Owner ID");

            int customerID = 0;
            bool validCustomerID = false;
            string? customerName = " ";

            while (!validCustomerID)
            {
                Console.Write("Customer ID: ");
                try
                {
                    validCustomerID = int.TryParse(Console.ReadLine(), out customerID);
                    //Consultar ID no banco e verificar o nome do customer que tem esse nome. Passar para o customerName;
                    customerName = ParkingManager.getCustomerName($"SELECT * FROM customer WHERE idCustomer = {customerID}");
                    Console.WriteLine($"This vehicle will belong to {customerName}");
                    validCustomerID = true;
                }
                catch
                {
                    Console.WriteLine("This Customer ID doesn't exist. Try Again");
                    validCustomerID = false;
                }
            }

            Console.WriteLine("Now, type the Vehicle License Plate");
            Console.Write("License Plate: ");

            string? licensePlate = Console.ReadLine();

            //O usuário digita a License Plate do veículo;  
            while (!validateLicensePlate(licensePlate))
            {
                Console.WriteLine($"This License plate \"{licensePlate}\" is invalid. Try Again.");
                licensePlate = Console.ReadLine();
            }

            Console.Write("Vehicle Maker: ");
            string? vehicleMaker = Console.ReadLine();

            //O usuário digita a fabricante do veículo;
            while (vehicleMaker == " ")
            {
                Console.WriteLine("This field cannot be empty, try again.");
            }

            //O usuário digita o modelo do veículo;
            Console.Write("Vehicle model: ");
            string? vehicleModel = Console.ReadLine();

            while (vehicleModel == " ")
            {
                Console.WriteLine("This field cannot be empty, try again.");
            }

            //O usuário digita a cor do veículo;
            Console.Write("Vehicle color: ");
            string? vehicleColor = Console.ReadLine();

            while (vehicleColor == " ")
            {
                Console.WriteLine("This field cannot be empty, try again.");
            }

            Console.WriteLine("Congratulations! You registered a vehicle!\n" +
                                $"Customer Name: {customerName}\n" +
                                $"Vehicle License Plate: {licensePlate}\n" +
                                $"Vehicle: {vehicleColor} {vehicleMaker} {vehicleModel}"
                                );

            ParkingManager.registerVehicle(customerID, licensePlate, vehicleMaker, vehicleModel, vehicleColor);
        }

        static void RegisterVehicleEntry()
        {
            Console.WriteLine("Type the Vehicle ID to Register the entry");
            int typedVehicleId = 0;

            bool idIsNumber = int.TryParse(Console.ReadLine(), out typedVehicleId);
            bool vehicleExists = ParkingManager.verifyIfVehicleExistsinBd(typedVehicleId);
            while (!idIsNumber || !vehicleExists)
            {
                Console.WriteLine("Invalid ID, type again");
                idIsNumber = int.TryParse(Console.ReadLine(), out typedVehicleId);
                vehicleExists = ParkingManager.verifyIfVehicleExistsinBd(typedVehicleId);
            }

            bool hasEntryWithoutExit = ParkingManager.verifyIfHasEntry(typedVehicleId);
            if (hasEntryWithoutExit)
            {
                Console.WriteLine("This vehicle has entry without exit");
                Console.WriteLine("Press any key to return to select menu");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("You can entry this vehicle");
            }
            Console.WriteLine("Press any key to entry with the vehicle");
            Console.ReadKey();

            ParkingManager.registerEntry(typedVehicleId);
        }

        static void RegisterVehicleExit()
        {
            Console.WriteLine("Type the Vehicle ID to Register the exit");
            int typedVehicleId = 0;

            bool idIsNumber = int.TryParse(Console.ReadLine(), out typedVehicleId);
            bool vehicleExists = ParkingManager.verifyIfVehicleExistsinBd(typedVehicleId);
            while (!idIsNumber || !vehicleExists)
            {
                Console.WriteLine("Invalid ID, type again");
                idIsNumber = int.TryParse(Console.ReadLine(), out typedVehicleId);
                vehicleExists = ParkingManager.verifyIfVehicleExistsinBd(typedVehicleId);
            }

            bool hasEntryWithoutExit = ParkingManager.verifyIfHasEntry(typedVehicleId);
            if (hasEntryWithoutExit)
            {
                Console.WriteLine("You can Register the vehicle exit");            
            }
            else
            {
                Console.WriteLine("You can't exit with a vehicle that has no entry");
                Console.ReadKey();
                return;
            }

            ParkingManager.registerExit(typedVehicleId);
        }

        static void ListCustomers()
        {
            Console.Clear();

            Console.WriteLine("--- List of Customers ---");
            Console.WriteLine("ID\t\tNAME\t\tCPF\t\t\tPHONE\t\tEMAIL\t\tSITUATION");
            ParkingManager.printAllCustomers();
            Console.ReadKey();
            Console.Clear();
        }
        static void ListVehicles()
        {
            Console.Clear();

            Console.WriteLine("--- List of Vehicles ---");
            Console.WriteLine("ID\t\tOWNER\t\tLICENSE PLATE\t\t\tMAKER\t\tMODEL\t\tCOLOR");
            ParkingManager.printAllVehicles();
            Console.ReadKey();
            Console.Clear();
        }

        static void SetSystemSettings()
        {
            Console.WriteLine("Type the new price per hour for parking [Format: 00.00]");

            decimal newPricePerHour;
            bool canBePriced = decimal.TryParse(Console.ReadLine(), out newPricePerHour);

            while (!canBePriced)
            {
                Console.WriteLine("Invalid Price, type again");
                canBePriced = decimal.TryParse(Console.ReadLine(), out newPricePerHour);
            }

            ParkingManager.setSystemSettings(newPricePerHour);
            Console.WriteLine($"The new price is: ${newPricePerHour}/hr");
            Console.ReadKey();
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

        static bool validateEmail(string email)
        {
            string validEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, validEmail);
        }
        static bool validateLicensePlate(string licensePlate)
        {
            string validLicensePlateMercosul = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
            string validLicensePlateOld = @"^[A-Z]{3}-?[0-9]{4}$";
            return Regex.IsMatch(licensePlate, validLicensePlateMercosul) || Regex.IsMatch(licensePlate, validLicensePlateOld);
        }
        static bool validateID(string id)
        {
            string validId = @"^\d";
            return Regex.IsMatch(id, validId);
        }
    }
}