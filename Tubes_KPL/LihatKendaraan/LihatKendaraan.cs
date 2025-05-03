// File: Tubes_KPL/LihatKendaraan/LihatKendaraan.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Test_API_tubes.Models;
//using Tubes_API.Models;
using Test_API_tubes.Models;

namespace Tubes_KPL.LihatKendaraan
{
    public class KendaraanViewer
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public KendaraanViewer(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task TampilkanSemuaKendaraan()
        {
            try
            {
                Console.WriteLine("\nMengambil data kendaraan...");
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Gagal mengambil data. Status: {response.StatusCode}");
                    return;
                }

                var vehicles = await response.Content.ReadFromJsonAsync<List<Vehicle>>();

                if (vehicles == null || vehicles.Count == 0)
                {
                    Console.WriteLine("Tidak ada kendaraan tersedia.");
                    return;
                }

                Console.WriteLine("\nDaftar Kendaraan Tersedia:");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("| ID  | Tipe\t| Brand\t\t| Model\t\t| Status\t|");
                Console.WriteLine("-----------------------------------------------------------------");

                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"| {vehicle.Id,-3} | {vehicle.Type,-7} | {vehicle.Brand,-13} | {vehicle.Model,-13} | {vehicle.State,-14}|");
                }

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"Total: {vehicles.Count} kendaraan");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }
        }

        public async Task TampilkanDetailKendaraan(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Kendaraan dengan ID {id} tidak ditemukan.");
                    return;
                }

                var vehicle = await response.Content.ReadFromJsonAsync<Vehicle>();

                Console.WriteLine("\nDetail Kendaraan:");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"ID: {vehicle.Id}");
                Console.WriteLine($"Merek: {vehicle.Brand}");
                Console.WriteLine($"Tipe: {vehicle.Type}");
                Console.WriteLine($"Status: {vehicle.State}");
                Console.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }
        }
    }
}