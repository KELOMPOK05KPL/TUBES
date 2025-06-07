using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;
using Test_API_tubes.Models;

namespace TUBESGUI
{
    public partial class LihatSemuaKendaraan : Form
    {
        private readonly string apiUrl = "http://localhost:5176/api/vehicles";
        private HttpClient client = new HttpClient();

        public LihatSemuaKendaraan()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadVehicles();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView appearance
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;

            // Add columns
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add("Brand", "Brand");
            dataGridView1.Columns.Add("Model", "Model");
            dataGridView1.Columns.Add("Status", "Status");

            // Style header
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 35;
        }

        private async void LoadVehicles()
        {
            try
            {
                loadingPanel.Visible = true;

                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(content);

                // Clear existing data
                dataGridView1.Rows.Clear();

                // Add data to DataGridView
                foreach (var vehicle in vehicles)
                {
                    dataGridView1.Rows.Add(
                        vehicle.Id,
                        vehicle.Type,
                        vehicle.Brand,
                        vehicle.Model,
                        vehicle.State
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingPanel.Visible = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadVehicles();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}