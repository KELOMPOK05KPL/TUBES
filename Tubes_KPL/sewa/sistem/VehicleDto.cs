namespace model
{
    public class VehicleDto
    {
        
            public int Id { get; set; }
            public string Type { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public int State { get; set; } // Tambahkan "State" karena ada di API
        

    }
}
