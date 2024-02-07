using System;

namespace LoginPage.Entities
{
    internal class Package
    {
        private string pack_Id;
        private string pack_Name;
        private double pack_PriceAdults;
        private double pack_PriceKids;
        private int pack_Duration;
        private DateTime pack_Created;
        private double pack_SingleRommCharge;
        private double pack_DoubleRommCharge;
        private double pack_FamilyRommCharge;
        private string pack_image;
        private string pack_FileName;
        private string pack_BusId_1;
        private string pack_Bus_Id_2;

        public Package()
        {
        }

        public Package(
            string pack_Id, 
            string pack_Name, 
            double pack_PriceAdults, 
            double pack_PriceKids, 
            int pack_Duration, 
            DateTime pack_Created, 
            double pack_SingleRoomCharge, 
            double pack_DoubleRoomCharge, 
            double pack_FamilyRoomCharge, 
            string pack_image, 
            string pack_FileName,
            string pack_BusId_1,
            string pack_BusId_2
            )
        {
            this.Pack_Id = pack_Id;
            this.Pack_Name = pack_Name;
            this.Pack_PriceAdults = pack_PriceAdults;
            this.Pack_PriceKids = pack_PriceKids;
            this.Pack_Duration = pack_Duration;
            this.Pack_Created = pack_Created;
            this.Pack_SingleRoomCharge = pack_SingleRoomCharge;
            this.Pack_DoubleRoomCharge = pack_DoubleRoomCharge;
            this.Pack_FamilyRoomCharge = pack_FamilyRoomCharge;
            this.Pack_image = pack_image;
            this.Pack_FileName = pack_FileName;
            this.Pack_BusId_1 = pack_BusId_1;
            this.Pack_BusId_2 = pack_BusId_2;
        }

        public string Pack_Id { get => pack_Id; set => pack_Id = value; }
        public string Pack_Name { get => pack_Name; set => pack_Name = value; }
        public double Pack_PriceAdults { get => pack_PriceAdults; set => pack_PriceAdults = value; }
        public double Pack_PriceKids { get => pack_PriceKids; set => pack_PriceKids = value; }
        public int Pack_Duration { get => pack_Duration; set => pack_Duration = value; }
        public DateTime Pack_Created { get => pack_Created; set => pack_Created = value; }
        public double Pack_SingleRoomCharge { get => pack_SingleRommCharge; set => pack_SingleRommCharge = value; }
        public double Pack_DoubleRoomCharge { get => pack_DoubleRommCharge; set => pack_DoubleRommCharge = value; }
        public double Pack_FamilyRoomCharge { get => pack_FamilyRommCharge; set => pack_FamilyRommCharge = value; }
        public string Pack_image { get => pack_image; set => pack_image = value; }
        public string Pack_FileName { get => pack_FileName; set => pack_FileName = value; }
        public string Pack_BusId_1 { get => pack_BusId_1; set => pack_BusId_1 = value; }
        public string Pack_BusId_2 { get => pack_Bus_Id_2; set => pack_Bus_Id_2 = value; }
    }
}
