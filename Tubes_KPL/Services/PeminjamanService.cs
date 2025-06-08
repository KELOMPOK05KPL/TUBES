using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_API_tubes.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Tubes_KPL.Services
{
    public class PeminjamanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _historyFilePath = "Data/RiwayatPeminjaman.json";

        // Dictionary for table-driven action handlers
        private readonly Dictionary<string, Func<int, Task<bool>>> _actionHandlers;

        public PeminjamanService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');

            _actionHandlers = new Dictionary<string, Func<int, Task<bool>>>
            {
                { "rent", RentVehicleAsync },
                { "return", ReturnVehicleAsync }
            };
        }

        public async Task<bool> HandleAction(string action, int vehicleId)
        {
            if (_actionHandlers.TryGetValue(action.ToLower(), out var handler))
                return await handler(vehicleId);

            Console.WriteLine("Unknown action.");
            return false;
        }

        private async Task<bool> RentVehicleAsync(int id)
        {
            var vehicle = await GetVehicleAsync(id);
            if (!IsVehicleAvailable(vehicle)) return false;

            var requestData = new { NamaPeminjam = "User" };
            if (!await PostRequestAsync($"{_baseUrl}/api/vehicles/{id}/rent", requestData))
                return false;

            var updatedVehicle = await GetVehicleAsync(id);
            if (updatedVehicle.State != VehicleState.Rented)
            {
                Console.WriteLine("Vehicle state did not change after renting.");
                return false;
            }

            await SaveHistoryAsync(new RiwayatPeminjaman
            {
                VehicleId = id,
                Brand = vehicle.Brand,
                Type = vehicle.Type,
                Peminjam = requestData.NamaPeminjam,
                TanggalPinjam = DateTime.Now,
                Status = "Dipinjam"
            });

            Console.WriteLine($"Vehicle {vehicle.Brand} {vehicle.Type} successfully rented.");
            return true;
        }

        private async Task<bool> ReturnVehicleAsync(int id)
        {
            var vehicle = await GetVehicleAsync(id);
            if (vehicle?.State != VehicleState.Rented)
            {
                Console.WriteLine("Vehicle is not currently rented or not found.");
                return false;
            }

            if (!await PostRequestAsync($"{_baseUrl}/api/vehicles/{id}/return", null))
                return false;

            var updatedVehicle = await GetVehicleAsync(id);
            if (updatedVehicle.State != VehicleState.Available)
            {
                Console.WriteLine("Vehicle state did not change after return.");
                return false;
            }

            await UpdateHistoryAsync(id);
            Console.WriteLine("Vehicle successfully returned.");
            return true;
        }

        private async Task<Vehicle> GetVehicleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Vehicle>();
        }

        private bool IsVehicleAvailable(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found.");
                return false;
            }

            if (vehicle.State != VehicleState.Available)
            {
                Console.WriteLine($"Vehicle is not available. Current state: {vehicle.State}");
                return false;
            }

            return true;
        }

        private async Task<bool> PostRequestAsync(string url, object data)
        {
            var content = data != null
                ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
                : null;

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Request failed. Error: {error}");
                return false;
            }

            return true;
        }

        private async Task SaveHistoryAsync(RiwayatPeminjaman record)
        {
            try
            {
                List<RiwayatPeminjaman> history = new();
                if (File.Exists(_historyFilePath))
                {
                    var json = await File.ReadAllTextAsync(_historyFilePath);
                    history = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new();
                }

                record.Id = history.Count > 0 ? history.Max(r => r.Id) + 1 : 1;
                history.Add(record);

                await File.WriteAllTextAsync(
                    _historyFilePath,
                    JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true })
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save local history: {ex.Message}");
            }
        }

        private async Task UpdateHistoryAsync(int vehicleId)
        {
            try
            {
                if (!File.Exists(_historyFilePath)) return;

                var json = await File.ReadAllTextAsync(_historyFilePath);
                var history = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                var record = history?.FirstOrDefault(r => r.VehicleId == vehicleId && r.TanggalKembali == null);
                if (record != null)
                {
                    record.TanggalKembali = DateTime.Now;
                    record.Status = "Dikembalikan";

                    await File.WriteAllTextAsync(
                        _historyFilePath,
                        JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true })
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update local history: {ex.Message}");
            }
        }

        public async Task DisplayHistoryAsync()
        {
            try
            {
                if (!File.Exists(_historyFilePath))
                {
                    Console.WriteLine("No rental history found.");
                    return;
                }

                var json = await File.ReadAllTextAsync(_historyFilePath);
                var history = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

                Console.WriteLine("\nRental History:");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("| ID  | Vehicle            | Renter         | Rent Date        | Status     |");
                Console.WriteLine("=============================================================================");

                foreach (var record in history)
                {
                    Console.WriteLine($"| {record.Id,-3} | {record.Brand + " " + record.Type,-18} | {record.Peminjam,-14} | {record.TanggalPinjam:yyyy-MM-dd HH:mm} | {record.Status,-10} |");
                }

                Console.WriteLine("=============================================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to display history: {ex.Message}");
            }
        }

       
        public async Task<bool> ReturnVehicleDetailedAsync(int id)
        {
            try
            {
                Console.WriteLine($"\nProcessing return for vehicle ID: {id}...");

                var vehicle = await GetVehicleAsync(id);
                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle not found!");
                    return false;
                }

                Console.WriteLine($"Current status: {vehicle.State}");
                if (vehicle.State != VehicleState.Rented)
                {
                    Console.WriteLine("Vehicle is not currently rented.");
                    return false;
                }

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/vehicles/{id}/return", null);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to return vehicle. Error: {errorContent}");
                    return false;
                }

                var updated = await GetVehicleAsync(id);
                if (updated.State != VehicleState.Available)
                {
                    Console.WriteLine("Return failed - vehicle state did not change.");
                    return false;
                }

                await UpdateHistoryAsync(id);
                Console.WriteLine("Return successfully recorded!");
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
