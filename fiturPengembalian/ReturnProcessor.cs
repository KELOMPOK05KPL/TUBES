using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using fiturPengembalian;

namespace fiturPengembalian
{
    public static class ReturnProcessor
    {
        public static PenaltyConfig LoadPenaltyConfig(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<PenaltyConfig>(json);
        }

        public static List<VehicleData> LoadVehicles(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<VehicleData>>(json);
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
            Console.WriteLine($"Nama Kendaraan   : {vehicle.Name}");
            Console.WriteLine($"Jenis Kendaraan  : {typeKey}");
            Console.WriteLine($"Tanggal Sewa     : {vehicle.RentDate:dd/MM/yyyy}");
            Console.WriteLine($"Tanggal Kembali  : {returnDate:dd/MM/yyyy}");
            Console.WriteLine($"Durasi Sewa      : {totalDays} hari");
            Console.WriteLine($"Terlambat        : {overdueDays} hari");
            Console.WriteLine($"Total Denda      : Rp{totalPenalty:N0}");
        }
    }
}
