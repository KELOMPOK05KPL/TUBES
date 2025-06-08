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

        public async Task<List<Vehicle>> TestGetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<bool> TestCreateAsync(Vehicle vehicle)
        {
            return await CreateAsync(vehicle);
        }

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
                        await DisplayAllVehicles();
                        break;
                    case "2":
                        await HandleCrudOperation("create");
                        break;
                    case "3":
                        await HandleCrudOperation("update");
                        break;
                    case "4":
                        await HandleCrudOperation("delete");
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                Console.WriteLine("\nTekan apa saja untuk melanjutkan...");
                Console.ReadKey();
            }
        }

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

        private async Task HandleCrudOperation(string operation)
        {
            if (!_crudOperations.ContainsKey(operation))
            {
                Console.WriteLine("Operasi tidak valid!");
                return;
            }

            if (operation == "create")
            {
                var newVehicle = CreateVehicleForm();
                var success = await _crudOperations[operation](newVehicle);
                Console.WriteLine(success ? "Berhasil menambahkan!" : "Gagal menambahkan!");
                return;
            }

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

            if (operation == "update")
            {
                vehicle = UpdateVehicleForm(vehicle);
            }

            var result = await _crudOperations[operation](vehicle);
            Console.WriteLine(result ? "Operasi berhasil!" : "Operasi gagal!");
        }

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
