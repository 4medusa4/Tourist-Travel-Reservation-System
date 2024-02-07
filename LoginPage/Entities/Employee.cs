using System;

namespace LoginPage.Entities
{
    internal class Employee
    {
        private string emp_Id;
        private string emp_FirstName;
        private string emp_LastName;
        private string emp_Role;
        private string emp_Email;
        private string emp_Phone;
        private DateTime emp_DOB;
        private string emp_Gender;

        public Employee()
        {
        }

        public Employee(
            string emp_Id, 
            string emp_FirstName, 
            string emp_LastName, 
            string emp_Role, 
            string emp_Email, 
            string emp_Phone,
            DateTime emp_DOB, 
            string emp_Gender)
        {
            this.Emp_Id = emp_Id;
            this.Emp_FirstName = emp_FirstName;
            this.Emp_LastName = emp_LastName;
            this.Emp_Role = emp_Role;
            this.Emp_Email = emp_Email;
            this.Emp_Phone = emp_Phone;
            this.Emp_DOB = emp_DOB;
            this.Emp_Gender = emp_Gender;
        }

        public string Emp_Id { get => emp_Id; set => emp_Id = value; }
        public string Emp_FirstName { get => emp_FirstName; set => emp_FirstName = value; }
        public string Emp_LastName { get => emp_LastName; set => emp_LastName = value; }
        public string Emp_Role { get => emp_Role; set => emp_Role = value; }
        public string Emp_Email { get => emp_Email; set => emp_Email = value; }
        public string Emp_Phone { get => emp_Phone; set => emp_Phone = value; }
        public DateTime Emp_DOB { get => emp_DOB; set => emp_DOB = value; }
        public string Emp_Gender { get => emp_Gender; set => emp_Gender = value; }
    }
}
