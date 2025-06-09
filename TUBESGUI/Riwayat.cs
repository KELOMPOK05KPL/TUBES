using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Riwayat : Form
    {
        private List<RiwayatPeminjaman> riwayatList = new();

        public Riwayat()
        {
            InitializeComponent();
            Load += Riwayat_Load;
        }

        private async void Riwayat_Load(object sender, EventArgs e)
        {
            await LoadRiwayatFromFileAsync();
        }

        private async Task LoadRiwayatFromFileAsync()
        {
            try
            {
                string filePath = "Data/riwayatPeminjaman.json"; // Pastikan path dan nama file benar
                if (!File.Exists(filePath))
                {
                    lblStatus.Text = "Status: File riwayat tidak ditemukan.";
                    dgvRiwayat.DataSource = null;
                    return;
                }

                var json = await File.ReadAllTextAsync(filePath);
                riwayatList = JsonSerializer.Deserialize<List<RiwayatPeminjaman>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<RiwayatPeminjaman>();

                lblStatus.Text = "Status: Data berhasil dimuat.";
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Status: Error - {ex.Message}";
            }
        }

        private void RefreshDataGrid()
        {
            dgvRiwayat.DataSource = null;
            dgvRiwayat.DataSource = riwayatList;
        }
    }

    // Model sesuai response/file Anda
    public class RiwayatPeminjaman
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Peminjam { get; set; } = string.Empty;
        public DateTime TanggalPinjam { get; set; }
        public DateTime? TanggalKembali { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}