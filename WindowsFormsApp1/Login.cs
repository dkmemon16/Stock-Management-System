using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    public partial class Login : Form
    {
        Employee _emp = new Employee();
        public Login()
        {
            InitializeComponent();
        }
        public Login(Employee account)
        {
            _emp = account;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //create the connection string for the database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            //create the sql query that will check the supplied pin and employee ID
            string sql = "select * from memonemployee where pin = @pin and employeeID = @value";

            //establish connection to the database
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            

            cmd.Parameters.AddWithValue("@pin", textBox2.Text);
            cmd.Parameters.AddWithValue("@value", textBox1.Text);
            conn.Open();
      
            MySqlDataReader reader = cmd.ExecuteReader();

            //if the database can be opened
            if (reader.Read())
            {
                //assign the columns of the returned row to values in the employee object
                _emp.EmployeeId = reader.GetInt32("employeeID");
                _emp.EmployeeName = reader.GetString("employeeName");
                _emp.Pin = Int32.Parse(reader.GetString("pin"));
                _emp.Manager = reader.GetInt16("manager");


                conn.Close();

                //open the main menu
                this.Hide();
                Menu ca = new Menu(_emp);
                ca.ShowDialog();
                this.Close();
            }
            else
            {
                //reopen the login form so that a new login attempt can be made
                conn.Close();
                this.Hide();
                Login ca = new Login(_emp);
                ca.ShowDialog();
                this.Close();
            }

            
               
        }
    }
}
