using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tubes_KPL.Services
{

    // Kelas abstrak dasar untuk layanan CRUD yang menggunakan HttpClient.
    public abstract class BaseService<T>
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;
        protected readonly string _endpoint;

        // Konstruktor untuk inisialisasi HttpClient dan endpoint.
        protected BaseService(HttpClient httpClient, string baseUrl, string endpoint)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');
            _endpoint = endpoint;
        }

        // Mengambil semua data T dari endpoint.
        protected async Task<List<T>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_endpoint}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<T>>() ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<T>();
            }
        }

        // Mengambil data berdasarkan ID.
        protected async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_endpoint}/{id}");
                return response.IsSuccessStatusCode
                    ? await response.Content.ReadFromJsonAsync<T>()
                    : default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default;
            }
        }

        // Membuat data baru.
        protected async Task<bool> CreateAsync(T data)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/api/{_endpoint}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Memperbarui data berdasarkan ID.
        protected async Task<bool> UpdateAsync(int id, T data)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/api/{_endpoint}/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Menghapus data berdasarkan ID.
        protected async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/{_endpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
