using System.Net.Http;
using System.Text.Json;
using System.Text;
using model;
using config;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Test_API_tubes.Models;

namespace controller
{
    public class Penyewaan
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseUrl = "https://localhost:44376"; // Sesuaikan dengan URL API

        public async Task TampilkanMenu()
        {
            Console.WriteLine("\n=== Fitur Sewa Kendaraan ===");

            var config = RuntimeConfig.Load();

            // Ambil semua kendaraan tanpa memilih tipe
            var semuaKendaraan = await AmbilKendaraanDariAPIAsync();
            if (semuaKendaraan == null || semuaKendaraan.Count == 0)
            {
                Console.WriteLine("Tidak ada kendaraan yang tersedia.");
                return;
            }

            var sewa = new Sistemsewa<VehicleDto>(semuaKendaraan, config, _httpClient, _baseUrl);
            await sewa.Jalankan();
        }

        private async Task<List<VehicleDto>> AmbilKendaraanDariAPIAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Gagal mengambil data kendaraan dari API. Status: {response.StatusCode}");
                    return new();
                }

                var jsonData = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<List<VehicleDto>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return data ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menghubungi API: {ex.Message}");
                return new();
            }
        }
    }
}
