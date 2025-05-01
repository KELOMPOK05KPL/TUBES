namespace Test_API_tubes.Models
{
    public class Vehicle
    {
        //public int Id { get; set; }
        //public string Brand { get; set; }
        //public string Model { get; set; }
        //public string Type { get; set; }
        //public bool IsAvailable { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // e.g., Mobil, Motor
        public bool IsAvailable { get; set; } = true;
    }

}
