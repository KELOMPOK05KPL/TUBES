using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tubes_KPL.Pengembalian.models;

namespace Tubes_KPL.Pengembalian.Controller
{
    public class RentalService
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseUrl = "https://localhost:44376";

        public RentalService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<Kendaraan>> GetKendaraanDipinjamAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/kendaraan/dipinjam");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Kendaraan>>(json);
            }
            return new List<Kendaraan>();
        }

        public async Task<bool> KembalikanKendaraanAsync(int id)
        {
            // Logging untuk debug
            Console.WriteLine($"Mengirim request ke: {_baseUrl}/kendaraan/kembalikan/{id}");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/kendaraan/kembalikan/{id}", null);

            // Logging hasil response dari API
            Console.WriteLine($"Response Status Code: {response.StatusCode}");

            return response.IsSuccessStatusCode;
        }


    }
}
