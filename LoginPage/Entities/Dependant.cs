using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage.Entities
{
    internal class Dependant
    {
        private string dep_Id;
        private string dep_FirstName;
        private string dep_LastName;
        private DateTime dep_DOB;
        private string dep_Gender;
        private string cid;

        public Dependant(string dep_Id, string dep_FirstName, string dep_LastName, DateTime dep_DOB, string dep_Gender, string cId)
        {
            this.Dep_Id = dep_Id;
            this.Dep_FirstName = dep_FirstName;
            this.Dep_LastName = dep_LastName;
            this.Dep_DOB = dep_DOB;
            this.Dep_Gender = dep_Gender;
            CID = cId;
        }

        public string Dep_Id { get => dep_Id; set => dep_Id = value; }
        public string Dep_FirstName { get => dep_FirstName; set => dep_FirstName = value; }
        public string Dep_LastName { get => dep_LastName; set => dep_LastName = value; }
        public DateTime Dep_DOB { get => dep_DOB; set => dep_DOB = value; }
        public string Dep_Gender { get => dep_Gender; set => dep_Gender = value; }
        public string CID { get => cid; set => cid = value; }
    }
}
