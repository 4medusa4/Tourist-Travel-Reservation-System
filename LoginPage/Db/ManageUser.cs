using LoginPage.Entities;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LoginPage.Db
{
    internal class ManageUser
    {
        private User user;

        internal User User { get => user; set => user = value; }

        public ManageUser(User user)
        {
            this.User = user;
        }

        public bool save()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "INSERT INTO tbl_employee " +
                                "( emp_ID, " +
                                  "emp_FirstName, " +
                                  "emp_LastName, " +
                                  "emp_Role, " +
                                  "emp_Email, " +
                                  "emp_Phone, " +
                                  "emp_DOB, " +
                                  "emp_Gender ) " +
                             "VALUES " +
                                "( @emp_ID, " +
                                  "@emp_FirstName, " +
                                  "@emp_LastName, " +
                                  "@emp_Role, " +
                                  "@emp_Email, " +
                                  "@emp_Phone, " +
                                  "@emp_DOB, " +
                                  "@emp_Gender )";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@emp_ID", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                    cmd.Parameters.Add("@emp_FirstName", SqlDbType.VarChar, 20).Value = User.Emp_FirstName;
                    cmd.Parameters.Add("@emp_LastName", SqlDbType.VarChar, 20).Value = User.Emp_LastName;
                    cmd.Parameters.Add("@emp_Role", SqlDbType.VarChar, 50).Value = User.Emp_Role;
                    cmd.Parameters.Add("@emp_Email", SqlDbType.VarChar, 12).Value = User.Emp_Email;
                    cmd.Parameters.Add("@emp_Phone", SqlDbType.VarChar, 10).Value = User.Emp_Phone;
                    cmd.Parameters.Add("@emp_DOB", SqlDbType.Date).Value = User.Emp_DOB;
                    cmd.Parameters.Add("@emp_Gender", SqlDbType.VarChar, 30).Value = User.Emp_Gender;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }

        public bool create_Admin_Account()
        {
            bool flag = false;
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "INSERT INTO " +
                                "tbl_user " +
                                    "( usr_ID, " +
                                    "usr_uname, " +
                                    "usr_pass ) " +
                                "VALUES " +
                                    "(@usr_ID, " +
                                    "@usr_uname, " +
                                    "@usr_pass)";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@usr_ID", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                        cmd.Parameters.Add("@usr_uname", SqlDbType.VarChar, 20).Value = User.Uname;
                        cmd.Parameters.Add("@usr_pass", SqlDbType.VarChar, -1).Value = User.Pass;
                        con.Open();
                        flag = cmd.ExecuteNonQuery() > 0;
                    }
                    sql = "INSERT INTO tbl_admin_user " +
                                    "( ausr_ID ) " +
                                 "VALUES " +
                                    "( @ausr_ID )";
                    if (flag)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.Add("@ausr_ID", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                            flag = cmd.ExecuteNonQuery() > 0;
                        }
                    }
                    con.Close();
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return flag;
        }

        public bool create_NonAdmin_Account()
        {
            bool flag = false;
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "INSERT INTO " +
                                "tbl_user " +
                                    "( usr_ID, " +
                                    "usr_uname, " +
                                    "usr_pass ) " +
                                "VALUES " +
                                    "(@usr_ID, " +
                                    "@usr_uname, " +
                                    "@usr_pass)";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@usr_ID", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                        cmd.Parameters.Add("@usr_uname", SqlDbType.VarChar, 20).Value = User.Uname;
                        cmd.Parameters.Add("@usr_pass", SqlDbType.VarChar, -1).Value = User.Pass;
                        flag = cmd.ExecuteNonQuery() > 0;
                    }
                    sql = "INSERT INTO tbl_nonadmin_user " +
                                    "( nusr_ID ) " +
                                 "VALUES " +
                                    "( @nusr_ID )";
                    if (flag)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.Add("@nusr_ID", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                            con.Open();
                            flag = cmd.ExecuteNonQuery() > 0;
                        }
                    }
                    con.Close();
                }

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return flag;
        }

        public int login()
        {
            int i = -1;
            using (SqlConnection con = new Database().connect_to_system())
            {
                byte[] enc = null;
                StringBuilder builder = new StringBuilder();
                string sql = "SELECT nusr_Id FROM tbl_nonadmin_user nu INNER JOIN tbl_user u ON nu.nusr_Id = u.usr_Id AND (usr_uname = @usr_uname AND usr_pass = @usr_pass)";
                using (SHA256 sha256hash = SHA256.Create())
                {
                    enc = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(User.Pass));
                    for (int j = 0; j < enc.Length; j++)
                    {
                        builder.Append(enc[j].ToString("x2"));
                    }
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@usr_uname", SqlDbType.VarChar, 20).Value = User.Uname;
                        cmd.Parameters.Add("@usr_pass", SqlDbType.VarChar, -1).Value = builder.ToString();
                        con.Open();
                        object obj = cmd.ExecuteScalar();
                        string id = null;
                        if (obj != null)
                        {
                            id = obj.ToString();
                        }
                        if (id != null)
                        {
                            i = 0;
                            this.User.Emp_Id = id;
                            con.Close();
                            return i;
                        }
                    }
                    sql = "SELECT ausr_Id FROM tbl_admin_user au  INNER JOIN tbl_user u ON au.ausr_Id = u.usr_Id AND (usr_uname = @usr_uname AND usr_pass = @usr_pass)";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@usr_uname", SqlDbType.VarChar, 20).Value = User.Uname;
                        cmd.Parameters.Add("@usr_pass", SqlDbType.VarChar, -1).Value = builder.ToString();
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        object obj = cmd.ExecuteScalar();
                        string id = null;
                        if (obj != null)
                        {
                            id = obj.ToString();
                        }
                        if (id != null)
                        {
                            this.User.Emp_Id = id;
                            i = 1;
                        }
                    }
                    con.Close();
                }
                catch (System.NullReferenceException) { }
            }
            return i;
        }

        public bool checkUserEmail()
        {
            bool f = false;
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT DISTINCT " +
                                "em.emp_Id " +
                            "FROM " +
                                "tbl_user u " +
                            "INNER JOIN " +
                                "tbl_employee em " +
                            "ON " +
                                "u.usr_Id = em.emp_Id " +
                            "AND " +
                                "em.emp_Email = @usr_Email";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@usr_Email", SqlDbType.VarChar, 50).Value = User.Emp_Email;
                        con.Open();
                        object obj = cmd.ExecuteScalar().ToString();
                        string id = null;
                        if (obj != null)
                        {
                            id = obj.ToString();
                        }
                        if (id != null)
                        {
                            f = true;
                            this.User.Emp_Id = id;
                        }
                    }
                    con.Close();
                }
                catch (System.NullReferenceException) { }
            }
            return f;
        }

        public void getUser()
        {
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "SELECT " +
                                        "emp_ID, " +
                                        "emp_FirstName, " +
                                        "emp_LastName, " +
                                        "emp_Role, " +
                                        "emp_Email, " +
                                        "emp_Phone, " +
                                        "emp_DOB, " +
                                        "emp_Gender " +
                                    "FROM " +
                                        "tbl_employee " +
                                    "WHERE " +
                                        "emp_Id = @emp_Id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.Parameters.Add("@emp_Id", SqlDbType.VarChar, 8).Value = User.Emp_Id;
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                        {
                            while (reader.Read())
                            {
                                User.Emp_FirstName = reader.GetString(1);
                                User.Emp_LastName = reader.GetString(2);
                                User.Emp_Role = reader.GetString(3);
                                User.Emp_Email = reader.GetString(4);
                                User.Emp_Phone = reader.GetString(5);
                                User.Emp_DOB = reader.GetDateTime(6);
                                User.Emp_Gender = reader.GetString(7);
                            }
                            reader.Close();
                        }
                    }
                    con.Close();
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public DataTable getUsers(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                con.Open();
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "emp_ID, " +
                                    "emp_FirstName, " +
                                    "emp_LastName, " +
                                    "emp_Role, " +
                                    "emp_Email, " +
                                    "emp_Phone " +
                                    "emp_DOB " +
                                    "emp_Gender " +
                                "FROM " +
                                    "tbl_employee" +
                                "ORDER BY" +
                                    "emp_ID ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "emp_ID, " +
                                    "emp_FirstName, " +
                                    "emp_LastName, " +
                                    "emp_Role, " +
                                    "emp_Email, " +
                                    "emp_Phone " +
                                    "emp_DOB " +
                                    "emp_Gender " +
                                "FROM " +
                                    "tbl_employee" +
                                "WHERE " +
                                    "emp_ID = @emp_ID " +
                                        "OR " +
                                    "emp_FirstName = @emp_FirstName " +
                                        "OR " +
                                    "emp_LastName = @emp_LastName " +
                                        "OR " +
                                    "emp_Role = @emp_Role " +
                                        "OR " +
                                    "emp_Email = @emp_Email " +
                                        "OR " +
                                    "emp_Phone = @emp_Phone " +
                                        "OR " +
                                    "emp_DOB = @emp_DOB " +
                                        "OR " +
                                    "emp_Gender = @emp_Gender " +
                                "ORDER BY " +
                                    "emp_ID ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@emp_ID", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@emp_FirstName", SqlDbType.VarChar, 20).Value = searchText;
                        cmd.Parameters.Add("@emp_LastName", SqlDbType.VarChar, 20).Value = searchText;
                        cmd.Parameters.Add("@emp_Role", SqlDbType.VarChar, 50).Value = searchText;
                        cmd.Parameters.Add("@emp_Email", SqlDbType.VarChar, 12).Value = searchText;
                        cmd.Parameters.Add("@emp_Phone", SqlDbType.VarChar, 10).Value = searchText;
                        cmd.Parameters.Add("@emp_DOB", SqlDbType.Date).Value = searchText;
                        cmd.Parameters.Add("@emp_Gender", SqlDbType.VarChar, 30).Value = searchText;

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

        public static void getUsers(ComboBox comboBox)
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                            "emp_Id, " +
                            "CONCAT(emp_FirstName, ' ', emp_LastName) " +
                                "AS " +
                                    "\"emp_Name\" " +
                            "FROM " +
                                "tbl_employee " +
                            "WHERE " +
                                "emp_Id " +
                            "NOT IN " +
                                "(SELECT " +
                                    "usr_id " +
                                "FROM " +
                                    "tbl_user)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        comboBox.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
                        }
                    }
                }
                con.Close();
            }
        }

        public bool resetPassword()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "UPDATE tbl_user SET " +
                                "usr_uname = @usr_uname " +
                                "usr_pass = @usr_pass " +
                             "WHERE " +
                                "usr_Id = @usr_Id";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@usr_uname", SqlDbType.VarChar, 20).Value = user.Uname;
                    cmd.Parameters.Add("@usr_pass", SqlDbType.VarChar, -1).Value = user.Pass;
                    cmd.Parameters.Add("@usr_Id", SqlDbType.VarChar, 8).Value = user.Emp_Id;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }
    }
}
