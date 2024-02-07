using System;
using System.Data.SqlClient;

namespace LoginPage.Db
{
    internal class Database
    {
        private SqlConnection con;
        private string db;
        private string pass;
        private string usrname;

        public string Db { get => db; set => db = value; }
        public SqlConnection Con { get => con; set => con = value; }

        public Database()
        {
            Con = null;
            Db = "";
            pass = "";
            usrname = "";
        }

        private SqlConnection get_connction(String source)
        {
            return new SqlConnection("Data Source = .; Initial Catalog = " + Db + "; Integrated Security = True");
        }

        public SqlConnection connect_to_location()
        {
            Db = "db_location";
            Con = get_connction(Db);
            return Con;
        }

        public SqlConnection connect_to_system()
        {
            Db = "db_tourist_management_system";
            Con = get_connction(Db);
            return Con;
        }
    }
}
