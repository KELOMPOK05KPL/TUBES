using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_API_tubes.Models;

namespace TUBESGUI
{
    public partial class VehicleForm : Form
    {
        // Properti ini untuk menampung vehicle yang sedang diedit
        private Vehicle _vehicleToEdit;

        // Properti publik untuk dibaca oleh MainForm saat membuat data baru
        public string VehicleType { get; private set; }
        public string VehicleBrand { get; private set; }
        public string VehicleModel { get; private set; }
        public VehicleState VehicleState { get; private set; }

        // Constructor ini akan dipanggil saat membuat vehicle baru (mode CREATE)
        public VehicleForm()
        {
            InitializeComponent();
            InitializeComboBoxes();
            this.Text = "Create New Vehicle";
        }

        // Constructor ini akan dipanggil saat mengedit vehicle yang ada (mode EDIT)
        public VehicleForm(Vehicle vehicleToEdit)
        {
            InitializeComponent();
            InitializeComboBoxes();
            _vehicleToEdit = vehicleToEdit; // Simpan vehicle yang akan diedit
            this.Text = "Edit Vehicle";
            LoadVehicleData();
        }

        private void InitializeComboBoxes()
        {
            // Isi ComboBox Type dengan daftar string manual
            cmbType.Items.Add("Mobil");
            cmbType.Items.Add("Motor");
            cmbType.Items.Add("Truk");
            cmbType.Items.Add("Bus");
            cmbType.SelectedIndex = 0; // Pilih "Mobil" sebagai default

            // Isi ComboBox Status dari Enum VehicleState Anda
            cmbStatus.DataSource = Enum.GetValues(typeof(VehicleState));
        }

        private void LoadVehicleData()
        {
            // Jika dalam mode edit, tampilkan data vehicle ke setiap kontrol
            if (_vehicleToEdit != null)
            {
                cmbType.SelectedItem = _vehicleToEdit.Type;
                txtBrand.Text = _vehicleToEdit.Brand;
                txtModel.Text = _vehicleToEdit.Model;
                cmbStatus.SelectedItem = _vehicleToEdit.State;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validasi sederhana, pastikan Brand dan Model tidak kosong
            if (string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Brand and Model cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Jika dalam mode EDIT, langsung ubah properti objek yang sudah ada
            if (_vehicleToEdit != null)
            {
                _vehicleToEdit.Type = cmbType.SelectedItem.ToString();
                _vehicleToEdit.Brand = txtBrand.Text;
                _vehicleToEdit.Model = txtModel.Text;
                _vehicleToEdit.State = (VehicleState)cmbStatus.SelectedItem;
            }
            // Jika dalam mode CREATE, isi properti publik agar bisa dibaca MainForm
            else
            {
                VehicleType = cmbType.SelectedItem.ToString();
                VehicleBrand = txtBrand.Text;
                VehicleModel = txtModel.Text;
                VehicleState = (VehicleState)cmbStatus.SelectedItem;
            }

            // Atur DialogResult ke OK, menandakan penyimpanan sukses
            this.DialogResult = DialogResult.OK;
            this.Close(); // Tutup form
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Atur DialogResult ke Cancel, menandakan pembatalan
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Tutup form
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
