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
        private readonly string apiUrl = "http://localhost:5176/api/vehicles";
        private HttpClient client = new HttpClient();
        private List<Vehicle> vehicleList = new List<Vehicle>();
        private int nextId = 1; // Untuk auto-increment ID

        public AdminDashboard()
        {
            InitializeComponent();
        }

        private async void AdminDashboard_LoadAsync(object sender, EventArgs e)
        {
            // Mengatur tampilan kolom DataGridView
            SetupDataGridView();
            await LoadVehicles();
        }

        private void SetupDataGridView()
        {
            VehicleData.AutoGenerateColumns = false; // Kita akan definisikan kolom secara manual

            // Menambahkan kolom sesuai urutan yang diinginkan
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 40 });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Type", DataPropertyName = "Type" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Brand", DataPropertyName = "Brand" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Model", DataPropertyName = "Model" });
            VehicleData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", DataPropertyName = "State" });

            // Menambahkan kolom Tombol Aksi (Edit & Delete)
            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Text = "✏️", // Emoji atau teks "Edit"
                UseColumnTextForButtonValue = true,
                Name = "Edit",
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            editButton.DefaultCellStyle.BackColor = Color.LightBlue;
            VehicleData.Columns.Add(editButton);

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
            {
                Text = "🗑️", // Emoji atau teks "Delete"
                UseColumnTextForButtonValue = true,
                Name = "Delete",
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.DefaultCellStyle.BackColor = Color.LightCoral;
            VehicleData.Columns.Add(deleteButton);
        }

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


        private void RefreshGrid()
        {
            VehicleData.DataSource = null;
            // Urutkan berdasarkan ID agar lebih rapi
            VehicleData.DataSource = vehicleList.OrderBy(v => v.Id).ToList();
        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void VehicleData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int vehicleId = (int)VehicleData.Rows[e.RowIndex].Cells[0].Value;
            Vehicle selectedVehicle = vehicleList.FirstOrDefault(v => v.Id == vehicleId);

            if (selectedVehicle == null) return;

            if (VehicleData.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Teruskan vehicle yang dipilih ke form untuk diedit
                using (VehicleForm form = new VehicleForm(selectedVehicle))
                {
                    // Tampilkan form, dan jika user menekan "Save" (DialogResult.OK)
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Serialisasi objek 'selectedVehicle' yang sudah diupdate oleh form
                            var json = JsonConvert.SerializeObject(selectedVehicle);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            // Kirim request PUT ke API
                            var response = await client.PutAsync($"{apiUrl}/{selectedVehicle.Id}", content);
                            response.EnsureSuccessStatusCode(); // Pastikan request sukses

                            // Muat ulang semua data dari API untuk menampilkan perubahan
                            await LoadVehicles();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
