using sewa_kendaraan;
using sewa_kendaraan.fiturSewa.config;
using sewa_kendaraan.fiturSewa.fitur;
using sewa_kendaraan.fiturSewa.model;
using System.Net.Http.Json;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("\n=== Fitur Sewa Kendaraan ===");

        var config = runtimeconfig.Load();

        Console.WriteLine("Pilih jenis kendaraan:");
        Console.WriteLine("1. Motor");
        Console.WriteLine("2. Mobil");
        Console.Write("Pilihan: ");
        var input = Console.ReadLine();

        string tipe = input switch
        {
            "1" => "motor",
            "2" => "mobil",
            _ => ""
        };

        if (string.IsNullOrEmpty(tipe))
        {
            Console.WriteLine("Jenis kendaraan tidak valid.");
            return;
        }

        var semua = await AmbilKendaraanAsync();
        var tersedia = semua
            .Where(k => k.Type.Equals(tipe, StringComparison.OrdinalIgnoreCase) && k.IsAvailable)
            .ToList();

        if (tersedia.Count == 0)
        {
            Console.WriteLine($"Tidak ada {tipe} yang tersedia.");
            return;
        }

        var sewa = new SewaKendaraan<VehicleDto>(tersedia, config, tipe);
        sewa.Jalankan();
    }

    static async Task<List<VehicleDto>> AmbilKendaraanAsync()
    {
        string path = "data_kendaraan.json";
        if (!File.Exists(path))
        {
            Console.WriteLine("File data kendaraan tidak ditemukan.");
            return new();
        }

        using var stream = File.OpenRead(path);
        var data = await JsonSerializer.DeserializeAsync<List<VehicleDto>>(stream);
        return data ?? new();
    }

}
