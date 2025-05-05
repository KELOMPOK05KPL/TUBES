// File: Tubes_KPL/Services/PeminjamanService.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_API_tubes.Models;
//using Tubes_API.Models;

namespace Tubes_KPL.Services
{
    public class PeminjamanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _riwayatFilePath = "Data/RiwayatPeminjaman.json";

        public PeminjamanService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public async Task<bool> PinjamKendaraan(int id, string namaPeminjam)
        {
            try
            {
                Console.WriteLine($"\nMemproses peminjaman kendaraan ID: {id}...");

                // 1. Verifikasi kendaraan tersedia
                var vehicle = await GetVehicle(id);
                if (vehicle == null)
                {
                    Console.WriteLine("Kendaraan tidak ditemukan!");
                    return false;
                }

                Console.WriteLine($"Status awal: {vehicle.State}");
                if (vehicle.State != VehicleState.Available)
                {
                    Console.WriteLine($"Kendaraan tidak tersedia. Status saat ini: {vehicle.State}");
                    return false;
                }

                // 2. Proses peminjaman ke API
                var request = new { NamaPeminjam = namaPeminjam };
                var content = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(
                    $"{_baseUrl}/api/vehicles/{id}/rent",
                    content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Gagal meminjam kendaraan. Error: {errorContent}");
                    return false;
                }

                // 3. Verifikasi status setelah peminjaman
                var updatedVehicle = await GetVehicle(id);
                if (updatedVehicle.State != VehicleState.Rented)
                {
                    Console.WriteLine("Peminjaman gagal - status tidak berubah");
                    return false;
                }

                // 4. Simpan riwayat peminjaman lokal (backup)
                await SimpanRiwayatLokal(new RiwayatPeminjaman
                {
                    VehicleId = id,
                    Brand = vehicle.Brand,
                    Type = vehicle.Type,
                    Peminjam = namaPeminjam,
                    TanggalPinjam = DateTime.Now,
                    Status = "Dipinjam"
                });

                Console.WriteLine("Peminjaman berhasil dicatat!");
                Console.WriteLine($"Kendaraan {vehicle.Brand} {vehicle.Type} berhasil dipinjam oleh {namaPeminjam}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<Vehicle> GetVehicle(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<Vehicle>();
        }

        private async Task SimpanRiwayatLokal(RiwayatPeminjaman riwayat)
        {
            try
            {
                List<RiwayatPeminjaman> riwayatList;

                if (System.IO.File.Exists(_riwayatFilePath))
                {
                    var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                    riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new List<RiwayatPeminjaman>();
                }
                else
                {
                    riwayatList = new List<RiwayatPeminjaman>();
                }

                riwayat.Id = riwayatList.Count > 0 ? riwayatList.Max(r => r.Id) + 1 : 1;

                riwayatList.Add(riwayat);

                var options = new JsonSerializerOptions { WriteIndented = true };
                await System.IO.File.WriteAllTextAsync(
                    _riwayatFilePath,
                    JsonSerializer.Serialize(riwayatList, options));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan riwayat lokal: {ex.Message}");
            }
        }

        public async Task TampilkanRiwayatPeminjaman()
        {
            try
            {
                if (!System.IO.File.Exists(_riwayatFilePath))
                {
                    Console.WriteLine("Belum ada riwayat peminjaman.");
                    return;
                }

                var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                var riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                Console.WriteLine("\nRiwayat Peminjaman Kendaraan:");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("| ID  | Kendaraan          | Peminjam       | Tanggal Pinjam    | Status     |");
                Console.WriteLine("=============================================================================");

                foreach (var riwayat in riwayatList)
                {
                    Console.WriteLine($"| {riwayat.Id,-3} | {riwayat.Brand + " " + riwayat.Type,-18} | {riwayat.Peminjam,-14} | {riwayat.TanggalPinjam:yyyy-MM-dd HH:mm} | {riwayat.Status,-10} |");
                }

                Console.WriteLine("=============================================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memuat riwayat: {ex.Message}");
            }

        }
        public async Task<bool> KembalikanKendaraan(int id)
        {
            try
            {
                Console.WriteLine($"\nMemproses pengembalian kendaraan ID: {id}...");

                // 1. Verifikasi status kendaraan
                var vehicle = await GetVehicle(id);
                if (vehicle == null)
                {
                    Console.WriteLine("Kendaraan tidak ditemukan!");
                    return false;
                }

                Console.WriteLine($"Status saat ini: {vehicle.State}");
                if (vehicle.State != VehicleState.Rented)
                {
                    Console.WriteLine("Kendaraan tidak sedang dipinjam");
                    return false;
                }

                // 2. Proses pengembalian ke API
                var response = await _httpClient.PostAsync(
                    $"{_baseUrl}/api/vehicles/{id}/return",
                    null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Gagal mengembalikan kendaraan. Error: {errorContent}");
                    return false;
                }

                // 3. Verifikasi status setelah pengembalian
                var updatedVehicle = await GetVehicle(id);
                if (updatedVehicle.State != VehicleState.Available)
                {
                    Console.WriteLine("Pengembalian gagal - status tidak berubah");
                    return false;
                }

                // 4. Update riwayat lokal (backup)
                await UpdateRiwayatLokal(id);

                Console.WriteLine("Pengembalian berhasil dicatat!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private async Task UpdateRiwayatLokal(int vehicleId)
        {
            try
            {
                if (!System.IO.File.Exists(_riwayatFilePath))
                    return;

                var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                var riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                var riwayatAktif = riwayatList?
                    .FirstOrDefault(r => r.VehicleId == vehicleId && r.TanggalKembali == null);

                if (riwayatAktif != null)
                {
                    riwayatAktif.TanggalKembali = DateTime.Now;
                    riwayatAktif.Status = "Dikembalikan";

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    await System.IO.File.WriteAllTextAsync(
                        _riwayatFilePath,
                        JsonSerializer.Serialize(riwayatList, options));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal update riwayat lokal: {ex.Message}");
            }
        }
    }
}