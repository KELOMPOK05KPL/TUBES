using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Program
{
    public class PenaltyConfig
    {
        public int MaxReturnDays { get; set; }
        public Dictionary<string, int> PenaltyPerDay { get; set; }
    }

    public class Vehicle<T>
    {
        public T Type { get; set; }
        public DateTime RentDate { get; set; }

        public Vehicle(T type, DateTime rentDate)
        {
            Type = type;
            RentDate = rentDate;
        }
    }

    public static void Main()
    {
        string configPath = "penalty_config.json";

        if (!File.Exists(configPath))
        {
            Console.WriteLine("File konfigurasi tidak ditemukan!");
            return;
        }

        PenaltyConfig config = LoadConfig(configPath);

        Console.Write("Masukkan jenis kendaraan (Motor/Mobil): ");
        string jenis = Console.ReadLine().Trim();

        if (!config.PenaltyPerDay.ContainsKey(jenis))
        {
            Console.WriteLine("Jenis kendaraan tidak dikenali. Hanya tersedia: Motor, Mobil");
            return;
        }

        Console.Write("Masukkan tanggal sewa (format: yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime rentDate))
        {
            Console.WriteLine("Format tanggal tidak valid.");
            return;
        }

        Console.Write("Masukkan tanggal pengembalian (format: yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
        {
            Console.WriteLine("Format tanggal tidak valid.");
            return;
        }

        var vehicle = new Vehicle<string>(jenis, rentDate);
        ProcessReturn(vehicle, config, returnDate);
    }

    public static void ProcessReturn<T>(Vehicle<T> vehicle, PenaltyConfig config, DateTime returnDate)
    {
        string typeKey = vehicle.Type.ToString();
        int maxDays = config.MaxReturnDays;
        int flatPenalty = config.PenaltyPerDay[typeKey];

        TimeSpan duration = returnDate - vehicle.RentDate;
        int totalDays = (int)Math.Ceiling(duration.TotalDays);
        bool isLate = totalDays > maxDays;
        int totalPenalty = isLate ? flatPenalty : 0;
        int overdueDays = isLate ? (totalDays - maxDays) : 0;

        Console.WriteLine("\n--- Ringkasan Pengembalian ---");
        Console.WriteLine($"Jenis Kendaraan  : {typeKey}");
        Console.WriteLine($"Tanggal Sewa     : {vehicle.RentDate:dd/MM/yyyy}");
        Console.WriteLine($"Tanggal Kembali  : {returnDate:dd/MM/yyyy}");
        Console.WriteLine($"Durasi Sewa      : {totalDays} hari");
        Console.WriteLine($"Terlambat        : {overdueDays} hari");
        Console.WriteLine($"Total Denda      : Rp{totalPenalty:N0}");
    }

    public static PenaltyConfig LoadConfig(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<PenaltyConfig>(json);
    }
}
