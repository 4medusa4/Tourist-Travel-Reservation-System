namespace LoginPage.Entities
{
    internal class Bus
    {
        private string bus_Id;
        private string bus_Number;
        private int bus_Capacity;

        public Bus()
        {
        }

        public Bus(string bus_Id, string bus_Number, int bus_Capacity)
        {
            this.bus_Id = bus_Id;
            this.bus_Number = bus_Number;
            this.bus_Capacity = bus_Capacity;
        }

        public string Bus_Id { get => bus_Id; set => bus_Id = value; }
        public string Bus_Number { get => bus_Number; set => bus_Number = value; }
        public int Bus_Capacity { get => bus_Capacity; set => bus_Capacity = value; }
    }
}
