using System.Text.Json;
using Test_API_tubes.Models;
//using Tubes_RentalAPI.Models;
namespace Tubes_API.Services
{
    public class VehicleService
    {
        private readonly string filePath = "Data/vehicles.json";
        private readonly RiwayatService _riwayatService;

        public VehicleService(RiwayatService riwayatService)
        {
            _riwayatService = riwayatService;
        }

        public (bool success, string message) RentVehicle(int id, string namaPeminjam)
        {
            var vehicles = GetAll();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return (false, "Kendaraan tidak ditemukan");
            }

            if (vehicle.State != VehicleState.Available)
            {
                return (false, $"Kendaraan tidak tersedia (Status: {vehicle.State})");
            }

            // Update state kendaraan
            vehicle.State = VehicleState.Rented;
            SaveAll(vehicles);

            // Tambahkan ke riwayat
            var riwayatBaru = new RiwayatPeminjaman
            {
                VehicleId = vehicle.Id,
                Brand = vehicle.Brand,
                Type = vehicle.Type,
                Peminjam = namaPeminjam,
                TanggalPinjam = DateTime.Now,
                TanggalKembali = null,
                Status = "Dipinjam"
            };
            _riwayatService.TambahRiwayat(riwayatBaru);

            return (true, "Peminjaman berhasil");
        }

        // Di Tubes_API/Services/VehicleService.cs
        public (bool success, string message) ReturnVehicle(int id)
        {
            var vehicles = GetAll();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
            {
                return (false, "Kendaraan tidak ditemukan");
            }

            if (vehicle.State != VehicleState.Rented)
            {
                return (false, $"Kendaraan tidak sedang dipinjam (Status: {vehicle.State})");
            }

            // Update state kendaraan
            vehicle.State = VehicleState.Available;
            SaveAll(vehicles);

            // Update riwayat peminjaman
            _riwayatService.UpdateRiwayatPengembalian(id);

            return (true, "Pengembalian berhasil");
        }

        public List<Vehicle> GetAll()
        {
            if (!File.Exists(filePath))
                return new List<Vehicle>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
        }

        public void SaveAll(List<Vehicle> vehicles)
        {
            string json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public Vehicle? GetById(int id) => GetAll().FirstOrDefault(v => v.Id == id);

        public void Add(Vehicle vehicle)
        {
            var vehicles = GetAll();
            vehicle.Id = vehicles.Count > 0 ? vehicles.Max(v => v.Id) + 1 : 1;
            vehicle.State = VehicleState.Available;
            vehicles.Add(vehicle);
            SaveAll(vehicles);
        }

        public void Update(Vehicle updated)
        {
            var vehicles = GetAll();
            var index = vehicles.FindIndex(v => v.Id == updated.Id);
            if (index >= 0)
            {
                vehicles[index].Brand = updated.Brand;
                vehicles[index].Type = updated.Type;
                // State tidak berubah di sini
                SaveAll(vehicles);
            }
        }

        public void Delete(int id)
        {
            var vehicles = GetAll();
            var updated = vehicles.Where(v => v.Id != id).ToList();
            SaveAll(updated);
        }

        public bool RentVehicle(int id)
        {
            var vehicles = GetAll();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle != null && vehicle.State == VehicleState.Available)
            {
                vehicle.State = VehicleState.Rented;
                SaveAll(vehicles);
                return true;
            }
            return false; // tidak bisa disewa
        }

        // Di VehicleService.cs - Hapus yang versi bool dan pertahankan yang ini:
       
    }
}
