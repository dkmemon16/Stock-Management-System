using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    public partial class EditStockvolume : Form
    {
        //create an employee object to hold the passed employee object
        Employee _em = new Employee();
        public EditStockvolume(Employee emp)
        {
            _em = emp;
            InitializeComponent();

            listStockVolume();
        }
        public void listStockVolume()
        {
            //create the data source for the stock item names that are returned from the database
            var dataSourceStockItems = new List<string>();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            string sql = "select * from memonmaterials";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();

            //while there are still rows in the table
            while (reader.Read())
            {
                //add the name of the items
                dataSourceStockItems.Add(reader.GetString("item"));
            }

            //set the data to appear in ht combo box
            comboBox1.DataSource = dataSourceStockItems;

            conn.Close();
        }
        //this button will update the amount of an item that is left in stock
        public void changeStockVolume()
        {
            //set up connection string for the database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            string sql;
            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            //if the user wants to add to the stock amount
            if (checkBox1.Checked)
            {
                //create the sql query will the use of addition
                sql = "update memonmaterials set quantity = (quantity + @num) where item = @item";
            }
            else //if the user wants to subtract from the stock
            {
                sql = "update memonmaterials set quantity = (quantity - @num) where item = @item";
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@num", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@item", comboBox1.SelectedValue);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            changeStockVolume();
        }

        //return to the main menu
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu ca = new Menu(_em);
            ca.ShowDialog();
            this.Close();
        }
    }
}
