using System;

namespace LoginPage.Entities
{
    internal class Customer
    {
        private string cus_Id;
        private string cus_FirstName;
        private string cus_LastName;
        private string cus_Email;
        private string cus_Phone;
        private DateTime cus_DOB;
        private string cus_Gender;
        private string cus_Country;
        private string cus_HandledBy;

        public Customer()
        {
        }

        public Customer(
            string cus_Id, 
            string cus_FirstName, 
            string cus_LastName, 
            string cus_Email, 
            string cus_Phone, 
            DateTime cus_DOB, 
            string cus_Gender, 
            string cus_Country, 
            string cus_HandledBy)
        {
            this.cus_Id = cus_Id;
            this.cus_FirstName = cus_FirstName;
            this.cus_LastName = cus_LastName;
            this.cus_Email = cus_Email;
            this.cus_Phone = cus_Phone;
            this.cus_DOB = cus_DOB;
            this.cus_Gender = cus_Gender;
            this.cus_Country = cus_Country;
            this.cus_HandledBy = cus_HandledBy;
        }

        public string Cus_Id
        {
            get => cus_Id; set => cus_Id = value;
        }
        public string Cus_FirstName
        {
            get => cus_FirstName; set => cus_FirstName = value;
        }
        public string Cus_LastName
        {
            get => cus_LastName; set => cus_LastName = value;
        }
        public string Cus_Email
        {
            get => cus_Email; set => cus_Email = value;
        }
        public string Cus_Phone
        {
            get => cus_Phone; set => cus_Phone = value;
        }
        public DateTime Cus_DOB
        {
            get => cus_DOB; set => cus_DOB = value;
        }
        public string Cus_Gender
        {
            get => cus_Gender; set => cus_Gender = value;
        }
        public string Cus_Country
        {
            get => cus_Country; set => cus_Country = value;
        }
        public string Cus_HandledBy
        {
            get => cus_HandledBy; set => cus_HandledBy = value;
        }
    }
}
