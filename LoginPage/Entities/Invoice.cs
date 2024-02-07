using System;

namespace LoginPage.Entities
{
    internal class Invoice
    {
        private string inv_Id;
        private DateTime inv_Date;
        private double inv_Total;
        private string cid;

        public Invoice() { }

        public Invoice(
            string inv_Id, 
            DateTime inv_Date, 
            double inv_Total, 
            string cid)
        {
            this.inv_Id = inv_Id;
            this.inv_Date = inv_Date;
            this.inv_Total = inv_Total;
            this.cid = cid;
        }

        public string Inv_Id
        {
            get => inv_Id; set => inv_Id = value;
        }
        public DateTime Inv_Date
        {
            get => inv_Date; set => inv_Date = value;
        }
        public double Inv_Total
        {
            get => inv_Total; set => inv_Total = value;
        }
        public string Cid
        {
            get => cid; set => cid = value;
        }
    }
}
