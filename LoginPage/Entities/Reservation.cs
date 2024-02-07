using System;

namespace LoginPage.Entities
{
    internal class Reservation
    {
        private string res_Id;
        private int res_NoOfTravellers;
        private DateTime res_Date;
        private DateTime res_StartDate;
        private DateTime res_EndtDate;
        private int res_SingleRoomCount;
        private int res_DoubleRoomCount;
        private int res_FamilyRoomCount;
        private string pid;
        private string iid;
        private string cid;


        public Reservation(string res_Id, 
            int res_NoOfTravellers, 
            DateTime res_Date, 
            DateTime res_StartDate, 
            DateTime res_EndtDate, 
            int res_SingleRoomCount, 
            int res_DoubleRoomCount, 
            int res_FamilyRoomCount, 
            string pid, 
            string iid, 
            string cid)
        {
            this.Res_Id = res_Id;
            this.Res_NoOfTravellers = res_NoOfTravellers;
            this.Res_Date = res_Date;
            this.Res_StartDate = res_StartDate;
            this.Res_EndtDate = res_EndtDate;
            this.Res_SingleRoomCount = res_SingleRoomCount;
            this.Res_DoubleRoomCount = res_DoubleRoomCount;
            this.Res_FamilyRoomCount = res_FamilyRoomCount;
            this.Pid = pid;
            this.Iid = iid;
            this.Cid = cid;
        }

        public string Res_Id { get => res_Id; set => res_Id = value; }
        public int Res_NoOfTravellers { get => res_NoOfTravellers; set => res_NoOfTravellers = value; }
        public DateTime Res_Date { get => res_Date; set => res_Date = value; }
        public DateTime Res_StartDate { get => res_StartDate; set => res_StartDate = value; }
        public DateTime Res_EndtDate { get => res_EndtDate; set => res_EndtDate = value; }
        public int Res_SingleRoomCount { get => res_SingleRoomCount; set => res_SingleRoomCount = value; }
        public int Res_DoubleRoomCount { get => res_DoubleRoomCount; set => res_DoubleRoomCount = value; }
        public int Res_FamilyRoomCount { get => res_FamilyRoomCount; set => res_FamilyRoomCount = value; }
        public string Pid { get => pid; set => pid = value; }
        public string Iid { get => iid; set => iid = value; }
        public string Cid { get => cid; set => cid = value; }
    }
}
