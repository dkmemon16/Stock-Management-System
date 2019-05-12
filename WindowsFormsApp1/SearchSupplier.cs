using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    public partial class SearchSupplier : Form
    {
        //Emp is the employee that is currently logged in, which will be passed back to the MainMenu
        //at the end so the program can remember who is logged in
        Employee emp;
        //oldID is the supplier's old ID, saved so if the user wants to edit it, we can query by the oldID to run the update command
        Int32 oldID;
        
        //Constructor for Search Supplier, populate the dropdown menus with all the Supplier Names and Types in the database
        public SearchSupplier(Employee employee)
        {
            emp = employee;
            InitializeComponent();
            PopulateSupplierName();
            PopulateSupplierType();
        }

        //PopulateSupplierName populates the drowpdown menu with all the supplier names
        private void PopulateSupplierName()
        {
            //Connect to the database and run the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);
            //the dataSource for the combobox is a list of strings of Supplier Names
            var dataSourceSuppliers = new List<String>();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT name FROM memonSupplier;";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader;

                reader = cmd.ExecuteReader();
                //for each item found in the query, add it to the dropdown menu
                while (reader.Read())
                {
                    dataSourceSuppliers.Add(reader.GetString("name"));
                }
                reader.Close();
                comboBox1.DataSource = dataSourceSuppliers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        //PopularSupplierType populates a dropdown menu with all the supplier types in the database
        private void PopulateSupplierType()
        {
            //Connect to the database and run the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);

            var dataSourceSuppliersType = new List<String>();
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT DISTINCT supplierType FROM memonSupplier;";
                Console.WriteLine(sql);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader;

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataSourceSuppliersType.Add(reader.GetString("supplierType"));
                }
                reader.Close();
                comboBox2.DataSource = dataSourceSuppliersType;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        //CreateUnboundButtonColumn adds a column of Edit buttons to to the dataGridView
        private void CreateUnboundButtonColumn()
        {
            // Initialize the button column.
            DataGridViewButtonColumn selectButtonColumn = new DataGridViewButtonColumn();
            selectButtonColumn.Text = "Edit";

            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.
            selectButtonColumn.UseColumnTextForButtonValue = true;
            selectButtonColumn.Width = 100;
            // Add the button column to the control.
            dataGridView1.Columns.Insert(4, selectButtonColumn);
        }

        //If the Supplier Name checkbox is chosen, hide the dropdown of Supplier types and show the dropdown of Supplier Names
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox1.Visible = true;
                comboBox2.Visible = false;
            }
        }

        //When Next is clicked, depending on what the user wants to search by, run a query and display the results
        private void button2_Click_1(object sender, EventArgs e)
        {
            DataTable supplierTable = new DataTable();
            //Search by Supplier Name
            if (radioButton1.Checked)
            {
                //Connect to the database and run the query
                string sql = "SELECT supplierID AS `Supplier ID`, name AS `Supplier Name`"
                    + ", phone AS `Phone Number`, supplierType AS `Supplier Type`"
                    + "FROM memonSupplier WHERE name = '" + comboBox1.SelectedValue + "';";

                string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    Console.WriteLine(sql);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);

                    //Fill the table with the results of the query
                    myAdapter.Fill(supplierTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }
            //Search by Supplier Type
            else
            {
                //Connect to the database and run the query
                string sql = "SELECT supplierID AS `Supplier ID`, name AS `Supplier Name`"
                    + ", phone AS `Phone Number`, supplierType AS `Supplier Type`"
                    + "FROM memonSupplier WHERE supplierType = '" + comboBox2.SelectedValue + "';";

                string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    Console.WriteLine(sql);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                    //Fill the table with the results of the query
                    myAdapter.Fill(supplierTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }

            dataGridView1.DataSource = supplierTable;
            CreateUnboundButtonColumn();

            //Hide this panel and show the next one
            panel1.Visible = false;
            panel2.Visible = true;

        }

        //When Return to Menu is clicked, return to the main menu
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }

        //When the user clicks Save, update that supplier in the database
        private void button5_Click_1(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.SupplierId = Int32.Parse(textBox1.Text);
            supplier.Name = textBox2.Text;
            supplier.Phone = Int64.Parse(textBox3.Text);
            supplier.SupplierType = textBox4.Text;
            //if the query was successful, return to the main menu
            if (supplier.UpdateSupplier(oldID))
            {
                Menu menu = new Menu(emp);
                this.Hide();
                menu.ShowDialog();
                this.Close();
            }
        }

        //If Back is clicked, return to the previous panel
        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
        }

        //If Edit is clicked, populate the the textboxes with the Supplier's information
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Connect to the database and run the query
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            MySqlConnection conn = new MySqlConnection(connStr);
            try

            {
                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "SELECT * FROM memonSupplier WHERE supplierID= " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + ";";
                Console.WriteLine(sql);

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                
                //Populate the textboxes with the Supplier's current information
                if (myReader.Read())
                {
                    textBox1.Text = myReader["supplierID"].ToString();
                    oldID = Int32.Parse(myReader["supplierID"].ToString());
                    textBox2.Text = myReader["name"].ToString();
                    textBox3.Text = myReader["phone"].ToString();
                    textBox4.Text = myReader["supplierType"].ToString();
                }
                myReader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            conn.Close();
            panel3.Visible = true;
            panel2.Visible = false;
        }

        //If the Supplier Type checkbox is chosen, show the dropdown of Supplier types and hidr the dropdown of Supplier Names
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox2.Visible = true;
                comboBox1.Visible = false;
            }
        }

        //If "Back" is clicked, return to the previous panel
        private void Button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }
    }
}
