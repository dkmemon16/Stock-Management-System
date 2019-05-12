using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Menu = StockManagement.Menu;

namespace StockManagement
{
    //AssignStaff is the form used to Assign Staff to delivery orders
    public partial class AssignStaff : Form
    {
        //EmployeeToAssign is the Employee that will be assigned to the order
        public Employee EmployeeToAssign = new Employee();
        //Emp is the employee that is currently logged in, which will be passed back to the MainMenu
        //at the end so the program can remember who is logged in
        public Employee Emp;
        //OrderId is the OrderID of the order that the employee will be assigned to 
        public long OrderId;

        //The constructor for AssignStaff
        //The parameter is an employee that is currently logged in
        public AssignStaff(Employee employee)
        {
            //set the width and height of the form, initialize the form, and load it
            Emp = employee;
            Width = 1352;
            Height = 719;
            InitializeComponent();
            AssignStaff_Load();

        }

        //PopulateEmployees makes an SQL query of the employees who are not managers and are currently clocked in
        //It then populates the combobox with the names of those employees
        public void PopulateEmployees()
        {
            //Connect to the MySQL database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);
            //dataSourceEmployees is the String List that will be used to populate the combobox
            var dataSourceEmployees = new List<String>();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //The sql query that will be performed
                string sql = "SELECT employeeName AS `Employee Name` from memonEmployee where manager = 0 AND clockedIn = 1;";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                //For each employee found, add its name to dataSourceEmployee
                while (reader.Read())
                {
                    dataSourceEmployees.Add(reader.GetString("Employee Name"));
                }
                reader.Close();
            }

            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
            //Set the datasource of the combobox to the string list made previously
            comboBox1.DataSource = dataSourceEmployees;
        }

        //PopulateOrders makes a query of the orders that are not complete and populates combobox2 with them
        public void PopulateOrders()
        {
            //Connect to the database and make a query
            String connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);

            //dataSourceOrders is a list of ints because orders are ints
            var dataSourceOrders = new List<int>();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT orderID AS `Order ID` from memonOrders WHERE orderStatus != 'Complete';";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                //for each order found, add it to the dataSourceOrders 
                while (reader.Read())
                {
                    dataSourceOrders.Add(reader.GetInt32("Order ID"));
                }
                reader.Close();
            }

            catch (Exception err)
            {
                Console.Write(err.StackTrace);
            }
            finally
            {
                conn.Close();
            }
            comboBox2.DataSource = dataSourceOrders;
        }

        //AsssignStaff_Load is run when the form is called, it currently only calls PopulateEmployees but could do mre if needed
        private void AssignStaff_Load()
        {
            PopulateEmployees();
        }

        //When next is clicked, save the value as the EmployeeToAssign's name, populate orders is called
        private void button2_Click_1(object sender, EventArgs e)
        {
            PopulateOrders();
            EmployeeToAssign.EmployeeName = (string)comboBox1.SelectedValue;
            //hide this panel and show the next one
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.Location = new Point(12, 27);
        }

        //When Return to Menu is clicked, return to the main menu
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(Emp);
            cp.ShowDialog();
            this.Close();
        }
        //When back is clicked, go back to the first panel and hide the 2nd
        private void button3_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }
        //When confirm is clicked, the value of combobox2 will be the OrderID
        //Call the AssignStaffToOrder function, then return to menu
        private void button4_Click_1(object sender, EventArgs e)
        {
            OrderId = (int)comboBox2.SelectedValue;
            Employee.AssignStaffToOrder(EmployeeToAssign.EmployeeName, OrderId);
            MessageBox.Show("Order Assigned");
            this.Hide();
            Menu cp = new Menu(Emp);
            cp.ShowDialog();
            this.Close();
        }
    }
}
