using System;
using System.Linq;
using fiturPengembalian;

class Program
{    
    static void Main()
    {
        string penaltyPath = "penalty_config.json";
        string vehiclePath = "Vehicle_Data.json";

        var config = ReturnProcessor.LoadPenaltyConfig(penaltyPath);
        var vehicleList = ReturnProcessor.LoadVehicles(vehiclePath);

        Console.WriteLine("=== Daftar Kendaraan Tersedia ===");
        var available = vehicleList.Where(v => v.IsAvailable).ToList();
        for (int i = 0; i < available.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {available[i].Name} ({available[i].Type})");
        }

        Console.Write("Pilih nomor kendaraan yang ingin dikembalikan: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > available.Count)
        {
            Console.WriteLine("Pilihan tidak valid.");
            return;
        }

        var selected = available[choice - 1];

        Console.Write("Masukkan tanggal sewa (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime rentDate))
        {
            Console.WriteLine("Format tanggal tidak valid.");
            return;
        }

        Console.Write("Masukkan tanggal pengembalian (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
        {
            Console.WriteLine("Format tanggal tidak valid.");
            return;
        }

        var vehicle = new Vehicle<string>(selected.Type, rentDate, selected.Name);
        ReturnProcessor.ProcessReturn(vehicle, config, returnDate);
    }
}
