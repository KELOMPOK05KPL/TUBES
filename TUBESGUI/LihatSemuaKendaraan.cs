using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using Test_API_tubes.Models;

namespace TUBESGUI
{
    public partial class LihatSemuaKendaraan : Form
    {
        private const string ApiUrl = "http://localhost:5176/api/vehicles";
        private readonly HttpClient _httpClient = new HttpClient();

        public LihatSemuaKendaraan()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadVehicles();
        }

        // Konfigurasi DataGridView
        private void ConfigureDataGridView()
        {
            // Properti DataGridView
            vehiclesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            vehiclesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            vehiclesDataGridView.MultiSelect = false;
            vehiclesDataGridView.ReadOnly = true;
            vehiclesDataGridView.AllowUserToAddRows = false;
            vehiclesDataGridView.RowHeadersVisible = false;

            // Style header
            vehiclesDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font("Century Gothic", 9, FontStyle.Bold);
            vehiclesDataGridView.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            vehiclesDataGridView.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            vehiclesDataGridView.ColumnHeadersHeight = 35;

            // Menambahkan kolom
            vehiclesDataGridView.Columns.Add("Id", "ID");
            vehiclesDataGridView.Columns.Add("Type", "Type");
            vehiclesDataGridView.Columns.Add("Brand", "Brand");
            vehiclesDataGridView.Columns.Add("Model", "Model");
            vehiclesDataGridView.Columns.Add("Status", "Status");
        }

        // Menampilkan data kendaraan
        private async void LoadVehicles()
        {
            try
            {
                loadingPanel.Visible = true;
                var vehicles = await FetchVehicles();
                InsertDataGridView(vehicles);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading vehicles: {ex.Message}");
            }
            finally
            {
                loadingPanel.Visible = false;
            }
        }

        // Mengambil data kendaraan dari API
        private async Task<List<Vehicle>> FetchVehicles()
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Vehicle>>(content);
        }

        // Mengisi DataGridView dengan data kendaraan
        private void InsertDataGridView(List<Vehicle> vehicles)
        {
            vehiclesDataGridView.Rows.Clear();

            foreach (var vehicle in vehicles)
            {
                vehiclesDataGridView.Rows.Add(
                    vehicle.Id,
                    vehicle.Type,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.State
                );
            }
        }

        // Menampilkan pesan error
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Refresh daftar kendaraan
        private void OnRefreshButtonClick(object sender, EventArgs e)
        {
            LoadVehicles();
        }
    }
}