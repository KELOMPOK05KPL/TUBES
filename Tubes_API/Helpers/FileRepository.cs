using System.Text.Json;

namespace Tubes_API.Helpers
{
    public static class FileRepository<T>
    {
        public static List<T> Load(string filePath)
        {
            if (!File.Exists(filePath)) return new();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new();
        }

        public static void Save(string filePath, List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
