using System;
using System.Data;
using System.Windows.Forms;

namespace StockManagement
{
    public partial class BusinessVolume : Form
    {
        //Emp is the employee that is currently logged in, which will be passed back to the MainMenu
        //at the end so the program can remember who is logged in
        private Employee emp;
        //The constructor for BusinessVolume
        public BusinessVolume(Employee employee)
        {
            Width = 1226;
            Height = 1219;
            InitializeComponent();
            emp = employee;

        }

        //When Search is clicked, query the business volume from the start date they chose and the end date, and display it.
        private void button2_Click_1(object sender, EventArgs e)
        {
            DataTable businessVolume = new DataTable();
            double bv = Employee.ListBusinessVolume(Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyy-MM-dd"));
            textBox1.Text = "Business Volume: $" + bv;
        }

        //When Return to Menu is clicked, return to the main menu
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menu cp = new Menu(emp);
            cp.ShowDialog();
            this.Close();
        }
    }
}
