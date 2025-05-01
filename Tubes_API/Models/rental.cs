namespace Test_API_tubes.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

}
