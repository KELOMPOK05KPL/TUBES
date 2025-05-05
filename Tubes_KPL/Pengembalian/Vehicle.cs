namespace model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int State { get; set; } // 0 = Available, 1 = Rented
    }
}
