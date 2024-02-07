using LoginPage.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace LoginPage.Db
{
    internal class ManageCustomer
    {
        private Customer customer;

        public ManageCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public bool save()
        {
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "INSERT INTO tbl_customer " +
                                    "( cus_Id, " +
                                      "cus_FirstName, " +
                                      "cus_LastName, " +
                                      "cus_Email, " +
                                      "cus_Phone, " +
                                      "cus_DOB, " +
                                      "cus_Gender, " +
                                      "cus_Country, " +
                                      "UID ) " +
                                 "VALUES " +
                                    "( @cus_Id, " +
                                      "@cus_FirstName, " +
                                      "@cus_LastName, " +
                                      "@cus_Email, " +
                                      "@cus_Phone, " +
                                      "@cus_DOB, " +
                                      "@cus_Gender, " +
                                      "@cus_Country, " +
                                      "@cus_handledBy " +
                                    ")";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@cus_Id", SqlDbType.VarChar, 8).Value = customer.Cus_Id;
                        cmd.Parameters.Add("@cus_FirstName", SqlDbType.VarChar, 20).Value = customer.Cus_FirstName;
                        cmd.Parameters.Add("@cus_LastName", SqlDbType.VarChar, 20).Value = customer.Cus_LastName;
                        cmd.Parameters.Add("@cus_Email", SqlDbType.VarChar, 50).Value = customer.Cus_Email;
                        cmd.Parameters.Add("@cus_Phone", SqlDbType.VarChar, 12).Value = customer.Cus_Phone;
                        cmd.Parameters.Add("@cus_DOB", SqlDbType.Date).Value = customer.Cus_DOB;
                        cmd.Parameters.Add("@cus_Gender", SqlDbType.VarChar, 1).Value = customer.Cus_Gender;
                        cmd.Parameters.Add("@cus_Country", SqlDbType.VarChar, 30).Value = customer.Cus_Country;
                        cmd.Parameters.Add("@cus_handledBy", SqlDbType.VarChar, 8).Value = customer.Cus_HandledBy;
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        return i > 0;
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public bool update()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "UPDATE tbl_customer SET " +
                                "cus_FirstName = @cus_FirstName, " +
                                "cus_LastName = @cus_LastName, " +
                                "cus_Email = @cus_Email, " +
                                "cus_Phone = @cus_Phone, " +
                                "cus_DOB = @cus_DOB, " +
                                "cus_Gender = @cus_Gender, " +
                                "cus_Country = @cus_Country, " +
                                "UID = @cus_handledBy " +
                             "WHERE " +
                                "cus_Id = @cus_Id";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@cus_FirstName", SqlDbType.VarChar, 20).Value = customer.Cus_FirstName;
                    cmd.Parameters.Add("@cus_LastName", SqlDbType.VarChar, 20).Value = customer.Cus_LastName;
                    cmd.Parameters.Add("@cus_Email", SqlDbType.VarChar, 50).Value = customer.Cus_Email;
                    cmd.Parameters.Add("@cus_Phone", SqlDbType.VarChar, 12).Value = customer.Cus_Phone;
                    cmd.Parameters.Add("@cus_DOB", SqlDbType.Date).Value = customer.Cus_DOB;
                    cmd.Parameters.Add("@cus_Gender", SqlDbType.VarChar, 1).Value = customer.Cus_Gender;
                    cmd.Parameters.Add("@cus_Country", SqlDbType.VarChar, 30).Value = customer.Cus_Country;
                    cmd.Parameters.Add("@cus_handledBy", SqlDbType.VarChar, 8).Value = customer.Cus_HandledBy;
                    cmd.Parameters.Add("@cus_Id", SqlDbType.VarChar, 8).Value = customer.Cus_Id;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }

        public static DataTable getCustomers(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "cus_Id, " +
                                    "cus_FirstName, " +
                                    "cus_LastName, " +
                                    "cus_Email, " +
                                    "cus_Phone, " +
                                    "cus_DOB, " +
                                    "cus_Gender, " +
                                    "cus_Country, " +
                                    "UID " +
                                "FROM " +
                                    "tbl_customer " +
                                "ORDER BY " +
                                    "cus_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "cus_Id, " +
                                    "cus_FirstName, " +
                                    "cus_LastName, " +
                                    "cus_Email, " +
                                    "cus_Phone, " +
                                    "cus_DOB, " +
                                    "cus_Gender, " +
                                    "cus_Country, " +
                                    "UID " +
                                "FROM " +
                                    "tbl_customer " +
                                "WHERE " +
                                    "cus_Id = @cus_Id " +
                                        "OR " +
                                    "cus_Gender = @cus_Gender " +
                                        "OR " +
                                    "cus_FirstName = @cus_FirstName " +
                                        "OR " +
                                    "cus_LastName = @cus_LastName " +
                                        "OR " +
                                    "cus_Email = @cus_Email " +
                                        "OR " +
                                    "cus_Phone = @cus_Phone " +
                                        "OR " +
                                    "cus_Country = @cus_Country " +
                                        "OR " +
                                    "UID = @UID " +
                                "ORDER BY " +
                                    "cus_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@cus_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@cus_Gender", SqlDbType.VarChar, 1).Value = searchText;
                        cmd.Parameters.Add("@cus_FirstName", SqlDbType.VarChar, 20).Value = searchText;
                        cmd.Parameters.Add("@cus_LastName", SqlDbType.VarChar, 20).Value = searchText;
                        cmd.Parameters.Add("@cus_Email", SqlDbType.VarChar, 50).Value = searchText;
                        cmd.Parameters.Add("@cus_Phone", SqlDbType.VarChar, 12).Value = searchText;
                        cmd.Parameters.Add("@cus_Country", SqlDbType.VarChar, 30).Value = searchText;
                        cmd.Parameters.Add("@UID", SqlDbType.VarChar, 8).Value = searchText;

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }
                    }
                }
                con.Close();
            }
            return dt;
        }

        public static string getLastCustomerId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(cus_Id) as \"cus_Id\" " +
                            "FROM " +
                                "tbl_customer";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "cus15000" : last_id;
        }
    }
}
