using System.Text.Json;
using System.IO;
namespace Pengembalian
{
    public class RuntimeConfig
    {
        public Dictionary<string, int> harga_sewa { get; set; }
        public Durasi durasi { get; set; }

        public class Durasi
        {
            public int min { get; set; }
            public int max { get; set; }
        }

        public static RuntimeConfig Load(string path = "sewa/rental_config.json")
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<RuntimeConfig>(json) ?? new();
        }
    }
}
