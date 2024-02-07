using System;

namespace LoginPage.Entities
{
    internal class AdminUser : User
    {
        public AdminUser(string uname, string pass) : base(uname, pass)
        {
        }

        public AdminUser(
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
        {

        }
    }
}
