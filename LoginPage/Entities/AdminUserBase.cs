namespace LoginPage.Entities
{
    internal class AdminUserBase
    {
        private string uname;
        private string pass;

        public string Uname { get => uname; set => uname = value; }
        public string Pass { get => pass; set => pass = value; }
    }
}