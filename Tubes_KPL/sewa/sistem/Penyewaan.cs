using System.Net.Http;
using System.Text.Json;
using System.Text;

using config;
using System.Net.Http.Json;
using Tubes_KPL.LihatKendaraan; 
using System.IO;
using Test_API_tubes.Models;

namespace controller
{
   
      

    public class VehicleDto
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int State { get; set; }


    }



    public class Penyewaan
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseUrl = "https://localhost:44376"; // Sesuaikan dengan URL API masing2
        private readonly KendaraanViewer _kendaraanViewer;
        private readonly string _riwayatFilePath = "data/RiwayatPeminjaman.json";

        public Penyewaan()
        {
            _kendaraanViewer = new KendaraanViewer(_httpClient, _baseUrl);
        }

        public async Task TampilkanMenu()
        {
            Console.WriteLine("\n=== Fitur Sewa Kendaraan ===");

            var config = RuntimeConfig.Load();

            await _kendaraanViewer.TampilkanSemuaKendaraan();

            Console.Write("\nMasukkan ID Kendaraan yang ingin dipinjam: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID kendaraan tidak valid.");
                return;
            }

            await _kendaraanViewer.TampilkanDetailKendaraan(id);

            Console.Write("Masukkan nama Anda: ");
            var namaPeminjam = Console.ReadLine();

            Console.Write($"Lama sewa (hari {config.durasi.min}-{config.durasi.max}): ");
            if (!int.TryParse(Console.ReadLine(), out int lamaHari) || lamaHari < config.durasi.min || lamaHari > config.durasi.max)
            {
                Console.WriteLine("Durasi tidak valid.");
                return;
            }

            var kendaraan = await GetVehicle(id);
            if (kendaraan == null)
            {
                Console.WriteLine("Kendaraan tidak ditemukan.");
                return;
            }

            string tipe = kendaraan.Type.ToLower();
            int harga = config.harga_sewa.ContainsKey(tipe) ? config.harga_sewa[tipe] : 0;
            int total = harga * lamaHari;

            Console.Write($"\nApakah Anda yakin ingin meminjam kendaraan ini? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
            {
                Console.WriteLine("Transaksi dibatalkan.");
                return;
            }

            var success = await PinjamKendaraan(id, namaPeminjam);
            if (!success)
            {
                Console.WriteLine("Gagal meminjam kendaraan.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Silahkan ambil kendaraan!");

            await SimpanRiwayatLokal(new RiwayatPeminjaman
            {
                VehicleId = kendaraan.Id,
                Brand = kendaraan.Brand,
                Type = kendaraan.Type,
                Peminjam = namaPeminjam,
                TanggalPinjam = DateTime.Now,
                Status = "Dipinjam"
            });

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

            return true;
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

        private async Task SimpanRiwayatLokal(RiwayatPeminjaman riwayat)
        {
            try
            {
                List<RiwayatPeminjaman> riwayatList;

                if (File.Exists(_riwayatFilePath))
                {
                    var json = await File.ReadAllTextAsync(_riwayatFilePath);
                    riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new List<RiwayatPeminjaman>();
                }
                else
                {
                    riwayatList = new List<RiwayatPeminjaman>();
                }

                riwayat.Id = riwayatList.Count > 0 ? riwayatList.Max(r => r.Id) + 1 : 1;

                riwayatList.Add(riwayat);
                await File.WriteAllTextAsync(_riwayatFilePath, JsonSerializer.Serialize(riwayatList, new JsonSerializerOptions { WriteIndented = true }));

                Console.WriteLine(" Riwayat peminjaman berhasil disimpan!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan riwayat lokal: {ex.Message}");
            }
        }
    }
}
