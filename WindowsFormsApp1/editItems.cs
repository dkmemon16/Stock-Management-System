using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    //EditItems is the form that allows the user to edit the menu items
    public partial class EditItems : Form
    {
        //menuTable is the DataTable that will be the DataSource for DataGridView1
        DataTable menuTable;
        //Emp is the employee that is currently logged in, which will be passed back to the MainMenu
        //at the end so the program can remember who is logged in
        Employee emp;
        //The Constructor for EditItems
        public EditItems(Employee employee)
        {
            //Populate the Menu and add the column of "Remove" buttons
            emp = employee;
            InitializeComponent();
            PopulateItems();
            CreateUnboundButtonColumn();


        }

        //CreateUnboundButtonColumn adds a column of TRemove buttons to to the dataGridView
        private void CreateUnboundButtonColumn()
        {
            // Initialize the button column.
            DataGridViewButtonColumn removeButtonColumn = new DataGridViewButtonColumn();
            //removeButtonColumn is a column of Remove Buttons";
            removeButtonColumn.Text = "Remove";

            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.
            removeButtonColumn.UseColumnTextForButtonValue = true;
            removeButtonColumn.Width = 100;
            // Add the button column to the control.
            dataGridView1.Columns.Insert(2, removeButtonColumn);
        }

        //PopulateItems fills the menu with the items in the database
        private void PopulateItems()
        {
            menuTable = new DataTable();
            //Connect to the database and run the query
            string sql = "SELECT item AS Item, cost AS Price FROM memonMaterials;";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                //Fill the menuTable with the results of the query
                myAdapter.Fill(menuTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            //Bind the menuTable to the dataGridView1
            dataGridView1.DataSource = menuTable;

        }

        //When Back is clicked, return to the previous panel
        private void button4_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
        }

        //When Add Items is clicked, close this panel and open the one that has the Add menu
        private void button1_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
        }

        //When Remove Items is clicked, close this panel and open the one that has the Remove menu
        private void button5_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
            //Populate the Menu
            PopulateItems();
        }

        //When Confirm is clicked, save the name, cost and quantity the user entered, and insert that item into the database
        private void button3_Click_1(object sender, EventArgs e)
        {
            decimal cost = numericUpDown1.Value;
            String item = textBox1.Text;
            decimal quantity = numericUpDown2.Value;
            Employee.AddItem(item, cost, quantity);
            MessageBox.Show(item + " added");
        }

        //When Cancel is clicked, return to the main menu
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }

        //When Remove Item is clicked,remove the item from the menu and repopulate the menu table
        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Employee.RemoveItem(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            menuTable.Clear();
            PopulateItems();
        }

        //When Return to Menu is clicked, return to the menu
        private void Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }
    }
}
