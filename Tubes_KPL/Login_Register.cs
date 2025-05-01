using System;
using System.Collections.Generic;

public class LoginMachine
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
    private Dictionary<string, string> users;

    public LoginMachine()
    {
        currentState = State.Idle;
        users = new Dictionary<string, string>();
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
                Console.WriteLine("Sudah login sebagai " + username);
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

        if (users.ContainsKey(username))
        {
            Console.WriteLine("Username sudah terdaftar");
            currentState = State.Failed;
            return;
        }

        users.Add(username, password);
        Console.WriteLine("Registrasi berhasil!");
        currentState = State.Idle;
    }

    private void Login(string username, string password)
    {
        if (users.ContainsKey(username) && users[username] == password)
        {
            Console.WriteLine("Login berhasil! Selamat datang " + username);
            currentState = State.Authenticated;
        }
        else
        {
            Console.WriteLine("Login gagal. Username atau password salah");
            currentState = State.Failed;
        }
    }

    public State GetState()
    {
        return currentState;
    }

    public void Logout()
    {
        if (currentState == State.Authenticated)
        {
            Console.WriteLine("Logout berhasil.");
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
            Console.WriteLine("- " + user.Key);
        }
    }
}
