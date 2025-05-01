using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Tubes_KPL
{
    public class RiwayatPeminjaman
    {
        public int Id { get; set; }
        public string NamaPeminjam { get; set; }
        public string BarangDipinjam { get; set; }
        public DateTime TanggalPeminjaman { get; set; }
        public DateTime? TanggalPengembalian { get; set; }
    }

    public class RiwayatPeminjamanClient
    {
        private readonly HttpClient _httpClient;

        public RiwayatPeminjamanClient(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<RiwayatPeminjaman>> GetAllRiwayatPeminjamanAsync()
        {
            var response = await _httpClient.GetAsync("api/RiwayatPeminjaman");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<RiwayatPeminjaman>>();
        }

        public async Task<RiwayatPeminjaman> GetRiwayatPeminjamanByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/RiwayatPeminjaman/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RiwayatPeminjaman>();
            }
            throw new Exception($"Riwayat peminjaman dengan ID {id} tidak ditemukan.");
        }

        public async Task<RiwayatPeminjaman> AddRiwayatPeminjamanAsync(string namaPeminjam, string barangDipinjam)
        {
            var newRiwayat = new RiwayatPeminjaman
            {
                NamaPeminjam = namaPeminjam,
                BarangDipinjam = barangDipinjam,
                TanggalPeminjaman = DateTime.Now
            };

            var response = await _httpClient.PostAsJsonAsync("api/RiwayatPeminjaman", newRiwayat);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RiwayatPeminjaman>();
        }
    }
}
