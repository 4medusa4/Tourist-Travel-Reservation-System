using LoginPage.Entities;
using System.Data;
using System.Data.SqlClient;

namespace LoginPage.Db
{
    internal class ManageReservation
    {
        private Reservation reservation;

        public ManageReservation(Reservation reservation)
        {
            this.reservation = reservation;
        }

        public bool save()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "INSERT INTO tbl_reservation " +
                                "( res_Id, " +
                                  "res_Date, " +
                                  "res_NoOfTravellers, " +
                                  "res_StartDate, " +
                                  "res_EndDate, " +
                                  "res_SingleRoomCount, " +
                                  "res_DoubleRoomCount, " +
                                  "res_FamilyRoomCount, " +
                                  "PID, " +
                                  "IID, " +
                                  "cid ) " +
                             "VALUES " +
                                "( @res_Id, " +
                                  "@res_Date, " +
                                  "@res_NoOfTravellers, " +
                                  "@res_StartDate, " +
                                  "@res_EndDate, " +
                                  "@res_SingleRoomCount, " +
                                  "@res_DoubleRoomCount, " +
                                  "@res_FamilyRoomCount, " +
                                  "@PID, " +
                                  "@IID, " +
                                  "@cid )";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@res_Id", SqlDbType.VarChar, 8).Value = reservation.Res_Id;
                    cmd.Parameters.Add("@res_Date", SqlDbType.Date).Value = reservation.Res_Date;
                    cmd.Parameters.Add("@res_NoOfTravellers", SqlDbType.Int).Value = reservation.Res_NoOfTravellers;
                    cmd.Parameters.Add("@res_StartDate", SqlDbType.Date).Value = reservation.Res_Date;
                    cmd.Parameters.Add("@res_EndDate", SqlDbType.Date).Value = reservation.Res_Date;
                    cmd.Parameters.Add("@res_SingleRoomCount", SqlDbType.Int).Value = reservation.Res_SingleRoomCount;
                    cmd.Parameters.Add("@res_DoubleRoomCount", SqlDbType.Int).Value = reservation.Res_DoubleRoomCount;
                    cmd.Parameters.Add("@res_FamilyRoomCount", SqlDbType.Int).Value = reservation.Res_FamilyRoomCount;
                    cmd.Parameters.Add("@PID", SqlDbType.VarChar, 8).Value = reservation.Pid;
                    cmd.Parameters.Add("@IID", SqlDbType.VarChar, 8).Value = reservation.Iid;
                    cmd.Parameters.Add("@cid", SqlDbType.VarChar, 8).Value = reservation.Cid;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }

        public Reservation getReservation()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                    "res_Id, " +
                                    "res_Date, " +
                                    "res_NoOfTravellers, " +
                                    "res_StartDate, " +
                                    "res_EndDate, " +
                                    "res_SingleRoomCount, " +
                                    "res_DoubleRoomCount, " +
                                    "res_FamilyRoomCount, " +
                                    "PID, " +
                                    "IID, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_reservation " +
                                "WHERE " +
                                    "res_Id = @res_Id ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Parameters.Add("@res_Id", SqlDbType.VarChar, 8).Value = reservation.Res_Id;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            reservation.Res_Date = reader.GetDateTime(1);
                            reservation.Res_NoOfTravellers = reader.GetInt32(2);
                            reservation.Res_StartDate = reader.GetDateTime(3);
                            reservation.Res_EndtDate = reader.GetDateTime(4);
                            reservation.Res_SingleRoomCount = reader.GetInt32(5);
                            reservation.Res_DoubleRoomCount = reader.GetInt32(6);
                            reservation.Res_FamilyRoomCount = reader.GetInt32(7);
                            reservation.Pid = reader.GetString(8);
                            reservation.Iid = reader.GetString(9);
                            reservation.Cid = reader.GetString(10);
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return reservation;
        }

        public DataTable getReservations(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "res_Id, " +
                                    "res_Date, " +
                                    "res_NoOfTravellers, " +
                                    "res_StartDate, " +
                                    "res_EndDate, " +
                                    "res_SingleRoomCount, " +
                                    "res_DoubleRoomCount, " +
                                    "res_FamilyRoomCount, " +
                                    "PID, " +
                                    "IID, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_reservation" +
                                "ORDER BY" +
                                    "res_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                } else
                {
                    string sql = "SELECT " +
                                    "res_Id, " +
                                    "res_Date, " +
                                    "res_NoOfTravellers, " +
                                    "res_StartDate, " +
                                    "res_EndDate, " +
                                    "res_SingleRoomCount, " +
                                    "res_DoubleRoomCount, " +
                                    "res_FamilyRoomCount, " +
                                    "PID, " +
                                    "IID, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_reservation " +
                                "WHERE " +
                                    "res_Id = @res_Id " +
                                        "OR " +
                                    "res_NoOfTravellers = @res_NoOfTravellers " +
                                        "OR " +
                                    "res_Date = @res_Date " +
                                "ORDER BY " +
                                    "res_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@res_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@res_Date", SqlDbType.Date).Value = searchText;
                        cmd.Parameters.Add("@res_NoOfTravellers", SqlDbType.Int).Value = searchText;
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

        public static DataTable getReservation(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "res_Id, " +
                                    "res_Date, " +
                                    "res_NoOfTravellers, " +
                                    "res_StartDate, " +
                                    "res_EndDate, " +
                                    "res_SingleRoomCount, " +
                                    "res_DoubleRoomCount, " +
                                    "res_FamilyRoomCount, " +
                                    "PID, " +
                                    "IID," +
                                    "CID " +
                                "FROM " +
                                    "tbl_reservation " +
                                "ORDER BY " +
                                    "res_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "res_Id, " +
                                    "res_Date, " +
                                    "res_NoOfTravellers, " +
                                    "res_StartDate, " +
                                    "res_EndDate, " +
                                    "res_SingleRoomCount, " +
                                    "res_DoubleRoomCount, " +
                                    "res_FamilyRoomCount, " +
                                    "PID, " +
                                    "IID, " +
                                    "CID " +
                                "FROM " +
                                    "tbl_reservation " +
                                "WHERE " +
                                    "res_Id = @res_Id " +
                                        "OR " +
                                    "PID = @PID " +
                                        "OR " +
                                    "IID = @IID " +
                                        "OR " +
                                    "CID = @CID " +
                                "ORDER BY " +
                                    "res_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@res_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@PID", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@IID", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@CID", SqlDbType.VarChar, 8).Value = searchText;

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

        public static string getLastReservationId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(res_Id) as \"res_Id\" " +
                            "FROM " +
                                "tbl_reservation";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "res15000" : last_id;
        }
    }
}
