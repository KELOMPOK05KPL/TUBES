using System;
using Tubes_KPL.Models;
using Tubes_KPL.Services;

class Program
{
    static IVehicleService vehicleService = new VehicleService();

    static void Main()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Sistem Manajemen Kendaraan ===");
            Console.WriteLine("1. Tambah Kendaraan");
            Console.WriteLine("2. Lihat Daftar Kendaraan");
            Console.WriteLine("3. Cari Kendaraan by ID");
            Console.WriteLine("4. Update Kendaraan");
            Console.WriteLine("5. Hapus Kendaraan");
            Console.WriteLine("6. Keluar");
            Console.Write("Pilih menu (1-6): ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddVehicleMenu();
                    break;
                case "2":
                    ShowAllVehicles();
                    break;
                case "3":
                    FindVehicleById();
                    break;
                case "4":
                    UpdateVehicleMenu();
                    break;
                case "5":
                    DeleteVehicleMenu();
                    break;
                case "6":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Input tidak valid!");
                    break;
            }
            Console.WriteLine("\nTekan apa saja untuk melanjutkan...");
            Console.ReadKey();
        }
    }

    // Menu 1: Tambah Kendaraan
    static void AddVehicleMenu()
    {
        Console.WriteLine("\n--- Tambah Kendaraan Baru ---");
        var vehicle = new Vehicle();

        Console.Write("ID Kendaraan: ");
        vehicle.Id = Console.ReadLine();

        Console.Write("Merek: ");
        vehicle.Brand = Console.ReadLine();

        Console.Write("Model: ");
        vehicle.Model = Console.ReadLine();

        Console.Write("Tahun: ");
        vehicle.Year = int.Parse(Console.ReadLine());

        vehicle.IsAvailable = true;

        try
        {
            vehicleService.AddVehicle(vehicle);
            Console.WriteLine("Kendaraan berhasil ditambahkan!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Menu 2: Tampilkan Semua Kendaraan
    static void ShowAllVehicles()
    {
        Console.WriteLine("\n--- Daftar Kendaraan ---");
        var vehicles = vehicleService.GetAllVehicles();
        foreach (var v in vehicles)
        {
            Console.WriteLine($"{v.Id} | {v.Brand} {v.Model} ({v.Year}) | Tersedia: {v.IsAvailable}");
        }
    }

    // Menu 3: Cari Kendaraan by ID
    static void FindVehicleById()
    {
        Console.Write("\nMasukkan ID Kendaraan: ");
        var id = Console.ReadLine();

        try
        {
            var vehicle = vehicleService.GetVehicleById(id);
            Console.WriteLine($"\nDetail Kendaraan:");
            Console.WriteLine($"ID: {vehicle.Id}");
            Console.WriteLine($"Merek: {vehicle.Brand}");
            Console.WriteLine($"Model: {vehicle.Model}");
            Console.WriteLine($"Tahun: {vehicle.Year}");
            Console.WriteLine($"Tersedia: {vehicle.IsAvailable}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Menu 4: Update Kendaraan
    static void UpdateVehicleMenu()
    {
        Console.Write("\nMasukkan ID Kendaraan yang akan diupdate: ");
        var id = Console.ReadLine();

        try
        {
            var existingVehicle = vehicleService.GetVehicleById(id);
            Console.WriteLine("\nData Lama:");
            Console.WriteLine($"Merek: {existingVehicle.Brand}");
            Console.WriteLine($"Model: {existingVehicle.Model}");

            Console.Write("\nMerek Baru (kosongkan jika tidak diubah): ");
            var brand = Console.ReadLine();
            if (!string.IsNullOrEmpty(brand)) existingVehicle.Brand = brand;

            Console.Write("Model Baru (kosongkan jika tidak diubah): ");
            var model = Console.ReadLine();
            if (!string.IsNullOrEmpty(model)) existingVehicle.Model = model;

            vehicleService.UpdateVehicle(existingVehicle);
            Console.WriteLine("Data berhasil diupdate!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Menu 5: Hapus Kendaraan
    static void DeleteVehicleMenu()
    {
        Console.Write("\nMasukkan ID Kendaraan yang akan dihapus: ");
        var id = Console.ReadLine();

        try
        {
            vehicleService.DeleteVehicle(id);
            Console.WriteLine("Kendaraan berhasil dihapus!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}