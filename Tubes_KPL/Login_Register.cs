using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
    private readonly HttpClient httpClient;
    private readonly string apiBaseUrl = "https://localhost:44376/api/User"; 
    private string currentUsername = "";

    public Login_Register()
    {
        currentState = State.Idle;
        httpClient = new HttpClient();
        Console.WriteLine("Sistem dalam keadaan Idle");
    }

    public async void Trigger(string action, string username = "", string password = "")
    {
        switch (currentState)
        {
            case State.Idle:
                if (action.ToLower() == "register")
                {
                    currentState = State.Registering;
                    await Register(username, password);
                }
                else if (action.ToLower() == "login")
                {
                    currentState = State.LoggingIn;
                    await Login(username, password);
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

    private async Task Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username dan password tidak boleh kosong");
            currentState = State.Failed;
            return;
        }

        var newUser = new User
        {
            Username = username,
            Password = password,
            Role = "User"
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{apiBaseUrl}/register", newUser);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Registrasi berhasil!");
                currentState = State.Idle;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Registrasi gagal: " + error);
                currentState = State.Failed;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal menghubungi API: " + ex.Message);
            currentState = State.Failed;
        }
    }

    private async Task Login(string username, string password)
    {
        try
        {
            var response = await httpClient.GetAsync($"{apiBaseUrl}/login?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Login berhasil! Selamat datang " + username);
                currentUsername = username;
                currentState = State.Authenticated;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Login gagal: " + error);
                currentState = State.Failed;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal menghubungi API: " + ex.Message);
            currentState = State.Failed;
        }
    }

    public async Task<bool> TriggerLoginAsync(string username, string password)
    {
        if (currentState != State.Idle)
        {
            Console.WriteLine("Tidak bisa login sekarang. Sistem sedang dalam keadaan: " + currentState);
            return false;
        }

        currentState = State.LoggingIn;
        try
        {
            var response = await httpClient.GetAsync($"{apiBaseUrl}/login?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Login berhasil! Selamat datang " + username);
                currentUsername = username;
                currentState = State.Authenticated;
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Login gagal: " + error);
                currentState = State.Failed;
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal menghubungi API: " + ex.Message);
            currentState = State.Failed;
            return false;
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

    public async void ListUsers()
    {
        try
        {
            var users = await httpClient.GetFromJsonAsync<List<User>>($"{apiBaseUrl}/all");
            Console.WriteLine("Daftar Pengguna:");
            foreach (var user in users)
            {
                Console.WriteLine($"- {user.Username} ({user.Role})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal memuat daftar user dari API: " + ex.Message);
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
