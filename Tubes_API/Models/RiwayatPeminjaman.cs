namespace Test_API_tubes.Models
{
    public class RiwayatPeminjaman
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Peminjam { get; set; } // Nama atau ID user yang meminjam
        public DateTime TanggalPinjam { get; set; }
        public DateTime? TanggalKembali { get; set; } // Nullable untuk kendaraan yang belum dikembalikan
        public string Status { get; set; } // "Dipinjam" atau "Dikembalikan"
    }

}
