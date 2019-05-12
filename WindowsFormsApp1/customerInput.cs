using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
        //the class Customer Input allows an employee to search for infromation about a
        //customer by using their name or id number
    public partial class CustomerInput : Form
    { 
        //create employee object to recieve the sent employee object
            Employee em = new Employee();
        public CustomerInput(Employee emp)
        {
            em = emp;
            InitializeComponent();
            
            //creates data sources for table values
            var dataSourceCustomerNames = new List<string>();
            var dataSourceCustomerID = new List<string>();

            //sets connection string for the database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            //select data from employee table
            string sql = "select * from memonaccounts";

            //establish connection witht the sql server
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader;
            
            //open the connection with the server
            conn.Open();
            reader = cmd.ExecuteReader();

            //for every row in the table
            while (reader.Read())
            {
                //add the employee name and employee id to the data sources
                dataSourceCustomerNames.Add(reader.GetString("name"));
                dataSourceCustomerID.Add(reader.GetString("accountID"));
            }

            //set the data for the comboboxes
            comboBox1.DataSource = dataSourceCustomerNames;
            comboBox2.DataSource = dataSourceCustomerID;

            conn.Close();
        }

        public void searchCustomer()
        {
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            string ident = "select * from memonaccounts where accountID = @value";
            string name = "select * from memonEmployee where name = @name";
            MySqlConnection conn = new MySqlConnection(connStr);

            //create the table to hold the returned data
            DataTable customerTable = new DataTable();
            conn.Open();

            //if there is a selected name, search based on that selected name
            if (comboBox1.SelectedValue != null)
            {
                //assign the string that selects the customer with the name from the combo box
                MySqlCommand cmdName = new MySqlCommand(name, conn);
                cmdName.Parameters.AddWithValue("@name", comboBox1.SelectedValue);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmdName);

                //fill the table with the data selected from the database
                myAdapter.Fill(customerTable);

                //set the data grid's data source to the filled table
                dataGridView1.DataSource = customerTable;

            }
            else
            if (comboBox2.SelectedValue != null)
            {
                //assign the string that selects the customer with the ID from the combo box
                MySqlCommand cmdId = new MySqlCommand(ident, conn);
                cmdId.Parameters.AddWithValue("@value", comboBox2.SelectedValue);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmdId);

                //fill the table with the data selected from the database
                myAdapter.Fill(customerTable);

                //set the data grid's data source to the filled table
                dataGridView1.DataSource = customerTable;
            }

            //make the filled data grid visible to the user
            dataGridView1.Visible = true;


            conn.Close();
        }
        //when this button is pressed the server will be queried and will return
        // the rows have either the name selected or, if the name value is left blank,
        //the rows that have the in number selected
        private void button1_Click(object sender, EventArgs e)
        {
            searchCustomer();

        }

        //this button returns the user to the main menu
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu ca = new Menu(em);
            ca.ShowDialog();
            this.Close();
        }
    }
}
