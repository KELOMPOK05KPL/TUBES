namespace Test_API_tubes.Models
{
    public enum VehicleState
    {
        Available,
        Rented
    }
    public class Vehicle
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; } // e.g., Mobil, Motor
        //public bool IsAvailable { get; set; } = true;
        public VehicleState State { get; set; } = VehicleState.Available;

        public Vehicle(int id, string type, string brand, string model, VehicleState state)
        {
            Id = id;
            Type = type;
            Brand = brand;
            Model = model;
            State = state;
        }
    }

}
