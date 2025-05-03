using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Test_API_tubes.Models;
using System.Text.Json;
using System.Text;

namespace Tubes_KPL
{
    public class VehicleService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5176"; 

        public VehicleService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Vehicle>>("/api/vehicles");
                return response ?? new List<Vehicle>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        public async Task<bool> PinjamKendaraan(int id, string namaPeminjam)
        {
            try
            {
                Console.WriteLine($"\nMemproses peminjaman kendaraan ID: {id}...");

                // Cek status kendaraan
                var checkResponse = await _httpClient.GetAsync($"{BaseUrl}/api/vehicles/{id}");
                if (!checkResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Kendaraan tidak ditemukan!");
                    return false;
                }

                var vehicle = await checkResponse.Content.ReadFromJsonAsync<Vehicle>();
                Console.WriteLine($"Status awal: {vehicle.State}");

                // Proses peminjaman dengan mengirim nama peminjam
                var request = new { NamaPeminjam = namaPeminjam };
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/api/vehicles/{id}/rent", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Gagal meminjam kendaraan!");
                    return false;
                }

                // Verifikasi status
                var verifyResponse = await _httpClient.GetAsync($"{BaseUrl}/api/vehicles/{id}");
                var updatedVehicle = await verifyResponse.Content.ReadFromJsonAsync<Vehicle>();

                Console.WriteLine($"Status setelah peminjaman: {updatedVehicle.State}");

                if (updatedVehicle.State == VehicleState.Rented)
                {
                    Console.WriteLine("Peminjaman berhasil dicatat!");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}