
using controller;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tubes_KPL.LihatKendaraan;
using Tubes_KPL.Pengembalian;
using Tubes_KPL.Services;

namespace Tubes_KPL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var baseUrl = "https://localhost:44376";
            var _riwayatFilePath = "Data/RiwayatPeminjaman.json";
            var kendaraanViewer = new KendaraanViewer(httpClient, baseUrl);
            var peminjamanService = new PeminjamanService(httpClient, baseUrl);
            var vehicleManagement = new VehicleManagementService(httpClient, baseUrl);
            var loginRegister = new Login_Register();
            var pengembalianKendaraan = new PengembalianKendaraan(httpClient, baseUrl, _riwayatFilePath);

            string loggedInUser = "";
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEM LOGIN ===");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Keluar");
                Console.Write("Pilih opsi (1-3): ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Masukkan username baru: ");
                        var regUser = Console.ReadLine();
                        Console.Write("Masukkan password: ");
                        var regPass = Console.ReadLine();
                        loginRegister.Trigger("register", regUser, regPass);
                        break;

                    case "2":
                        Console.Write("Masukkan username: ");
                        var loginUser = Console.ReadLine();
                        Console.Write("Masukkan password: ");
                        var loginPass = Console.ReadLine();

                        if (await loginRegister.TriggerLoginAsync(loginUser, loginPass))
                        {
                            loggedInUser = loginUser;
                            isLoggedIn = true;
                            Console.Clear();
                            Console.WriteLine($"Login berhasil! Selamat datang, {loggedInUser}.");
                        }
                        else
                        {
                            Console.WriteLine("Login gagal. Silakan coba lagi.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Keluar dari aplikasi.");
                        return;

                    default:
                        Console.WriteLine("Opsi tidak valid. Pilih antara 1-3.");
                        break;
                }

                await Task.Delay(1000);
            }


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
                Console.WriteLine("5. Manajemen Kendaraan (Admin)");
                Console.WriteLine("6. Keluar");
                Console.Write("Pilihan Anda: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        await kendaraanViewer.TampilkanSemuaKendaraan();
                        break;

                    case "2":
                        var penyewaan = new Penyewaan();
                        await penyewaan.TampilkanMenu();
                        break;
                        //Console.Write("Masukkan ID Kendaraan yang ingin dipinjam: ");
                        //if (int.TryParse(Console.ReadLine(), out int id))
                        //{
                        //    await kendaraanViewer.TampilkanDetailKendaraan(id);
                        //    Console.Write("Masukkan nama Anda: ");
                        //    var namaPeminjam = Console.ReadLine();

                        //    Console.Write($"Apakah Anda yakin ingin meminjam kendaraan ini? (y/n): ");
                        //    if (Console.ReadLine().ToLower() == "y")
                        //    {
                        //        var success = await peminjamanService.PinjamKendaraan(id, namaPeminjam);
                        //        if (success)
                        //        {
                        //            Console.Clear();
                        //            Console.WriteLine("Silahkan ambil kendaraan!");
                        //        }
                        //    }
                        //}
                        break;

                    case "3":
                        await peminjamanService.TampilkanRiwayatPeminjaman();
                        break;

                    case "4":
                        await pengembalianKendaraan.TampilkanMenuPengembalian(); // Memanggil fitur pengembalian kendaraan
                        break;
                    case "5":
                        await vehicleManagement.HandleVehicleManagement();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                await Task.Delay(1000);
            }
        }
    }
}