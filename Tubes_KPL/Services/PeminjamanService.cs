// File: Services/PeminjamanServiceTableDriven.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_API_tubes.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tubes_KPL.Services
{
    public class PeminjamanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _riwayatFilePath = "Data/RiwayatPeminjaman.json";

        private readonly Dictionary<string, Func<int, Task<bool>>> _actionHandlers;

        public PeminjamanService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');

            // Table Driven
            _actionHandlers = new Dictionary<string, Func<int, Task<bool>>>
            {
                { "rent", id => ProsesPeminjaman(id) },
                { "return", id => ProsesPengembalian(id) }
            };
        }

        public async Task<bool> HandleAction(string action, int vehicleId)
        {
            if (_actionHandlers.TryGetValue(action.ToLower(), out var handler))
                return await handler(vehicleId);

            Console.WriteLine("Aksi tidak dikenal.");
            return false;
        }

        private async Task<bool> ProsesPeminjaman(int id)
        {
            var vehicle = await GetVehicle(id);
            if (!IsVehicleAvailable(vehicle)) return false;

            var request = new { NamaPeminjam = "User" }; 
            if (!await SendPostRequest($"{_baseUrl}/api/vehicles/{id}/rent", request))
                return false;

            var updated = await GetVehicle(id);
            if (updated.State != VehicleState.Rented)
            {
                Console.WriteLine("Status kendaraan tidak berubah setelah peminjaman.");
                return false;
            }

            await SimpanRiwayatLokal(new RiwayatPeminjaman
            {
                VehicleId = id,
                Brand = vehicle.Brand,
                Type = vehicle.Type,
                Peminjam = request.NamaPeminjam,
                TanggalPinjam = DateTime.Now,
                Status = "Dipinjam"
            });

            Console.WriteLine($"Kendaraan {vehicle.Brand} {vehicle.Type} berhasil dipinjam.");
            return true;
        }

        private async Task<bool> ProsesPengembalian(int id)
        {
            var vehicle = await GetVehicle(id);
            if (vehicle?.State != VehicleState.Rented)
            {
                Console.WriteLine("Kendaraan tidak sedang dipinjam atau tidak ditemukan.");
                return false;
            }

            if (!await SendPostRequest($"{_baseUrl}/api/vehicles/{id}/return", null))
                return false;

            var updated = await GetVehicle(id);
            if (updated.State != VehicleState.Available)
            {
                Console.WriteLine("Status kendaraan tidak berubah setelah pengembalian.");
                return false;
            }

            await UpdateRiwayatLokal(id);
            Console.WriteLine("Pengembalian berhasil.");
            return true;
        }

        //Reusable Code

        private async Task<Vehicle> GetVehicle(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<Vehicle>();
        }

        private bool IsVehicleAvailable(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                Console.WriteLine("Kendaraan tidak ditemukan.");
                return false;
            }

            if (vehicle.State != VehicleState.Available)
            {
                Console.WriteLine($"Kendaraan tidak tersedia. Status: {vehicle.State}");
                return false;
            }

            return true;
        }

        private async Task<bool> SendPostRequest(string url, object data)
        {
            var content = data != null
                ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
                : null;

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Gagal melakukan request. Error: {await response.Content.ReadAsStringAsync()}");
                return false;
            }

            return true;
        }

        private async Task SimpanRiwayatLokal(RiwayatPeminjaman riwayat)
        {
            try
            {
                List<RiwayatPeminjaman> list = new();
                if (System.IO.File.Exists(_riwayatFilePath))
                {
                    var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                    list = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new();
                }

                riwayat.Id = list.Count > 0 ? list.Max(r => r.Id) + 1 : 1;
                list.Add(riwayat);

                await System.IO.File.WriteAllTextAsync(
                    _riwayatFilePath,
                    JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan riwayat lokal: {ex.Message}");
            }
        }

        private async Task UpdateRiwayatLokal(int vehicleId)
        {
            try
            {
                if (!System.IO.File.Exists(_riwayatFilePath)) return;

                var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                var list = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                var riwayat = list?.FirstOrDefault(r => r.VehicleId == vehicleId && r.TanggalKembali == null);
                if (riwayat != null)
                {
                    riwayat.TanggalKembali = DateTime.Now;
                    riwayat.Status = "Dikembalikan";

                    await System.IO.File.WriteAllTextAsync(
                        _riwayatFilePath,
                        JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal update riwayat lokal: {ex.Message}");
            }
        }

        public async Task TampilkanRiwayat()
        {
            try
            {
                if (!System.IO.File.Exists(_riwayatFilePath))
                {
                    Console.WriteLine("Belum ada riwayat peminjaman.");
                    return;
                }

                var json = await System.IO.File.ReadAllTextAsync(_riwayatFilePath);
                var list = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                Console.WriteLine("\nRiwayat Peminjaman:");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("| ID  | Kendaraan          | Peminjam       | Tanggal Pinjam    | Status     |");
                Console.WriteLine("=============================================================================");

                foreach (var r in list)
                {
                    Console.WriteLine($"| {r.Id,-3} | {r.Brand + " " + r.Type,-18} | {r.Peminjam,-14} | {r.TanggalPinjam:yyyy-MM-dd HH:mm} | {r.Status,-10} |");
                }

                Console.WriteLine("=============================================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menampilkan riwayat: {ex.Message}");
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
    }
    }
