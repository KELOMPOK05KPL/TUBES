namespace Tubes_KPL.Pengembalian
{
    public class RiwayatPeminjaman
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Peminjam { get; set; }
        public DateTime TanggalPinjam { get; set; }
        public DateTime? TanggalKembali { get; set; }
        public string Status { get; set; }
    }
}
