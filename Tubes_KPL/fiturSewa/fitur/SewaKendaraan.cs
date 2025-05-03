using Tubes_KPL.fiturSewa.config;
using Tubes_KPL.fiturSewa.model;

using System.Text.Json;

namespace sewa_kendaraan.fiturSewa.fitur
{
    public class SewaKendaraan<T> where T : class
    {
        private List<T> DaftarKendaraan;
        private runtimeconfig Config;
        private string TipeKendaraan;

        private string historyPath = "TransactionHistory.json";


        public SewaKendaraan(List<T> kendaraan, runtimeconfig config, string tipe)
        {
            DaftarKendaraan = kendaraan;
            Config = config;
            TipeKendaraan = tipe;
        }

        public void Jalankan()
        {
            Console.WriteLine("\nDaftar Kendaraan Tersedia:");
            for (int i = 0; i < DaftarKendaraan.Count; i++)
            {
                dynamic k = DaftarKendaraan[i];
                Console.WriteLine($"{i + 1}. {k.Brand} {k.Model}");
            }

            Console.Write("Pilih nomor kendaraan: ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 1 || pilih > DaftarKendaraan.Count)
            {
                Console.WriteLine("Pilihan tidak valid.");
                return;
            }

            dynamic kendaraan = DaftarKendaraan[pilih - 1];

            Console.Write("Tanggal peminjaman (yyyy-MM-dd): ");
            string tanggal = Console.ReadLine();

            Console.Write($"Lama sewa (1–{Config.MaxDuration} hari): ");
            if (!int.TryParse(Console.ReadLine(), out int hari) || hari < 1 || hari > Config.MaxDuration)
            {
                Console.WriteLine("Durasi tidak valid.");
                return;
            }

            int harga = Config.GetHargaSewa(TipeKendaraan);
            int total = harga * hari;

            Console.WriteLine($"\n{kendaraan.Brand} {kendaraan.Model} akan disewa mulai {tanggal} selama {hari} hari.");
            Console.WriteLine($"Harga per hari: {Config.Currency} {harga:N0}");
            Console.WriteLine($"Total: {Config.Currency} {total:N0}");

            Console.Write("Konfirmasi peminjaman? (y/n): ");
            var konfirmasi = Console.ReadLine();
            if (konfirmasi?.ToLower() == "y")
            {
                Console.WriteLine("\nPeminjaman berhasil.");
                TampilkanDetailDanSimpan(kendaraan, tanggal, hari, total);
            }
            else
            {
                Console.WriteLine("Peminjaman dibatalkan.");
            }
        }

        private void TampilkanDetailDanSimpan(dynamic kendaraan, string tanggal, int hari, int total)
        {
            var transaksi = new Transaction
            {
                Vehicle = $"{kendaraan.Brand} {kendaraan.Model}",
                Type = TipeKendaraan,
                TanggalPinjam = tanggal,
                LamaHari = hari,
                TotalHarga = total
            };

            Console.WriteLine("\n--- Detail Transaksi ---");
            Console.WriteLine($"Kendaraan     : {transaksi.Vehicle}");
            Console.WriteLine($"Jenis         : {transaksi.Type}");
            Console.WriteLine($"Tanggal Pinjam: {transaksi.TanggalPinjam}");
            Console.WriteLine($"Durasi        : {transaksi.LamaHari} hari");
            Console.WriteLine($"Total Harga   : Rp {transaksi.TotalHarga:N0}");

            SimpanTransaksi(transaksi);

            
        }

        private void SimpanTransaksi(Transaction record)
        {
            List<Transaction> history = new();

            if (File.Exists(historyPath))
            {
                var json = File.ReadAllText(historyPath);
                history = JsonSerializer.Deserialize<List<Transaction>>(json) ?? new();
            }

            history.Add(record);
            File.WriteAllText(historyPath, JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
