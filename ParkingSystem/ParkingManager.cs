using MySql.Data.MySqlClient;

namespace ParkingSystem
{
    static class ParkingManager
    {
        static string data_source = "datasource=localhost;username=root;password=Guilhermeabdalla007madmax@;database=db_parkingsystem";
        static MySqlConnection connection = new MySqlConnection(data_source);
        public static void registerCustomer(string name, string document, string phoneNumber, string email)
        {
            string sql = "INSERT INTO customer (nameCustomer, docCustomer, phoneCustomer, emailcustomer)" +
                $"  VALUES (\'{name}\', \'{document}\', \'{phoneNumber}\', \'{email.ToLower()}\')";

            executeQuery(sql);
        }

        public static void registerVehicle(int idCustomer, string licensePlate, string maker, string model, string color)
        {
            string sql = "INSERT INTO vehicle (idCustomer, licensePlate, maker, model, color)" +
                         $" VALUES ({idCustomer}, \'{licensePlate}\', \'{maker}\', \'{model}\', \'{color}\')";
            executeQuery(sql);
        }

        public static string getCustomerName(string sql)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Customer customerTemp = new Customer();
                    customerTemp.idCustomer = reader.GetInt32(0);
                    customerTemp.nameCustomer = reader.GetString(1);
                    customerTemp.docCustomer = reader.GetString(2);
                    customerTemp.phoneCustomer = reader.GetString(3);
                    customerTemp.emailCustomer = reader.GetString(4);
                    customerTemp.activeSituation = reader.GetInt32(5);

                    customers.Add(customerTemp);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                connection.Close();
            }
            return customers[0].nameCustomer;
        }

        public static void printAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            string sql = "SELECT veh.idVehicle, customer.nameCustomer, veh.licensePlate, veh.maker, veh.model, veh.color FROM vehicle veh LEFT JOIN customer ON veh.idCustomer = customer.idCustomer";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.idVehicle = reader.GetInt32(0);
                    vehicle.nameCustomer = reader.GetString(1);
                    vehicle.licensePlate = reader.GetString(2);
                    vehicle.maker = reader.GetString(3);
                    vehicle.model = reader.GetString(4);
                    vehicle.color = reader.GetString(5);

                    vehicles.Add(vehicle);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                connection.Close();
            }
            foreach (Vehicle veh in vehicles)
            {
                Console.WriteLine(veh.ToString());
            }

        }

        public static void printAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string sql = "SELECT * FROM customer";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.idCustomer = reader.GetInt32(0);
                    customer.nameCustomer = reader.GetString(1);
                    customer.docCustomer = reader.GetString(2);
                    customer.phoneCustomer = reader.GetString(3);
                    customer.emailCustomer = reader.GetString(4);
                    customer.activeSituation = reader.GetInt32(5);

                    customers.Add(customer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                connection.Close();
            }
            foreach (Customer cust in customers)
            {
                Console.WriteLine(cust.ToString());
            }

        }

        public static bool verifyIfVehicleExistsinBd(int typedVehicleId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            string sql = $"SELECT idVehicle FROM vehicle WHERE idVehicle = {typedVehicleId}";
            bool isValidId = false;
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();

                    vehicle.idVehicle = reader.GetInt32(0);

                    vehicles.Add(vehicle);

                    if (vehicles[0] != null)
                    {
                        isValidId = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID doesn't exists in Db");
            }
            finally
            {
                connection.Close();
            }
            return isValidId;
        }

        public static bool verifyIfHasEntry(int typedVehicleId)
        {
            string sql = $"SELECT idVehicle FROM entryexit WHERE idVehicle = {typedVehicleId} AND exitTime IS NULL";

            bool hasEntryWithoutExit = true;

            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    hasEntryWithoutExit = true;
                }
                else
                {
                    hasEntryWithoutExit = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }

            return hasEntryWithoutExit;
        }

        public static void registerEntry(int typedVehicleId)
        {
            string sql = $"INSERT INTO entryexit (idVehicle, entryTime) VALUES({typedVehicleId}, CURRENT_TIME())";

            executeQuery(sql);
        }

        public static void registerExit(int typedVehicleId)
        {
            decimal parkingValuePerHour = getParkingValue(typedVehicleId);

            Console.WriteLine($"The Total Price is ${parkingValuePerHour}");

            Console.Write("Press any key to pay");
            Console.ReadKey();

            string sql = $"UPDATE entryexit SET exitTime = CURRENT_TIME(), totalPrice = {parkingValuePerHour} WHERE idVehicle = {typedVehicleId} AND exitTime IS NULL";
            executeQuery(sql);


        }

        static void executeQuery(string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                command.ExecuteReader();

                Console.WriteLine($"You registered a data sucessfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                connection.Close();
            }

        }

        static decimal getParkingValue(int typedVehicleId)
        {

            string sqlDateDiff = $"SELECT TIMESTAMPDIFF(HOUR, entryTime, CURRENT_TIME()) as DateDiff FROM entryexit WHERE idVehicle = {typedVehicleId} AND exitTime IS NULL";

            int dateDiff = 0;

            try
            {
                MySqlCommand command = new MySqlCommand(sqlDateDiff, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dateDiff = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Exception 1");
            }
            finally
            {
                connection.Close();
            }

            string sqlPricePerHour = "SELECT settingValue FROM systemsettings WHERE settingDescription = 'PARKINGPRICEPERHOUR'";

            try
            {
                MySqlCommand command = new MySqlCommand(sqlPricePerHour, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return decimal.Parse(reader.GetString(0)) * dateDiff;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Exception 2");
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }
        public static void setSystemSettings(decimal priceValue)
        {
            string sql = $"UPDATE systemsettings SET settingvalue = {priceValue} WHERE settingDescription = \'PARKINGPRICEPERHOUR\'";

            executeQuery(sql);
        }
    }
}