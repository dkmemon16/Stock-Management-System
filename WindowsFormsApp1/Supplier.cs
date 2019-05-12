using System;
using System.Windows.Forms;

namespace StockManagement
{
    //Supplier class, contains the Update Supplier function and the information of a supplier
    class Supplier
    {
        //The Supplier's information, as in the database
        public int SupplierId;
        public String Name;
        public Int64 Phone;
        public String SupplierType;

        //UpdateSupplier takes the supplier's old ID before any updates, and updates the database with the values the user entered
        //Returns true is successful, false otherwise
        public Boolean UpdateSupplier(Int32 oldId)
        {
            //Connect to the Database and run the Update command
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE memonSupplier SET supplierID = " + this.SupplierId +
                    ", name = '" + this.Name + "', phone = " + this.Phone +
                    " , supplierType = '" + this.SupplierType + "' WHERE "
                    + "supplierID= " + oldId + ";";
                Console.WriteLine(sql);
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                //if the supplier was succesfully updated, return true, else it will return false by default
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Supplier Updated");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return false;
        }
    }
}
