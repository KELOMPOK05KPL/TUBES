using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tubes_KPL.Pengembalian;

namespace Tubes_KPL.Pengembalian
{
    public class PengembalianKendaraan
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _riwayatFilePath;

        public PengembalianKendaraan(HttpClient httpClient, string baseUrl, string riwayatFilePath)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _riwayatFilePath = riwayatFilePath;
        }

        public async Task TampilkanMenuPengembalian()
        {
            Console.WriteLine("\n=== Fitur Pengembalian Kendaraan ===");

            var kendaraanDipinjam = await CariKendaraanDipinjamAsync();
            if (kendaraanDipinjam == null)
            {
                Console.WriteLine("Tidak ada kendaraan yang sedang dipinjam.");
                return;
            }

            Console.WriteLine("\nDetail Kendaraan yang Dipinjam:");
            Console.WriteLine($"Merek: {kendaraanDipinjam.Brand}");
            Console.WriteLine($"Model: {kendaraanDipinjam.Type}");
            Console.WriteLine($"Peminjam: {kendaraanDipinjam.Peminjam}");
            Console.WriteLine($"Status: {kendaraanDipinjam.Status}");

            Console.Write("\nIngin dikembalikan? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                var success = await ProsesPengembalianAsync(kendaraanDipinjam.VehicleId);
                Console.WriteLine(success ? "Status: Sudah dikembalikan." : "Pengembalian gagal.");
            }
            else
            {
                Console.WriteLine("Transaksi dibatalkan.");
            }
        }

        private async Task<RiwayatPeminjaman> CariKendaraanDipinjamAsync()
        {
            if (!File.Exists(_riwayatFilePath))
                return null;

            var json = await File.ReadAllTextAsync(_riwayatFilePath);
            var riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json);

            return riwayatList?.Find(r => r.Status == "Dipinjam");
        }

        private async Task<bool> ProsesPengembalianAsync(int vehicleId)
        {
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/vehicles/{vehicleId}/return", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> KembalikanKendaraan(int vehicleId)
        {
            // Update status kendaraan di API
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/vehicles/{vehicleId}/return", null);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Gagal memperbarui status kendaraan.");
                return false;
            }

            // Update riwayat peminjaman
            var riwayatList = await GetRiwayatPeminjaman();
            var riwayat = riwayatList.FirstOrDefault(r => r.VehicleId == vehicleId && r.Status == "Dipinjam");
            if (riwayat != null)
            {
                riwayat.Status = "Dikembalikan";
                riwayat.TanggalKembali = DateTime.Now;
                await SimpanRiwayatLokal(riwayatList);
            }

            Console.WriteLine("Kendaraan berhasil dikembalikan.");
            return true;
        }

        private async Task<List<RiwayatPeminjaman>> GetRiwayatPeminjaman()
        {
            if (!File.Exists("data/RiwayatPeminjaman.json"))
                return new List<RiwayatPeminjaman>();

            var json = await File.ReadAllTextAsync("data/RiwayatPeminjaman.json");
            return JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json) ?? new List<RiwayatPeminjaman>();
        }

        private async Task SimpanRiwayatLokal(List<RiwayatPeminjaman> riwayatList)
        {
            await File.WriteAllTextAsync("data/RiwayatPeminjaman.json", JsonSerializer.Serialize(riwayatList, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
