
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tubes_KPL.LihatKendaraan;
using Tubes_KPL.Services;

namespace Tubes_KPL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var baseUrl = "http://localhost:5176";

            var kendaraanViewer = new KendaraanViewer(httpClient, baseUrl);
            var peminjamanService = new PeminjamanService(httpClient, baseUrl);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nAplikasi Manajemen Kendaraan");
                Console.WriteLine("============================");
                Console.WriteLine("Menu Utama:");
                Console.WriteLine("1. Lihat Semua Kendaraan");
                Console.WriteLine("2. Pinjam Kendaraan");
                Console.WriteLine("3. Lihat Riwayat Peminjaman");
                Console.WriteLine("4. Kembalikan Kendaraan");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilihan Anda: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        await kendaraanViewer.TampilkanSemuaKendaraan();
                        break;
                    case "2":
                        Console.Write("Masukkan ID Kendaraan yang ingin dipinjam: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            await kendaraanViewer.TampilkanDetailKendaraan(id);
                            Console.Write("Masukkan nama Anda: ");
                            var namaPeminjam = Console.ReadLine();

                            Console.Write($"Apakah Anda yakin ingin meminjam kendaraan ini? (y/n): ");
                            if (Console.ReadLine().ToLower() == "y")
                            {
                                var success = await peminjamanService.PinjamKendaraan(id, namaPeminjam);
                                if (success)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Silahkan ambil kendaraan!");
                                }
                            }
                        }
                        break;
                    case "3":
                        await peminjamanService.TampilkanRiwayatPeminjaman();
                        break;
                    case "4":
                        Console.Write("Masukkan ID Kendaraan yang ingin dikembalikan: ");
                        if (int.TryParse(Console.ReadLine(), out int returnId))
                        {
                            var success = await peminjamanService.KembalikanKendaraan(returnId);
                            if (success)
                            {
                                Console.WriteLine("Kendaraan telah dikembalikan!");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }
            }

            Console.WriteLine("Terima kasih telah menggunakan aplikasi!");
        }
    }
}
