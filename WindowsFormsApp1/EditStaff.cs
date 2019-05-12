using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    //EditStaff is the form that lets the user edit a staff's profile
    public partial class EditStaff : Form
    {
        //Emp is the employee that is currently logged in, which will be passed back to the MainMenu
        //at the end so the program can remember who is logged in
        Employee emp;
        //toEdit is the employee that needs to be edited
        Employee toEdit;
        //currentName holds the employees current name so that if it needs to be changed, we can still query by their old name
        private String currentName;
        public EditStaff(Employee employee)
        {
            this.Width = 1652;
            this.Height = 719;
            InitializeComponent();
            emp = employee;
            //Populate the dropdown menu with the list of employees
            PopulateStaff();
            toEdit = new Employee();
        }

        //PopulateStaff populates the dropdown menu with the list of employees
        public void PopulateStaff()
        {
            //Connect to the database and run the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);

            var dataSourceEmployees = new List<String>();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT employeeName AS `Employee Name` from memonEmployee;";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader;

                reader = cmd.ExecuteReader();
                //for each query, add the employee name to the list of employees
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
            comboBox1.DataSource = dataSourceEmployees;
        }

        //When Return to Menu is clicked, return to the main menu
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }

        //When next is clicked, save the user's selection as the employee toEdit's name, change to the next panel
        private void button2_Click_1(object sender, EventArgs e)
        {
            currentName = (String)comboBox1.SelectedValue;
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.Location = new Point(53, 63);
            //Populate the textboxes with the employee's information
            //Connect to the databse and make the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "SELECT employeeID, employeeName, pin, manager FROM memonEmployee WHERE employeeName= '" + currentName + "';";
                Console.WriteLine(sql);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    textBox1.Text = myReader["employeeID"].ToString();
                    textBox2.Text = myReader["employeeName"].ToString();
                    textBox3.Text = myReader["pin"].ToString();
                    if (Int16.Parse(myReader["manager"].ToString()) == 1)
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            conn.Close();
        }

        //When Back is clicked, return to the previous panel
        private void button4_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        //When Confirm is clicked, save the information the user entered, and call the EditEmployee function
        private void button3_Click_1(object sender, EventArgs e)
        {
            toEdit.EmployeeId = Int32.Parse(textBox1.Text);
            toEdit.EmployeeName = textBox2.Text;
            toEdit.Manager = checkBox1.Checked == true ? 1 : 0;
            toEdit.Pin = Int32.Parse(textBox3.Text);
            toEdit.EditEmployee(currentName);

            MessageBox.Show("Employee Updated");

            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }
    }
}
