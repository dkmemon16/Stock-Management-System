using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StockManagement
{
    public partial class ChangeOrderStatus : Form
    {
        Employee _em = new Employee();
        public ChangeOrderStatus(Employee emp)
        {
            _em = emp;
            InitializeComponent();

            var dataSourceStockItems = new List<string>();
            var dataSourceStockItems2 = new List<string>();

            //input the possible states for the order status
            dataSourceStockItems2.Add("Order Recieved");
            dataSourceStockItems2.Add("Being Prepared");
            dataSourceStockItems2.Add("Out for Delivery");
            dataSourceStockItems2.Add("Delivered");


            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            string sql = "select * from memonorders where orderStatus != 'Delivered'";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataSourceStockItems.Add(reader.GetString("orderID"));
            }
            comboBox1.DataSource = dataSourceStockItems;
            comboBox2.DataSource = dataSourceStockItems2;

            conn.Close();
            
        }

        public void updateInfo()
        {
            string sql = "update memonorders set orderStatus = @status where orderID = @num";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";

            //open Mysql connection to the school's database
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();


            cmd.Parameters.AddWithValue("@status", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@num", comboBox1.SelectedValue);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateInfo();
            this.Hide();
            ChangeOrderStatus ca = new ChangeOrderStatus(_em);
            ca.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu ca = new Menu(_em);
            ca.ShowDialog();
            this.Close();
        }

        private void changeOrderStatus_Load(object sender, EventArgs e)
        {

        }
    }
}
