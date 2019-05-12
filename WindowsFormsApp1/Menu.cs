using System;
using System.Windows.Forms;

namespace StockManagement
{
    public partial class Menu : Form
    {
        Employee _emp = new Employee();

        public Menu(Employee mm1)
        {
            InitializeComponent();
            _emp = mm1;
            label3.Text = _emp.EmployeeName;

            //if the employee is a manager, then allow them to view these buttons
            if(_emp.Manager == 1)
            {
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
            }
        }

        //open the change order status form
        private void button1_Click(object sender, EventArgs e)
        { 
            this.Hide();
            ChangeOrderStatus cp = new ChangeOrderStatus(_emp);
            cp.ShowDialog();
            this.Close();
        }
        
        //open the stock list form
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListStock cp = new ListStock(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the customer search form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerInput cp = new CustomerInput(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the form that lets th employee edit stock totals
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditStockvolume cp = new EditStockvolume(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the search supplier form
        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchSupplier cp = new SearchSupplier(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the business volume form
        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            BusinessVolume cp = new BusinessVolume(_emp);
            cp.ShowDialog();
            this.Close();
        }
        
        //open the assign staff form
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssignStaff cp = new AssignStaff(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the edit items form
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditItems cp = new EditItems(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //open the edit staff form
        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditStaff cp = new EditStaff(_emp);
            cp.ShowDialog();
            this.Close();
        }

        //return to the login screne
        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}
