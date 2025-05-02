using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sewa_kendaraan.fiturSewa.config
{
    public class runtimeconfig
    {
        public Dictionary<string, int> HargaSewa { get; set; }
        public int MaxDuration { get; set; }
        public int DefaultRentalDuration { get; set; }
        public string Currency { get; set; }

        public int GetHargaSewa(string tipe) => HargaSewa.ContainsKey(tipe) ? HargaSewa[tipe] : 0;

        public static runtimeconfig Load()
        {
            var json = File.ReadAllText("config.json");
            return JsonSerializer.Deserialize<runtimeconfig>(json)!;
        }
    }
}
