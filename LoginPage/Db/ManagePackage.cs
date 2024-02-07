using LoginPage.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LoginPage.Db
{
    internal class ManagePackage
    {
        private Package package;
        public ManagePackage(Package package)
        {
            this.Package = package;
        }
        public bool save()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "INSERT INTO tbl_package " +
                                "( pack_Id, " +
                                  "pack_Name, " +
                                  "pack_PriceAdults, " +
                                  "pack_PriceKids, " +
                                  "pack_Duration, " +
                                  "pack_Created, " +
                                  "pack_SingleRoomCharge, " +
                                  "pack_DoubleRoomCharge, " +
                                  "pack_FamilyRoomCharge, " +
                                  "pack_image, " +
                                  "pack_FileName ) " +
                             "VALUES " +
                                "( @pack_Id, " +
                                  "@pack_Name, " +
                                  "@pack_PriceAdults, " +
                                  "@pack_PriceKids, " +
                                  "@pack_Duration, " +
                                  "@pack_Created, " +
                                  "@pack_SingleRoomCharge, " +
                                  "@pack_DoubleRoomCharge, " +
                                  "@pack_FamilyRoomCharge, " +
                                  "@pack_image, " +
                                  "@pack_FileName )";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@pack_Id", SqlDbType.VarChar, 8).Value = package.Pack_Id;
                    cmd.Parameters.Add("@pack_Name", SqlDbType.VarChar, 30).Value = package.Pack_Name;

                    SqlParameter price_adults = new SqlParameter("@pack_PriceAdults", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(price_adults).Value = package.Pack_PriceAdults;

                    SqlParameter price_kids = new SqlParameter("@pack_PriceKids", SqlDbType.Decimal);
                    price_kids.Precision = 8;
                    price_kids.Scale = 2;
                    cmd.Parameters.Add(price_kids).Value = package.Pack_PriceAdults;

                    SqlParameter pack_SingleRoomeCharge = new SqlParameter("@pack_SingleRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_SingleRoomeCharge).Value = package.Pack_PriceAdults;

                    SqlParameter pack_DoubleRoomeCharge = new SqlParameter("@pack_DoubleRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_DoubleRoomeCharge).Value = package.Pack_PriceAdults;

                    SqlParameter pack_FamilyRoomeCharge = new SqlParameter("@pack_FamilyRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_FamilyRoomeCharge).Value = package.Pack_PriceAdults;

                    cmd.Parameters.Add("@pack_Duration", SqlDbType.Int).Value = package.Pack_Duration;
                    cmd.Parameters.Add("@pack_Created", SqlDbType.Date).Value = package.Pack_Created;
                    cmd.Parameters.Add("@pack_image", SqlDbType.VarChar, 30).Value = package.Pack_image;
                    cmd.Parameters.Add("@pack_FileName", SqlDbType.NVarChar, -1).Value = package.Pack_FileName;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0 && saveBusData();
                }
            }
        }

        public bool update()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "UPDATE tbl_package " +
                                "SET " +
                                    "pack_Name = @pack_Name, " +
                                    "pack_PriceAdults = @pack_PriceAdults, " +
                                    "pack_PriceKids = @pack_PriceKids, " +
                                    "pack_Duration = @pack_Duration, " +
                                    "pack_Created = @pack_Created, " +
                                    "pack_SingleRoomCharge = @pack_SingleRoomCharge, " +
                                    "pack_DoubleRoomCharge = @pack_DoubleRoomCharge, " +
                                    "pack_FamilyRoomCharge = @pack_FamilyRoomCharge, " +
                                    "pack_image = @pack_image, " +
                                    "pack_FileName = @pack_FileName  " +
                                "WHERE " +
                                    "pack_Id = @pack_Id";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@pack_Name", SqlDbType.VarChar, 30).Value = package.Pack_Name;

                    SqlParameter price_adults = new SqlParameter("@pack_PriceAdults", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(price_adults).Value = package.Pack_PriceAdults;

                    SqlParameter price_kids = new SqlParameter("@pack_PriceKids", SqlDbType.Decimal);
                    price_kids.Precision = 8;
                    price_kids.Scale = 2;
                    cmd.Parameters.Add(price_kids).Value = package.Pack_PriceKids;

                    SqlParameter pack_SingleRoomeCharge = new SqlParameter("@pack_SingleRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_SingleRoomeCharge).Value = package.Pack_PriceAdults;

                    SqlParameter pack_DoubleRoomeCharge = new SqlParameter("@pack_DoubleRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_DoubleRoomeCharge).Value = package.Pack_PriceAdults;

                    SqlParameter pack_FamilyRoomeCharge = new SqlParameter("@pack_FamilyRoomCharge", SqlDbType.Decimal);
                    price_adults.Precision = 8;
                    price_adults.Scale = 2;
                    cmd.Parameters.Add(pack_FamilyRoomeCharge).Value = package.Pack_PriceAdults;

                    cmd.Parameters.Add("@pack_Duration", SqlDbType.Int).Value = package.Pack_Duration;
                    cmd.Parameters.Add("@pack_Created", SqlDbType.Date).Value = package.Pack_Created;

                    cmd.Parameters.Add("@pack_image", SqlDbType.VarChar, 30).Value = package.Pack_image;
                    cmd.Parameters.Add("@pack_FileName", SqlDbType.NVarChar, -1).Value = package.Pack_FileName;
                    cmd.Parameters.Add("@pack_Id", SqlDbType.VarChar, 8).Value = package.Pack_Id;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0 && saveBusData();
                }
            }
        }

        public bool saveBusData()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "INSERT INTO tbl_package_bus " +
                                "(PID, " +
                                  "BID, " +
                                  "LastUpdated) " +
                             "VALUES " +
                                "(@PID, " +
                                  "@BID " +
                                  "@LastUpdated)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DateTime dt = DateTime.Now.Date;
                    cmd.Parameters.Add("@PID", SqlDbType.VarChar, 8).Value = package.Pack_Id;
                    cmd.Parameters.Add("@BID", SqlDbType.VarChar, 8).Value = package.Pack_BusId_1;
                    cmd.Parameters.Add("@LastUpdated", SqlDbType.Date, 8).Value = dt;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    int j = 0;
                    if (!string.IsNullOrEmpty(package.Pack_BusId_2))
                    {
                        cmd.Parameters.Remove("@BID");
                        cmd.Parameters.Add("@BID", SqlDbType.VarChar, 8).Value = package.Pack_BusId_2;
                        j = cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    return (i > 0) && (j > 0);
                }
            }
        }

        public Package getPackage()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                    "pack_Id, " +
                                    "pack_Name, " +
                                    "pack_PriceAdults, " +
                                    "pack_PriceKids, " +
                                    "pack_Duration, " +
                                    "pack_Created, " +
                                    "pack_SingleRoomCharge, " +
                                    "pack_DoubleRoomCharge, " +
                                    "pack_FamilyRoomCharge, " +
                                    "pack_image, " +
                                    "pack_FileName " +
                                "FROM " +
                                    "tbl_package " +
                                "WHERE " +
                                    "pack_Id = @pack_Id";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@pack_Id", SqlDbType.VarChar, 8).Value = package.Pack_Id;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            package.Pack_Name = reader.GetString(1);
                            package.Pack_PriceAdults = (double)reader.GetDecimal(2);
                            package.Pack_PriceKids = (double)reader.GetDecimal(3);
                            package.Pack_Duration = reader.GetInt32(4);
                            package.Pack_Created = reader.GetDateTime(5);
                            package.Pack_SingleRoomCharge = (double)reader.GetDecimal(6);
                            package.Pack_DoubleRoomCharge = (double)reader.GetDecimal(7);
                            package.Pack_FamilyRoomCharge = (double)reader.GetDecimal(8);
                            if (!reader.IsDBNull(9))
                                package.Pack_image = reader.GetString(9);
                            else
                                package.Pack_image = string.Empty;
                            if (!reader.IsDBNull(10))
                                package.Pack_FileName = reader.GetString(10);
                            else
                                package.Pack_FileName = string.Empty;
                        }
                        reader.Close();
                    }
                }
                con.Close();
                getBusData();
            }
            return Package;
        }

        public static DataTable getPackages(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "pack_Id, " +
                                    "pack_Name, " +
                                    "pack_PriceAdults, " +
                                    "pack_PriceKids, " +
                                    "pack_Duration, " +
                                    "pack_Created, " +
                                    "pack_SingleRoomCharge, " +
                                    "pack_DoubleRoomCharge, " +
                                    "pack_FamilyRoomCharge, " +
                                    "pack_image, " +
                                    "pack_FileName " +
                                "FROM " +
                                    "tbl_package " +
                                "ORDER BY " +
                                    "pack_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "pack_Id, " +
                                    "pack_Name, " +
                                    "pack_PriceAdults, " +
                                    "pack_PriceKids, " +
                                    "pack_Duration, " +
                                    "pack_Created, " +
                                    "pack_SingleRoomCharge, " +
                                    "pack_DoubleRoomCharge, " +
                                    "pack_FamilyRoomCharge, " +
                                    "pack_image, " +
                                    "pack_FileName " +
                                "FROM " +
                                    "tbl_package " +
                                "WHERE " +
                                    "pack_Id = @pack_Id " +
                                        "OR " +
                                    "pack_Name = @pack_Name " +
                                        "OR " +
                                    "pack_FileName = @pack_FileName " +
                                "ORDER BY " +
                                    "pack_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@pack_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@pack_Name", SqlDbType.VarChar, 30).Value = searchText;
                        cmd.Parameters.Add("@pack_FileName", SqlDbType.NVarChar, -1).Value = searchText;

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

        private void getBusData()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "BID " +
                            "FROM " +
                                "tbl_package_bus " +
                            "INNER JOIN " +
                                "tbl_package " +
                            "ON " +
                                "tbl_package_bus.PID = tbl_package.pack_Id " +
                            "GROUP BY " +
                                "LastUpdated, BID " +
                            "HAVING " +
                                "LastUpdated = MAX(tbl_package_bus.LastUpdated)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        List<string> bus = new List<string>();
                        Package p = new Package();
                        while (reader.Read())
                        {
                            bus.Add(reader.GetString(0));
                        }
                        for (int i = 0; i < bus.Count; i++)
                        {
                            if (i == 0)
                                package.Pack_BusId_1 = bus[i];
                            else if (i == 1)
                                package.Pack_BusId_2 = bus[i];
                        }
                    }
                }
                con.Close();
            }
        }

        public static void getPackages(ComboBox comboBox)
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT pack_Id, pack_Name FROM tbl_package";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        comboBox.Items.Clear();
                        Package p = new Package();
                        while (reader.Read())
                        {
                            p.Pack_Id = reader.GetString(0);
                            p.Pack_Name = reader.GetString(1);
                            comboBox.Items.Add(p.Pack_Name + "-" + p.Pack_Id);
                        }
                    }
                }
                con.Close();
            }
        }

        public static List<Package> getPackages()
        {
            List<Package> p = new List<Package>();
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                    "pack_Id, " +
                                    "pack_Name, " +
                                    "pack_PriceAdults, " +
                                    "pack_PriceKids, " +
                                    "pack_Duration, " +
                                    "pack_Created, " +
                                    "pack_SingleRoomCharge, " +
                                    "pack_DoubleRoomCharge, " +
                                    "pack_FamilyRoomCharge, " +
                                    "pack_image, " +
                                    "pack_FileName " +
                                "FROM " +
                                    "tbl_package";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        while (reader.Read())
                        {
                            Package package = new Package();
                            package.Pack_Name = reader.GetString(1);
                            package.Pack_PriceAdults = (double)reader.GetDecimal(2);
                            package.Pack_PriceKids = (double)reader.GetDecimal(3);
                            package.Pack_Duration = reader.GetInt32(4);
                            package.Pack_Created = reader.GetDateTime(5);
                            package.Pack_SingleRoomCharge = (double)reader.GetDecimal(6);
                            package.Pack_DoubleRoomCharge = (double)reader.GetDecimal(7);
                            package.Pack_FamilyRoomCharge = (double)reader.GetDecimal(8);
                            if (!reader.IsDBNull(9))
                                package.Pack_image = reader.GetString(9);
                            else
                                package.Pack_image = string.Empty;
                            if (!reader.IsDBNull(10))
                                package.Pack_FileName = reader.GetString(10);
                            else
                                package.Pack_FileName = string.Empty;
                            p.Add(package);

                        }
                    }
                }
                con.Close();
            }
            return p;
        }

        public static int getPackageCount()
        {
            int i = 0;
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT COUNT(pack_Id) FROM tbl_package";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    i = int.Parse(cmd.ExecuteScalar().ToString());
                }
                con.Close();
            }
            return i;
        }

        public static string getLastPackageId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(pack_Id) as \"pack_Id\" " +
                            "FROM " +
                                "tbl_package";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "pak15000" : last_id;
        }

        internal Package Package { get => package; set => package = value; }
    }
}
