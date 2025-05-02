using sewa_kendaraan.fiturSewa.config;
using sewa_kendaraan.fiturSewa.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sewa_kendaraan.fiturSewa.fitur
{
    public class SewaKendaraan<T> where T : class
    {
        private List<T> DaftarKendaraan;
        private runtimeconfig Config;
        private string TipeKendaraan;

        public SewaKendaraan(List<T> kendaraan, runtimeconfig config, string tipe)
        {
            DaftarKendaraan = kendaraan;
            Config = config;
            TipeKendaraan = tipe;
        }

        public void Jalankan()
        {
            Console.WriteLine("\nDaftar Kendaraan Tersedia:");
            for (int i = 0; i < DaftarKendaraan.Count; i++)
            {
                dynamic k = DaftarKendaraan[i];
                Console.WriteLine($"{i + 1}. {k.Brand} {k.Model}");
            }

            Console.Write("Pilih nomor kendaraan: ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 1 || pilih > DaftarKendaraan.Count)
            {
                Console.WriteLine("Pilihan tidak valid.");
                return;
            }

            dynamic kendaraan = DaftarKendaraan[pilih - 1];

            Console.Write($"Lama sewa (1–{Config.MaxDuration} hari): ");
            if (!int.TryParse(Console.ReadLine(), out int hari) || hari < 1 || hari > Config.MaxDuration)
            {
                Console.WriteLine("Durasi tidak valid.");
                return;
            }

            int harga = Config.GetHargaSewa(TipeKendaraan);
            int total = harga * hari;

            Console.WriteLine($"\n{kendaraan.Brand} {kendaraan.Model} akan disewa selama {hari} hari.");
            Console.WriteLine($"Harga per hari: {Config.Currency} {harga:N0}");
            Console.WriteLine($"Total: {Config.Currency} {total:N0}");

            Console.Write("Konfirmasi peminjaman? (y/n): ");
            var konfirmasi = Console.ReadLine();
            if (konfirmasi?.ToLower() == "y")
            {
                Console.WriteLine(" Peminjaman berhasil.");
            }
            else
            {
                Console.WriteLine(" Peminjaman dibatalkan.");
            }
        }
    }
}
