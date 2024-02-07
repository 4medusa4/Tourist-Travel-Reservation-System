using System.Data;
using System.Data.SqlClient;

namespace LoginPage.Db
{
    internal class ManageInvoice
    {
        private Entities.Invoice invoice;
        private Database db;

        public ManageInvoice(Entities.Invoice invoice)
        {
            db = new Database();
            this.invoice = invoice;
        }

        public bool save()
        {
            using (SqlConnection con = db.connect_to_system())
            {
                string sql = "INSERT INTO tbl_invoice " +
                                "( inv_Id, " +
                                  "inv_Date, " +
                                  "inv_Total, " +
                                  "cid ) " +
                             "VALUES " +
                                "( @inv_Id, " +
                                  "@inv_Date, " +
                                  "@inv_Total, " +
                                  "@cid )";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@inv_Id", SqlDbType.VarChar, 8).Value = invoice.Inv_Id;
                    cmd.Parameters.Add("@inv_Date", SqlDbType.Date).Value = invoice.Inv_Date;
                    SqlParameter tot = new SqlParameter("@inv_Total", SqlDbType.Decimal);
                    tot.Precision = 8;
                    tot.Scale = 2;
                    tot.Value = invoice.Inv_Total;
                    cmd.Parameters.Add(tot);
                    cmd.Parameters.Add("@cid", SqlDbType.VarChar, 30).Value = invoice.Cid;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
        }

        public Entities.Invoice getInvoice()
        {
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                    "inv_Id, " +
                                    "inv_Date, " +
                                    "inv_Total, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_invoice " +
                                "WHERE " +
                                    "inv_Id = @inv_Id ";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Parameters.Add("@inv_Id", SqlDbType.VarChar, 8).Value = invoice.Inv_Id;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            invoice.Inv_Date = reader.GetDateTime(2);
                            invoice.Inv_Total = reader.GetDouble(3);
                            invoice.Cid = reader.GetString(4);
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return invoice;
        }

        public DataTable getInvoices(string searchText)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new Database().connect_to_system())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    string sql = "SELECT " +
                                    "inv_Id, " +
                                    "inv_Date, " +
                                    "inv_Total, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_invoice " +
                                "ORDER BY" +
                                    "inv_Id ASC";
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = new SqlCommand(sql, con);
                        sda.Fill(dt);
                    }
                }
                else
                {
                    string sql = "SELECT " +
                                    "inv_Id, " +
                                    "inv_Date, " +
                                    "inv_Total, " +
                                    "cid " +
                                "FROM " +
                                    "tbl_invoice " +
                                "WHERE " +
                                    "inv_Id = @inv_Id " +
                                        "OR " +
                                    "inv_Date = @inv_Date " +
                                        "OR " +
                                    "inv_Total = @inv_Total " +
                                "ORDER BY" +
                                    "inv_Id ASC";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@inv_Id", SqlDbType.VarChar, 8).Value = searchText;
                        cmd.Parameters.Add("@inv_Date", SqlDbType.Date).Value = searchText;
                        SqlParameter total = new SqlParameter("@inv_Total", SqlDbType.Decimal);
                        total.Precision = 8;
                        total.Scale = 2;
                        cmd.Parameters.Add(total).Value = double.Parse(searchText);

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
        public static string getLastInvoiceId()
        {
            string last_id = "";
            using (SqlConnection con = new Database().connect_to_system())
            {
                string sql = "SELECT " +
                                "MAX(inv_Id) as \"inv_Id\" " +
                            "FROM " +
                                "tbl_invoice";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    last_id = cmd.ExecuteScalar().ToString();
                }
                con.Close();
            }
            return string.IsNullOrEmpty(last_id) == true ? "inv15001" : last_id;
        }
    }
}
