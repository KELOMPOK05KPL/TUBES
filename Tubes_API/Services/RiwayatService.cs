// Tubes_API/Services/RiwayatService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Test_API_tubes.Models;
//using Tubes_API.Models;
using Test_API_tubes.Controllers;

namespace Tubes_API.Services
{
    public class RiwayatService
    {
        private readonly string _filePath = "Data/riwayatPeminjaman.json";

        public List<RiwayatPeminjaman> GetAllRiwayat()
        {
            if (!File.Exists(_filePath))
                return new List<RiwayatPeminjaman>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json)
                ?? new List<RiwayatPeminjaman>();
        }

        public void TambahRiwayat(RiwayatPeminjaman riwayatBaru)
        {
            var semuaRiwayat = GetAllRiwayat();
            riwayatBaru.Id = semuaRiwayat.Count > 0 ? semuaRiwayat.Max(r => r.Id) + 1 : 1;
            semuaRiwayat.Add(riwayatBaru);
            SaveRiwayat(semuaRiwayat);
        }

        private void SaveRiwayat(List<RiwayatPeminjaman> riwayat)
        {
            string json = JsonSerializer.Serialize(riwayat, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, json);
        }

        // Di Tubes_API/Services/RiwayatService.cs
        public void UpdateRiwayatPengembalian(int vehicleId)
        {
            var semuaRiwayat = GetAllRiwayat();
            var riwayatAktif = semuaRiwayat
                .FirstOrDefault(r => r.VehicleId == vehicleId && r.TanggalKembali == null);

            if (riwayatAktif != null)
            {
                riwayatAktif.TanggalKembali = DateTime.Now;
                riwayatAktif.Status = "Dikembalikan";
                SaveRiwayat(semuaRiwayat);
            }
        }
    }
}