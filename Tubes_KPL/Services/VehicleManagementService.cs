using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_API_tubes.Models;

namespace Tubes_KPL.Services
{
    public class VehicleManagementService : BaseService<Vehicle>
    {
        // Dictionary untuk menyimpan operasi CRUD dengan key string dan value berupa fungsi async
        private Dictionary<string, Func<Vehicle, Task<bool>>> _crudOperations;

        public VehicleManagementService(HttpClient httpClient, string baseUrl)
            : base(httpClient, baseUrl, "vehicles")
        {
            _crudOperations = new Dictionary<string, Func<Vehicle, Task<bool>>>
            {
                ["create"] = async (vehicle) => await CreateAsync(vehicle),
                ["update"] = async (vehicle) => await UpdateAsync(vehicle.Id, vehicle),
                ["delete"] = async (vehicle) => await DeleteAsync(vehicle.Id)
            };
        }

        // Fungsi test untuk mengambil semua kendaraan
        public async Task<List<Vehicle>> TestGetAllAsync()
        {
            return await GetAllAsync();
        }

        // Fungsi test untuk menambahkan kendaraan
        public async Task<bool> TestCreateAsync(Vehicle vehicle)
        {
            return await CreateAsync(vehicle);
        }

        // Menampilkan menu interaktif untuk manajemen kendaraan
        public async Task HandleVehicleManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MANAJEMEN KENDARAAN");
                Console.WriteLine("===================");
                Console.WriteLine("1. Lihat Semua Kendaraan");
                Console.WriteLine("2. Tambah Kendaraan");
                Console.WriteLine("3. Edit Kendaraan");
                Console.WriteLine("4. Hapus Kendaraan");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilihan: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await DisplayAllVehicles(); // Menampilkan semua kendaraan
                        break;
                    case "2":
                        await HandleCrudOperation("create"); // Tambah kendaraan
                        break;
                    case "3":
                        await HandleCrudOperation("update"); // Edit kendaraan
                        break;
                    case "4":
                        await HandleCrudOperation("delete"); // Hapus kendaraan
                        break;
                    case "5":
                        return; // Keluar dari menu
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                Console.WriteLine("\nTekan apa saja untuk melanjutkan...");
                Console.ReadKey();
            }
        }

        // Menampilkan semua kendaraan yang ada
        private async Task DisplayAllVehicles()
        {
            var vehicles = await GetAllAsync();
            Console.WriteLine("\nDaftar Kendaraan:");
            Console.WriteLine("-----------------------------------------");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"ID: {v.Id} | {v.Brand} {v.Model} ({v.Type}) - {v.State}");
            }
            Console.WriteLine("-----------------------------------------");
        }

        // Menangani operasi CRUD berdasarkan nama operasi (create, update, delete)
        private async Task HandleCrudOperation(string operation)
        {
            if (!_crudOperations.ContainsKey(operation))
            {
                Console.WriteLine("Operasi tidak valid!");
                return;
            }

            // Operasi create tidak butuh ID
            if (operation == "create")
            {
                var newVehicle = CreateVehicleForm(); // Form input untuk kendaraan baru
                var success = await _crudOperations[operation](newVehicle);
                Console.WriteLine(success ? "Berhasil menambahkan!" : "Gagal menambahkan!");
                return;
            }

            // Untuk update dan delete butuh ID kendaraan
            Console.Write("Masukkan ID Kendaraan: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID tidak valid!");
                return;
            }

            var vehicle = await GetByIdAsync(id);
            if (vehicle == null)
            {
                Console.WriteLine("Kendaraan tidak ditemukan!");
                return;
            }

            // Kalau update, ambil input baru dari user
            if (operation == "update")
            {
                vehicle = UpdateVehicleForm(vehicle);
            }

            // Eksekusi operasi
            var result = await _crudOperations[operation](vehicle);
            Console.WriteLine(result ? "Operasi berhasil!" : "Operasi gagal!");
        }

        // Form input untuk kendaraan baru (digunakan saat create)
        private Vehicle CreateVehicleForm()
        {
            Console.WriteLine("\nTambah Kendaraan Baru");
            Console.WriteLine("---------------------");

            var vehicle = new Vehicle(0, "", "", "", VehicleState.Available);

            Console.Write("Tipe (Mobil/Motor): ");
            vehicle.Type = Console.ReadLine();

            Console.Write("Merek: ");
            vehicle.Brand = Console.ReadLine();

            Console.Write("Model: ");
            vehicle.Model = Console.ReadLine();

            return vehicle;
        }

        // Form input untuk mengubah data kendaraan (digunakan saat update)
        private Vehicle UpdateVehicleForm(Vehicle vehicle)
        {
            Console.WriteLine("\nEdit Kendaraan");
            Console.WriteLine("--------------");

            Console.Write($"Tipe ({vehicle.Type}): ");
            var type = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(type)) vehicle.Type = type;

            Console.Write($"Merek ({vehicle.Brand}): ");
            var brand = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(brand)) vehicle.Brand = brand;

            Console.Write($"Model ({vehicle.Model}): ");
            var model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model)) vehicle.Model = model;

            return vehicle;
        }
    }
}
