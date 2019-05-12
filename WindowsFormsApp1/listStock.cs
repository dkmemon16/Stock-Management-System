using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    public partial class ListStock : Form
    {
        Employee _em = new Employee();
        public ListStock(Employee mm1)
        {
            _em = mm1;
            InitializeComponent();
            listStock_Load();
        }

        //this class will display the stock status of every item in the database
        //using a grid view.
        public void listStockVolume()
        {
            //create the table that is to be filled
            DataTable stockTable = new DataTable();

            //create the connection string that will be used to connect to the database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            //create sql query that returns all infromation regarding the materials in the table
            string sql = "select * from memonmaterials";

            //create and open a connection to the sql database
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            //set the created command to be a data adapter so that it can then be used to
            //populate the previously created table
            MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
            myAdapter.Fill(stockTable);

            //make the filled table appear in the grid view
            dataGridView1.DataSource = stockTable;
            conn.Close();
        }
        private void listStock_Load()
        {
            listStockVolume();
        }

        //return to main menu
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(_em);
            cp.ShowDialog();
            this.Close();
        }
    }
}
