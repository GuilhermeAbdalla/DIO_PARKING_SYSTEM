using System.Data.SqlTypes;
using MySql.Data.MySqlClient;
using ParkingSystem.Common;

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
    }
}