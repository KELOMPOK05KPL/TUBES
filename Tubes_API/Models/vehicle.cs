namespace Test_API_tubes.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }

        public Vehicle(int id, string brand, string model, string type, bool isAvailable)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Type = type;
            IsAvailable = isAvailable;
        }
    }

}
