using System;
using System.Net.Http;
using System.Windows.Forms;
using Tubes_KPL.Services;
using Tubes_API.Models;
using Test_API_tubes.Models;

namespace TUBESGUI
{
    public partial class ReturnKendaraan : Form
    {
        private readonly PeminjamanService _peminjamanService;

        public ReturnKendaraan()
        {
            InitializeComponent();

            _peminjamanService = InitializeService();
        }

        // Inisialisasi service dengan konfigurasi HttpClient dan Base URL.
        private PeminjamanService InitializeService()
        {
            var httpClient = new HttpClient();
            const string baseUrl = "http://localhost:5176";
            return new PeminjamanService(httpClient, baseUrl);
        }

        // Event handler untuk tombol pengembalian kendaraan.
        private async void btnReturn_Click(object sender, EventArgs e)
        {
            // Validasi input Vehicle ID
            if (!TryGetVehicleId(out int vehicleId)) return;

            // Nonaktifkan tombol selama proses pengembalian
            SetUiProcessingState(true);

            try
            {
                // Ambil data kendaraan berdasarkan ID
                var vehicle = await _peminjamanService.GetVehicleAsync(vehicleId);

                // Cek apakah kendaraan valid untuk dikembalikan
                if (!ValidateVehicleForReturn(vehicle)) return;

                // Lakukan proses pengembalian
                await ProcessReturn(vehicleId);
            }
            catch (HttpRequestException)
            {
                DisplayStatus("Tidak dapat terhubung ke server. Periksa koneksi Anda.");
            }
            catch (Exception)
            {
                DisplayStatus("Terjadi kesalahan yang tidak terduga. Coba lagi nanti.");
            }
            finally
            {
                SetUiProcessingState(false);
            }
        }

        // Mengambil dan memvalidasi input ID kendaraan.
        private bool TryGetVehicleId(out int vehicleId)
        {
            if (!int.TryParse(txtVehicleId.Text.Trim(), out vehicleId))
            {
                DisplayStatus("ID kendaraan harus berupa angka.");
                return false;
            }

            if (vehicleId <= 0)
            {
                DisplayStatus("ID kendaraan tidak valid.");
                return false;
            }

            return true;
        }

        // Validasi apakah kendaraan dalam status yang bisa dikembalikan.
        private bool ValidateVehicleForReturn(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                DisplayStatus("Kendaraan tidak ditemukan.");
                return false;
            }

            if (vehicle.State != VehicleState.Rented)
            {
                DisplayStatus("Kendaraan tidak sedang dipinjam.");
                return false;
            }

            return true;
        }

        // Menjalankan proses pengembalian kendaraan ke sistem.
        private async Task ProcessReturn(int vehicleId)
        {
            var isReturned = await _peminjamanService.HandleAction("return", vehicleId);
            DisplayStatus(isReturned ? "Pengembalian berhasil." : "Pengembalian gagal. Periksa status kendaraan.");
        }

        // Mengatur status UI selama proses berjalan.
        private void SetUiProcessingState(bool isProcessing)
        {
            btnReturn.Enabled = !isProcessing;

            if (isProcessing)
            {
                lblStatus.Text = "Memproses pengembalian...";
            }
        }

        // Menampilkan pesan status kepada pengguna.
        private void DisplayStatus(string message)
        {
            lblStatus.Text = message;
        }
    }
}