using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace LoginPage.Db
{
    internal class ManageLocations
    {
        public static void getCountries(ComboBox comboBox)
        {
            using (SqlConnection con = new Database().connect_to_location())
            {
                string sql = "SELECT countries.country_name FROM dbo.countries ORDER BY country_name";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        comboBox.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
                con.Close();
            }
        }
    }
}
