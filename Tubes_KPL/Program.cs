using System;
using Tubes_KPL.fiturSewa;
using Tubes_KPL.Login_Register;

class Program
{
    static async Task Main(string[] args)
    {
        var auth = new Login_Register();
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Selamat Datang ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Lihat semua user");
            Console.WriteLine("4. Status login");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Username: ");
                    var usernameLogin = Console.ReadLine();
                    Console.Write("Password: ");
                    var passwordLogin = Console.ReadLine();

                    auth.Trigger("login", usernameLogin, passwordLogin);
                    if (auth.GetState() == Login_Register.State.Authenticated)
                    {
                        await MenuSetelahLogin(auth);
                    }
                    else
                    {
                        Console.WriteLine("Login gagal. Tekan Enter untuk kembali...");
                        Console.ReadLine();
                    }
                    break;

                case "2":
                    Console.Write("Username: ");
                    var usernameReg = Console.ReadLine();
                    Console.Write("Password: ");
                    var passwordReg = Console.ReadLine();

                    auth.Trigger("register", usernameReg, passwordReg);
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;

                case "3":
                    auth.ListUsers();
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;

                case "4":
                    Console.WriteLine("Status saat ini: " + auth.GetState());
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;

                case "5":
                    isRunning = false;
                    Console.WriteLine("Program selesai.");
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static async Task MenuSetelahLogin(Login_Register auth)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine("=== Menu Setelah Login ===");
            Console.WriteLine("1. Sewa Kendaraan");
            Console.WriteLine("2. Logout");
            Console.Write("Pilih: ");
            var pilih = Console.ReadLine();

            switch (pilih)
            {
                case "1":
                    var penyewaan = new Penyewaan();
                    await penyewaan.TampilkanMenu();
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;

                case "2":
                    auth.Logout();
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    Console.WriteLine("Tekan Enter untuk kembali...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
