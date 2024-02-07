using System;

namespace LoginPage.Entities
{
    internal class User : Employee
    {
        private string uname;
        private string pass;

        public string Uname { get => uname; set => uname = value; }
        public string Pass { get => pass; set => pass = value; }

        public User()
        {
        }

        public User(
            string emp_Id,
            string emp_FirstName,
            string emp_LastName,
            string emp_Role,
            string emp_Email,
            string emp_Phone,
            DateTime emp_DOB,
            string emp_Gender) : base(
                    emp_Id,
                    emp_FirstName,
                    emp_LastName,
                    emp_Role,
                    emp_Email,
                    emp_Phone,
                    emp_DOB,
                    emp_Gender)
        { }

        public User(string uname, string pass)
        {
            Uname = uname;
            Pass = pass;
        }
    }
}
