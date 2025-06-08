using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class LoginRegister
{
    public enum AuthState
    {
        Idle,
        Registering,
        LoggingIn,
        Authenticated,
        Failed
    }

    private AuthState _currentState;
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "https://localhost:44376/api/User";
    private string _currentUsername = string.Empty;

    public LoginRegister()
    {
        _currentState = AuthState.Idle;
        _httpClient = new HttpClient();
        Console.WriteLine("Sistem dalam keadaan Idle");
    }

    public LoginRegister(HttpClient injectedClient)
    {
        _currentState = AuthState.Idle;
        _httpClient = injectedClient;
        Console.WriteLine("Sistem dalam keadaan Idle (Injected HttpClient)");
    }

    public async Task TriggerAsync(string action, string username = "", string password = "")
    {
        switch (_currentState)
        {
            case AuthState.Idle:
                if (action.Equals("register", StringComparison.OrdinalIgnoreCase))
                {
                    _currentState = AuthState.Registering;
                    await RegisterAsync(username, password);
                }
                else if (action.Equals("login", StringComparison.OrdinalIgnoreCase))
                {
                    _currentState = AuthState.LoggingIn;
                    await LoginAsync(username, password);
                }
                else
                {
                    _currentState = AuthState.Failed;
                    Console.WriteLine("Aksi tidak valid pada keadaan Idle");
                }
                break;

            case AuthState.Registering:
            case AuthState.LoggingIn:
                Console.WriteLine("Silakan kembali ke Idle untuk melakukan aksi baru");
                break;

            case AuthState.Authenticated:
                Console.WriteLine($"Sudah login sebagai {_currentUsername}");
                break;

            case AuthState.Failed:
                Console.WriteLine("Terjadi kesalahan. Kembali ke Idle.");
                _currentState = AuthState.Idle;
                break;
        }
    }

    public async Task RegisterAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username dan password tidak boleh kosong");
            _currentState = AuthState.Failed;
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
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/register", newUser);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Registrasi berhasil!");
                _currentState = AuthState.Idle;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Registrasi gagal: {error}");
                _currentState = AuthState.Failed;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menghubungi API: {ex.Message}");
            _currentState = AuthState.Failed;
        }
    }

    public async Task LoginAsync(string username, string password)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/login?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                _currentUsername = username;
                _currentState = AuthState.Authenticated;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login gagal: {error}");
                _currentState = AuthState.Failed;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menghubungi API: {ex.Message}");
            _currentState = AuthState.Failed;
        }
    }

    public async Task<bool> TriggerLoginAsync(string username, string password)
    {
        if (_currentState != AuthState.Idle)
        {
            Console.WriteLine($"Tidak bisa login sekarang. Sistem sedang dalam keadaan: {_currentState}");
            return false;
        }

        _currentState = AuthState.LoggingIn;

        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/login?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Login berhasil! Selamat datang {username}");
                _currentUsername = username;
                _currentState = AuthState.Authenticated;
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login gagal: {error}");
                _currentState = AuthState.Failed;
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menghubungi API: {ex.Message}");
            _currentState = AuthState.Failed;
            return false;
        }
    }

    public async Task<bool> TriggerRegisterAsync(string username, string password)
    {
        if (_currentState != AuthState.Idle)
        {
            Console.WriteLine($"Tidak bisa registrasi sekarang. Sistem sedang dalam keadaan: {_currentState}");
            return false;
        }

        _currentState = AuthState.Registering;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Username dan password tidak boleh kosong");
            _currentState = AuthState.Failed;
            return false;
        }

        var newUser = new User
        {
            Username = username,
            Password = password,
            Role = "User"
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/register", newUser);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Registrasi berhasil!");
                _currentState = AuthState.Idle;
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Registrasi gagal: {error}");
                _currentState = AuthState.Failed;
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menghubungi API: {ex.Message}");
            _currentState = AuthState.Failed;
            return false;
        }
    }

    public void Logout()
    {
        if (_currentState == AuthState.Authenticated)
        {
            Console.WriteLine("Logout berhasil.");
            _currentUsername = string.Empty;
            _currentState = AuthState.Idle;
        }
        else
        {
            Console.WriteLine("Belum login.");
        }
    }

    public async Task ListUsersAsync()
    {
        try
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>($"{_apiBaseUrl}/all");

            if (users is null || users.Count == 0)
            {
                Console.WriteLine("Tidak ada pengguna terdaftar.");
                return;
            }

            Console.WriteLine("Daftar Pengguna:");
            foreach (var user in users)
            {
                Console.WriteLine($"- {user.Username} ({user.Role})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal memuat daftar user dari API: {ex.Message}");
        }
    }

    public AuthState GetState() => _currentState;
}

// Model User
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}
