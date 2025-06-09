using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using config;
using controller;
using Tubes_KPL.LihatKendaraan;
using System.Net.Http;
using System.Net.Http.Json;


namespace TUBESGUI
{
    public partial class SewaKendaraan : Form
    {
        private enum VehicleState { Available = 0, Rented = 1 } 

        private readonly HttpClient _httpClient;
        private readonly SistemSewa<VehicleDto> _sistemSewa;
        private readonly RuntimeConfig _runtimeConfig;
        private readonly string _baseUrl;
        private const string BaseApiUrl = "https://localhost:44376"; // Konstanta untuk URL API

        public SewaKendaraan()
        {
            InitializeComponent();

            // Inisialisasi objek utama yang digunakan dalam proses penyewaan kendaraan
            _runtimeConfig = RuntimeConfig.Load();
            _httpClient = new HttpClient();
            _baseUrl = BaseApiUrl;
            _sistemSewa = new SistemSewa<VehicleDto>(new List<VehicleDto>(), _runtimeConfig, _httpClient, _baseUrl);

            LoadDataFromAPI(); // Ambil data langsung dari API, bukan dari console
        }

        // Mengambil data kendaraan dari API dan mengisi DataGridView  
        private async void LoadDataFromAPI()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/vehicles");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mengambil data kendaraan dari API!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var kendaraanList = await response.Content.ReadFromJsonAsync<List<VehicleDto>>();

                DataKendaraan.DataSource = kendaraanList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mengambil data: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCekHarga_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput(true))
            {
                return;
            }

            int idKendaraan = int.Parse(textBoxId.Text);
            int lamaSewa = int.Parse(textBoxDurasi.Text);

            var kendaraanTerpilih = AmbilKendaraanTerpilih(idKendaraan);

            if (kendaraanTerpilih == null)
            {
                return;
            }

            string tipeKendaraan = kendaraanTerpilih.Type.ToLower();

            // Jika harga tidak tersedia di VehicleDto, gunakan harga yang diambil dari konfigurasi  
            if (!_runtimeConfig.harga_sewa.ContainsKey(tipeKendaraan))
            {
                MessageBox.Show("Harga untuk tipe kendaraan ini tidak tersedia!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int hargaPerHari = _runtimeConfig.harga_sewa[tipeKendaraan];
            int totalHarga = hargaPerHari * lamaSewa;
            lblHarga.Text = $"Rp {totalHarga:N0}";
        }

        private async void ButtonSewa_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput(false))
            {
                return;
            }  

            int idKendaraan = int.Parse(textBoxId.Text);
            string namaPeminjam = textBoxNama.Text;

            var kendaraanTerpilih = AmbilKendaraanTerpilih(idKendaraan);

            if (kendaraanTerpilih == null)
            {
                return;
            }

            // Kirim permintaan penyewaan kendaraan ke sistem
            bool success = await _sistemSewa.PinjamKendaraan(idKendaraan, namaPeminjam);

            MessageBox.Show(success ? "Penyewaan berhasil!" : "Gagal melakukan penyewaan!","Informasi",MessageBoxButtons.OK,success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        // Validasi input sebelum memproses sewa
        private bool ValidasiInput(bool butuhDurasi)
        {
            if (string.IsNullOrWhiteSpace(textBoxId.Text) ||
                string.IsNullOrWhiteSpace(textBoxNama.Text) ||
                (butuhDurasi && string.IsNullOrWhiteSpace(textBoxDurasi.Text)))
            {
                MessageBox.Show("Pastikan semua data terisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(textBoxId.Text, out _) || (butuhDurasi && !int.TryParse(textBoxDurasi.Text, out _)))
            {
                MessageBox.Show("ID kendaraan dan durasi harus berupa angka!", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Method untuk hindari duplikasi
        private VehicleDto AmbilKendaraanTerpilih(int id) 
        {
            var kendaraanList = DataKendaraan.DataSource as List<VehicleDto>;
            if (kendaraanList == null)
            {
                return null;
            }

            var kendaraan = kendaraanList.FirstOrDefault(k => k.Id == id);
            if (kendaraan == null)
            {
                MessageBox.Show("ID kendaraan tidak ditemukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if ((VehicleState)kendaraan.State != VehicleState.Available)
            {
                MessageBox.Show("Kendaraan ini sedang disewa dan tidak tersedia!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return kendaraan;
        }
        private void DataKendaraan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
