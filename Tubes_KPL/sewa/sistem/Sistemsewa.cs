using System.Net.Http;
using System.Text.Json;
using System.Text;
using config;
using System.Net.Http.Json;
using Test_API_tubes.Models;

namespace controller
{
    public class Transaction
    {
        public string VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string TanggalSewa { get; set; }
        public int LamaHari { get; set; }
        public int TotalHarga { get; set; }
    }

    public class Sistemsewa<T> where T : VehicleDto
    {
        private readonly List<T> kendaraanTersedia;
        private readonly RuntimeConfig config;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public Sistemsewa(List<T> kendaraanTersedia, RuntimeConfig config, HttpClient httpClient, string baseUrl)
        {
            this.kendaraanTersedia = kendaraanTersedia;
            this.config = config;
            this._httpClient = httpClient;
            this._baseUrl = baseUrl;
        }

        public async Task Jalankan()
        {
            Console.WriteLine("\nDaftar kendaraan yang tersedia:");

            if (kendaraanTersedia.Count == 0)
            {
                Console.WriteLine("Tidak ada kendaraan yang tersedia.");
                return;
            }

            for (int i = 0; i < kendaraanTersedia.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {kendaraanTersedia[i].Brand} {kendaraanTersedia[i].Model} (ID: {kendaraanTersedia[i].Id})");
            }

            Console.Write("Masukkan ID Kendaraan yang ingin dipinjam: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || !kendaraanTersedia.Any(k => k.Id == id))
            {
                Console.WriteLine("ID kendaraan tidak valid.");
                return;
            }

            var kendaraan = kendaraanTersedia.First(k => k.Id == id);

            // Menampilkan detail kendaraan
            Console.WriteLine($"\nDetail Kendaraan:");
            Console.WriteLine($"Merek   : {kendaraan.Brand}");
            Console.WriteLine($"Model   : {kendaraan.Model}");
            Console.WriteLine($"Status  : {(kendaraan.State == 0 ? "Available" : "Rented")}");

            // Ambil tipe kendaraan dari API untuk menghitung harga
            string tipe = kendaraan.Type.ToLower();
            if (!config.harga_sewa.ContainsKey(tipe))
            {
                Console.WriteLine("Error: Harga untuk tipe kendaraan ini tidak tersedia.");
                return;
            }
            int harga = config.harga_sewa[tipe];

            Console.Write($"Lama sewa (hari {config.durasi.min}-{config.durasi.max}): ");
            if (!int.TryParse(Console.ReadLine(), out int lamaHari) || lamaHari < config.durasi.min || lamaHari > config.durasi.max)
            {
                Console.WriteLine("Durasi tidak valid.");
                return;
            }

            int total = harga * lamaHari;

            Console.Write("Masukkan nama Anda: ");
            var namaPeminjam = Console.ReadLine();

            Console.Write($"Apakah Anda yakin ingin meminjam kendaraan ini? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
            {
                Console.WriteLine("Transaksi dibatalkan.");
                return;
            }

            // Proses peminjaman ke API
            var success = await PinjamKendaraan(id, namaPeminjam);
            if (!success)
            {
                Console.WriteLine("Gagal meminjam kendaraan.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Silahkan ambil kendaraan!");

            // Simpan riwayat peminjaman tanpa menyimpan total harga
            await SimpanRiwayatLokal(new RiwayatPeminjaman
            {
                VehicleId = kendaraan.Id,
                Brand = kendaraan.Brand,
                Type = kendaraan.Type,
                Peminjam = namaPeminjam,
                TanggalPinjam = DateTime.Now,
                Status = "Dipinjam"
            });

            // **Pastikan harga tetap tampil di fitur penyewaan**
            Console.WriteLine($"\nRingkasan Penyewaan:");
            Console.WriteLine($"Kendaraan  : {kendaraan.Brand} {kendaraan.Model} (ID: {kendaraan.Id})");
            Console.WriteLine($"Tanggal    : {DateTime.Now:dd/MM/yyyy}");
            Console.WriteLine($"Durasi     : {lamaHari} hari");
            Console.WriteLine($"Total Harga: Rp{total:N0}");
        }

        private async Task<bool> PinjamKendaraan(int id, string namaPeminjam)
        {
            var request = new { NamaPeminjam = namaPeminjam };
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/vehicles/{id}/rent", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Gagal meminjam kendaraan. Error: {errorContent}");
                return false;
            }

            var updatedVehicle = await GetVehicle(id);
            if (updatedVehicle.State != 1) 
            {
                Console.WriteLine("Peminjaman gagal - status tidak berubah");
                return false;
            }

            return true;
        }

        private async Task SimpanRiwayatLokal(RiwayatPeminjaman riwayat)
        {
            try
            {
                List<RiwayatPeminjaman> riwayatList;

                if (System.IO.File.Exists("data/RiwayatPeminjaman.json"))
                {
                    var json = await System.IO.File.ReadAllTextAsync("data/RiwayatPeminjaman.json");
                    riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new List<RiwayatPeminjaman>();
                }
                else
                {
                    riwayatList = new List<RiwayatPeminjaman>();
                }

                riwayat.Id = riwayatList.Count > 0 ? riwayatList.Max(r => r.Id) + 1 : 1;

                riwayatList.Add(riwayat);

                await System.IO.File.WriteAllTextAsync("data/RiwayatPeminjaman.json", JsonSerializer.Serialize(riwayatList, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan riwayat lokal: {ex.Message}");
            }
        }

        private async Task<VehicleDto> GetVehicle(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Gagal mengambil detail kendaraan dari API. Status: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<VehicleDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menghubungi API: {ex.Message}");
                return null;
            }
        }
    }
}
