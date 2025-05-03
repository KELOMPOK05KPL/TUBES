using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

 namespace Tubes_KPL.Login_Register
{
    public class Login_Register
    {
        public enum State
        {
            Idle,
            Registering,
            LoggingIn,
            Authenticated,
            Failed
        }

        private State currentState;
        private List<User> users;
        private readonly string filePath = "users.json";
        private string currentUsername = "";

        public Login_Register()
        {
            currentState = State.Idle;
            LoadUsersFromFile();
            Console.WriteLine("Sistem dalam keadaan Idle");
        }

        public void Trigger(string action, string username = "", string password = "")
        {
            switch (currentState)
            {
                case State.Idle:
                    if (action.ToLower() == "register")
                    {
                        currentState = State.Registering;
                        Register(username, password);
                    }
                    else if (action.ToLower() == "login")
                    {
                        currentState = State.LoggingIn;
                        Login(username, password);
                    }
                    else
                    {
                        currentState = State.Failed;
                        Console.WriteLine("Aksi tidak valid pada keadaan Idle");
                    }
                    break;

                case State.Registering:
                case State.LoggingIn:
                    Console.WriteLine("Silakan kembali ke Idle untuk melakukan aksi baru");
                    break;

                case State.Authenticated:
                    Console.WriteLine("Sudah login sebagai " + currentUsername);
                    break;

                case State.Failed:
                    Console.WriteLine("Terjadi kesalahan. Kembali ke Idle.");
                    currentState = State.Idle;
                    break;
            }
        }

        private void Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username dan password tidak boleh kosong");
                currentState = State.Failed;
                return;
            }

            if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Username sudah terdaftar");
                currentState = State.Failed;
                return;
            }

            int newId = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;

            var newUser = new User
            {
                Id = newId,
                Username = username,
                Password = password,
                Role = "User"
            };

            users.Add(newUser);
            SaveUsersToFile();

            Console.WriteLine("Registrasi berhasil!");
            currentState = State.Idle;
        }

        private void Login(string username, string password)
        {
            var user = users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (user != null)
            {
                Console.WriteLine("Login berhasil! Selamat datang " + username);
                currentUsername = username;
                currentState = State.Authenticated;
            }
            else
            {
                Console.WriteLine("Login gagal. Username atau password salah");
                currentState = State.Failed;
            }
        }

        private void SaveUsersToFile()
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void LoadUsersFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
            }
        }

        public void Logout()
        {
            if (currentState == State.Authenticated)
            {
                Console.WriteLine("Logout berhasil.");
                currentUsername = "";
                currentState = State.Idle;
            }
            else
            {
                Console.WriteLine("Belum login.");
            }
        }

        public void ListUsers()
        {
            Console.WriteLine("Daftar Pengguna:");
            foreach (var user in users)
            {
                Console.WriteLine($"- {user.Username} ({user.Role})");
            }
        }

        public State GetState() => currentState;
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

}

