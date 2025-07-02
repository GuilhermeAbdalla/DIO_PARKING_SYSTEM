using MySql.Data.MySqlClient;
using ParkingSystem.Common;

public struct CustomerStruct
{
    public int idCustomer;
    public string nameCustomer;
    public string docCustomer;
    public string phoneCustomer;
    public string emailCustomer;
    public int activeSituation;
}

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
            List<CustomerStruct> customers = new List<CustomerStruct>();
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    CustomerStruct customerTemp = new CustomerStruct();
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