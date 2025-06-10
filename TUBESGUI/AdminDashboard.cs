using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Test_API_tubes.Models;

namespace TUBESGUI
{
    public partial class AdminDashboard : Form
    {
        // URL endpoint API kendaraan
        private readonly string apiUrl = "http://localhost:5176/api/vehicles";

        // HTTP client untuk request API
        private HttpClient client = new HttpClient();

        // List kendaraan untuk ditampilkan
        private List<Vehicle> vehicleList = new List<Vehicle>();

        public AdminDashboard()
        {
            InitializeComponent();
        }

        // Event saat form load: setup tabel dan load data kendaraan
        private async void AdminDashboard_LoadAsync(object sender, EventArgs e)
        {
            SetupDataGridView();
            await LoadVehicles();
        }

        // Setup kolom-kolom pada DataGridView kendaraan
        private void SetupDataGridView()
        {
            VehicleData.AutoGenerateColumns = false;

            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 40 });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Type", DataPropertyName = "Type" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Brand", DataPropertyName = "Brand" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Model", DataPropertyName = "Model" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", DataPropertyName = "State" });

            // Tombol Edit
            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Text = "✏️ Edit",
                UseColumnTextForButtonValue = true,
                Name = "Edit",
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            editButton.DefaultCellStyle.BackColor = Color.LightBlue;
            VehicleData.Columns.Add(editButton);

            // Tombol Delete
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
            {
                Text = "🗑️ Delete",
                UseColumnTextForButtonValue = true,
                Name = "Delete",
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.DefaultCellStyle.BackColor = Color.LightCoral;
            VehicleData.Columns.Add(deleteButton);
        }

        // Memuat data kendaraan dari API
        private async Task LoadVehicles()
        {
            try
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                vehicleList = JsonConvert.DeserializeObject<List<Vehicle>>(content) ?? new List<Vehicle>();

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Refresh tampilan data di DataGridView
        private void RefreshGrid()
        {
            VehicleData.DataSource = null;
            VehicleData.DataSource = vehicleList.OrderBy(v => v.Id).ToList();
        }

        // Event klik pada DataGridView (untuk Edit dan Delete)
        private async void VehicleData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int vehicleId = (int)VehicleData.Rows[e.RowIndex].Cells[0].Value;
            Vehicle selectedVehicle = vehicleList.FirstOrDefault(v => v.Id == vehicleId);

            if (selectedVehicle == null) return;

            // Aksi Edit
            if (VehicleData.Columns[e.ColumnIndex].Name == "Edit")
            {
                using (VehicleForm form = new VehicleForm(selectedVehicle))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var json = JsonConvert.SerializeObject(selectedVehicle);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            var response = await client.PutAsync($"{apiUrl}/{selectedVehicle.Id}", content);
                            response.EnsureSuccessStatusCode();

                            await LoadVehicles();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            // Aksi Delete
            else if (VehicleData.Columns[e.ColumnIndex].Name == "Delete")
            {
                DialogResult confirm = MessageBox.Show(
                    $"Are you sure you want to delete {selectedVehicle.Brand} {selectedVehicle.Model}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        var response = await client.DeleteAsync($"{apiUrl}/{selectedVehicle.Id}");
                        response.EnsureSuccessStatusCode();
                        await LoadVehicles();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Event tombol tambah kendaraan
        private async void Btn_Create_Click(object sender, EventArgs e)
        {
            using (VehicleForm form = new VehicleForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newVehicle = new Vehicle(0, form.VehicleType, form.VehicleBrand, form.VehicleModel, form.VehicleState);

                    try
                    {
                        var json = JsonConvert.SerializeObject(newVehicle);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(apiUrl, content);
                        response.EnsureSuccessStatusCode();

                        await LoadVehicles();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Placeholder event untuk label
        private void labelTitle_Click(object sender, EventArgs e)
        {
        }

        // Placeholder event untuk header
        private void Header_Paint(object sender, PaintEventArgs e)
        {
        }

        // Button Logout untuk kembali ke menu login
        private void button1_Click(object sender, EventArgs e)
        {
            var loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        // SearchBar pencarian kendaraan berdasarkan ID
        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            string searchText = SearchBar.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshGrid();
                return;
            }

            if (int.TryParse(searchText, out int searchId))
            {
                var filteredList = vehicleList.Where(v => v.Id == searchId).ToList();

                VehicleData.DataSource = null;
                VehicleData.DataSource = filteredList;
            }

            else
            {
                VehicleData.DataSource = null;
                VehicleData.DataSource = new List<Vehicle>();
            }
        }
    }
}
