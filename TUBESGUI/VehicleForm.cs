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
        private Vehicle _vehicleToEdit;

        // Properti untuk mengambil data kendaraan dari form
        public string VehicleType { get; private set; }
        public string VehicleBrand { get; private set; }
        public string VehicleModel { get; private set; }
        public VehicleState VehicleState { get; private set; }

        // Konstruktor untuk membuat kendaraan baru
        public VehicleForm()
        {
            InitializeComponent();
            InitializeComboBoxes();
            this.Text = "Create New Vehicle";
        }

        // Konstruktor untuk mengedit kendaraan yang sudah ada
        public VehicleForm(Vehicle vehicleToEdit)
        {
            InitializeComponent();
            InitializeComboBoxes();

            _vehicleToEdit = vehicleToEdit;
            this.Text = "Edit Vehicle";

            LoadVehicleData();
        }

        /// <summary>
        /// Mengisi nilai awal combo box untuk jenis kendaraan dan status.
        /// </summary>
        private void InitializeComboBoxes()
        {
            cmbType.Items.Add("Mobil");
            cmbType.Items.Add("Motor");
            cmbType.Items.Add("Truk");
            cmbType.Items.Add("Bus");
            cmbType.SelectedIndex = 0;

            cmbStatus.DataSource = Enum.GetValues(typeof(VehicleState));
        }

        /// <summary>
        /// Menampilkan data kendaraan ke dalam form jika sedang mode edit.
        /// </summary>
        private void LoadVehicleData()
        {
            if (_vehicleToEdit != null)
            {
                cmbType.SelectedItem = _vehicleToEdit.Type;
                txtBrand.Text = _vehicleToEdit.Brand;
                txtModel.Text = _vehicleToEdit.Model;
                cmbStatus.SelectedItem = _vehicleToEdit.State;
            }
        }

        /// <summary>
        /// Aksi saat tombol Simpan diklik: validasi dan simpan data form.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validasi agar brand dan model tidak kosong
            if (string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Brand and Model cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_vehicleToEdit != null)
            {
                _vehicleToEdit.Type = cmbType.SelectedItem.ToString();
                _vehicleToEdit.Brand = txtBrand.Text;
                _vehicleToEdit.Model = txtModel.Text;
                _vehicleToEdit.State = (VehicleState)cmbStatus.SelectedItem;
            }
            else
            {
                VehicleType = cmbType.SelectedItem.ToString();
                VehicleBrand = txtBrand.Text;
                VehicleModel = txtModel.Text;
                VehicleState = (VehicleState)cmbStatus.SelectedItem;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Aksi saat tombol Batal diklik: tutup form tanpa menyimpan.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
