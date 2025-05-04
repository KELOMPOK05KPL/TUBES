using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Test_API_tubes.Models;

namespace Test_API_tubes.Services
{
    public class UserService
    {
        private readonly string _filePath;

        public UserService()
        {
            _filePath = "users.json";
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<User> GetAll() => LoadUsers();

        public User? GetById(int id) => LoadUsers().FirstOrDefault(u => u.Id == id);

        public User Add(User newUser)
        {
            var users = LoadUsers();
            newUser.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(newUser);
            SaveUsers(users);
            return newUser;
        }

        public bool Delete(int id)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            users.Remove(user);
            SaveUsers(users);
            return true;
        }

        public bool Update(User updated)
        {
            var users = LoadUsers();
            var index = users.FindIndex(u => u.Id == updated.Id);
            if (index == -1) return false;

            users[index] = updated;
            SaveUsers(users);
            return true;
        }
    }
}
