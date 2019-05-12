using System;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    //Class Employee handles functions that employees can do
    public class Employee
    {
        //The variables for an employee, same as in the database
        public int EmployeeId;
        public string EmployeeName;
        public int Pin;
        public int ClockedIn;
        public int Manager;

        //AssignStaffToOrder assigns a staff to an order
        //Input is an employee's name and orderID
        public static void AssignStaffToOrder(string e, long o)
        {
            Employee employee = new Employee();
            employee.EmployeeName = e;

            //Connect to the database and run the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //The first query gets the employeeID from the employee name that was chosen
                string sql = "SELECT employeeID FROM memonEmployee WHERE employeeName = '" + employee.EmployeeName + "';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader;

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee.EmployeeId = reader.GetInt32("employeeID");
                }
                reader.Close();

                //The second SQL statment inserts the employee and the orderId into the table memonDrivers

                sql = "INSERT INTO memonDrivers VALUES (" + employee.EmployeeId + ", " + o + ");";
                Console.WriteLine(sql);
                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        //ListBusinessVolume takes the input start and end date, and makes a query of the sum of order totals in that period
        public static Double ListBusinessVolume(String start, String end)
        {
            //Connect to the database and make the query
            string sql = "SELECT FORMAT((SELECT SUM(total) AS `Business Volume` FROM memonOrders WHERE datePlaced BETWEEN '"
                 + start + "' AND '" + end + "'  ), 2) AS `Business Volume`;";
            double bv = 0;

            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
               
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bv = reader.GetDouble("Business Volume");
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            return bv;
        }

        //Edit Employee updates an employee with the values 
        public void EditEmployee(string currentName)
        {
            //Connect to the databse and Update the employee with name=currentName to the new values
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE memonEmployee SET employeeID = " + this.EmployeeId +
                    ", employeeName = '" + this.EmployeeName + "', pin = " + this.Pin +
                    " , manager = " + this.Manager + " WHERE employeeName = '" + currentName + "';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        //AddItem adds an item to the menu in the database
        public static void AddItem(String name, decimal cost, decimal quantity)
        {
            //Conect to the database and run the INSERT command
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO memonMaterials VALUES ('" + name + "', " + cost + ", " + quantity + ");";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        //Remove an item from the menu
        public static void RemoveItem(String name)
        {
            //Connect to the database and remove the item with the chose name from the database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "DELETE FROM memonMaterials WHERE item = '" + name + "';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
