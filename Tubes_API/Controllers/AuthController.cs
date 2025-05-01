using Test_API_tubes.Models;

public static class DataStore
{
    private static readonly string filePath = "D:\\My Code\\GUI C#\\TUBES\\Tubes_KPL\\Tubes_API\\Repositories\\user.json";

    public static List<User> LoadUsers()
    {
        if (!File.Exists(filePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(filePath);
        return System.Text.Json.JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public static void SaveUsers(List<User> users)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(users);
        File.WriteAllText(filePath, json);
    }

    public static void AddUser(User user)
    {
        var users = LoadUsers();
        users.Add(user);
        SaveUsers(users);
    }

    public static void DeleteUser(int id)
    {
        var users = LoadUsers();
        var userToDelete = users.FirstOrDefault(u => u.Id == id);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            SaveUsers(users);
        }
    }
}

