using LoginPage.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LoginPage.Db
{
    internal class ManageBus
    {
        private Bus bus;

        public ManageBus(Bus bus)
        {
            this.bus = bus;
        }

        public bool save()
        {
            try
            {
                using (SqlConnection con = new Database().connect_to_system())
                {
                    string sql = "INSERT INTO tbl_bus " +
                                    "( bus_Id, " +
                                    "bus_Number, " +
                                    "bus_Capacity ) " +
                                 "VALUES " +
                                    "( @bus_Id, " +
                                      "@bus_Number, " +
                                      "@bus_Capacity )";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Add("@bus_Id", SqlDbType.VarChar, 8).Value = bus.Bus_Id;
                        cmd.Parameters.Add("@bus_Number", SqlDbType.VarChar, 8).Value = bus.Bus_Number;
                        cmd.Parameters.Add("@bus_Capacity", SqlDbType.Int).Value = bus.Bus_Capacity;
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        return i > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public bool update()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "UPDATE tbl_bus SET " +
                                "bus_Capacity = @bus_Capacity " +
                             "WHERE " +
                                "bus_Id = @bus_Id";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@bus_Capacity", SqlDbType.Int).Value = bus.Bus_Capacity;
                    cmd.Parameters.Add("@bus_Id", SqlDbType.VarChar, 8).Value = bus.Bus_Id;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }


        public DataTable getBuses(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "bus_Id, " +
                                    "bus_Number, " +
                                    "bus_Capacity " +
                                "FROM " +
                                    "tbl_bus " +
                                "ORDER BY " +
                                    "bus_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "bus_Id, " +
                                    "bus_Number, " +
                                    "bus_Capacity " +
                                "FROM " +
                                    "tbl_bus " +
                                "WHERE " +
                                    "bus_Id = @bus_Id " +
                                        "OR " +
                                    "bus_Number = @bus_Number " +
                                        "OR " +
                                    "bus_Capacity = @bus_Capacity " +
                                "ORDER BY " +
                                    "bus_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@bus_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@bus_Number", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@bus_Capacity", SqlDbType.Int).Value = searchText;
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

        public Bus getBus()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                    "bus_Id, " +
                                    "bus_Number, " +
                                    "bus_Capacity " +
                                "FROM " +
                                    "tbl_bus " +
                                "WHERE " +
                                    "bus_Id = @bus_Id ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@pack_Id", SqlDbType.VarChar, 8).Value = bus.Bus_Id;
                    cmd.Parameters.Add("@bus_Number", SqlDbType.VarChar, 8).Value = bus.Bus_Number;
                    cmd.Parameters.Add("@bus_Capacity", SqlDbType.Int).Value = bus.Bus_Capacity;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            bus.Bus_Id = reader.GetString(1);
                            bus.Bus_Number = reader.GetString(2);
                            bus.Bus_Capacity = reader.GetInt32(3);
                            bus.Bus_Capacity = reader.GetInt32(3);
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return bus;
        }

        public static string getLastBusId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(bus_Id) as \"bus_Id\" " +
                            "FROM " +
                                "tbl_bus";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "bus15000" : last_id;
        }
    }
}
