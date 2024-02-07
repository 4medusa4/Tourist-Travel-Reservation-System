using LoginPage.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LoginPage.Db
{
    internal class ManageDependant
    {
        private Dependant dependant;

        public ManageDependant(Dependant dependant)
        {
            this.dependant = dependant;
        }

        public bool save()
        {
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "INSERT INTO tbl_dependant " +
                                    "( dep_Id, " +
                                    "dep_FirstName, " +
                                    "dep_LastName, " +
                                    "dep_DOB, " +
                                    "cus_Gender, " +
                                    "CID ) " +
                                 "VALUES " +
                                    "( @dep_Id, " +
                                      "@dep_FirstName, " +
                                      "@dep_LastName, " +
                                      "@dep_DOB, " +
                                      "@dep_Gender, " +
                                      "@cid)";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@dep_Id", SqlDbType.VarChar, 8).Value = dependant.Dep_Id;
                        cmd.Parameters.Add("@dep_FirstName", SqlDbType.VarChar, 20).Value = dependant.Dep_FirstName;
                        cmd.Parameters.Add("@dep_LastName", SqlDbType.VarChar, 20).Value = dependant.Dep_LastName;
                        cmd.Parameters.Add("@dep_DOB", SqlDbType.Date).Value = dependant.Dep_DOB;
                        cmd.Parameters.Add("@dep_Gender", SqlDbType.VarChar, 1).Value = dependant.Dep_Gender;
                        cmd.Parameters.Add("@cid", SqlDbType.VarChar, 8).Value = dependant.CID;
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
                string sql = "UPDATE tbl_dependant SET " +
                                "dep_FirstName = @dep_FirstName, " +
                                "dep_LastName = @dep_LastName, " +
                                "dep_DOB = @dep_DOB, " +
                                "dep_Gender = @dep_Gender, " +
                             "WHERE " +
                                "dep_Id = @dep_Id AND CID = @cId";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dep_FirstName", SqlDbType.VarChar, 20).Value = dependant.Dep_FirstName;
                    cmd.Parameters.Add("@dep_LastName", SqlDbType.VarChar, 20).Value = dependant.Dep_LastName;
                    cmd.Parameters.Add("@dep_DOB", SqlDbType.Date).Value = dependant.Dep_DOB;
                    cmd.Parameters.Add("@dep_Gender", SqlDbType.VarChar, 1).Value = dependant.Dep_Gender;
                    cmd.Parameters.Add("@dep_Id", SqlDbType.VarChar, 8).Value = dependant.Dep_Gender;
                    cmd.Parameters.Add("@cId", SqlDbType.VarChar, 8).Value = dependant.CID;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }


        public static string getLastdepenadantId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(dep_Id) as \"dep_Id\" " +
                            "FROM " +
                                "tbl_dependant";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "dep15000" : last_id;
        }
    }
}
