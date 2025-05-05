using System.Text.Json;
using Test_API_tubes.Models;
using Tubes_API.Helpers;

namespace Tubes_API.Services
{
    public class VehicleService
    {
        private readonly string filePath = "Data/Vehicles.json";
        private readonly RiwayatService _riwayatService;
        

        public VehicleService(RiwayatService riwayatService)
        {
            _riwayatService = riwayatService;
        }

        public List<Vehicle> GetAll()
        {
            return FileRepository<Vehicle>.Load(filePath);
        }

        public Vehicle? GetById(int id) => GetAll().FirstOrDefault(v => v.Id == id);

        public void SaveAll(List<Vehicle> vehicles)
        {
            FileRepository<Vehicle>.Save(filePath, vehicles);
        }

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
                // Tidak mengubah State saat update
                SaveAll(vehicles);
            }
        }

        public void Delete(int id)
        {
            var vehicles = GetAll();
            var updated = vehicles.Where(v => v.Id != id).ToList();
            SaveAll(updated);
        }

        // Fitur peminjaman kendaraan
        public (bool success, string message) RentVehicle(int id, string namaPeminjam)
        {
            var vehicles = GetAll();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                return (false, "Kendaraan tidak ditemukan");

            if (vehicle.State != VehicleState.Available)
                return (false, $"Kendaraan tidak tersedia (Status: {vehicle.State})");

            vehicle.State = VehicleState.Rented;
            SaveAll(vehicles);

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

        // Fitur pengembalian kendaraan
        public (bool success, string message) ReturnVehicle(int id)
        {
            var vehicles = GetAll();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                return (false, "Kendaraan tidak ditemukan.");

            if (vehicle.State != VehicleState.Rented)
                return (false, "Kendaraan tidak sedang dipinjam.");

            // Perbarui status kendaraan
            vehicle.State = VehicleState.Available;

            // Simpan perubahan ke file
            SaveAll(vehicles);

            return (true, "Kendaraan berhasil dikembalikan.");
        }

    }
}
