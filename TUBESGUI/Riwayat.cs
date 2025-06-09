using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Riwayat : Form
    {
        private List<RiwayatPeminjaman> riwayatPeminjaman;

        public Riwayat()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            riwayatPeminjaman = new List<RiwayatPeminjaman>
            {
                new RiwayatPeminjaman { Id = 1, VehicleId = "KND123", Status = "Dipinjam", Tanggal = DateTime.Now.AddDays(-1) },
                new RiwayatPeminjaman { Id = 2, VehicleId = "KND124", Status = "Dikembalikan", Tanggal = DateTime.Now.AddDays(-3) }
            };

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dgvRiwayat.DataSource = null;
            dgvRiwayat.DataSource = riwayatPeminjaman.Select(r => new
            {
                r.Id,
                r.VehicleId,
                r.Status,
                Tanggal = r.Tanggal.ToString("yyyy-MM-dd HH:mm")
            }).ToList();
        }

        private void BtnPinjam_Click(object sender, EventArgs e)
        {
            string vehicleId = txtVehicleId.Text.Trim();

            if (string.IsNullOrEmpty(vehicleId))
            {
                lblStatus.Text = "Status: ID kendaraan tidak boleh kosong.";
                return;
            }

            var existing = riwayatPeminjaman.FirstOrDefault(r => r.VehicleId == vehicleId && r.Status == "Dipinjam");
            if (existing != null)
            {
                lblStatus.Text = "Status: Kendaraan sudah dipinjam.";
                return;
            }

            riwayatPeminjaman.Add(new RiwayatPeminjaman
            {
                Id = riwayatPeminjaman.Count + 1,
                VehicleId = vehicleId,
                Status = "Dipinjam",
                Tanggal = DateTime.Now
            });

            lblStatus.Text = "Status: Peminjaman berhasil.";
            RefreshDataGrid();
        }

        private void BtnKembalikan_Click(object sender, EventArgs e)
        {
            string vehicleId = txtVehicleId.Text.Trim();

            if (string.IsNullOrEmpty(vehicleId))
            {
                lblStatus.Text = "Status: ID kendaraan tidak boleh kosong.";
                return;
            }

            var existing = riwayatPeminjaman.FirstOrDefault(r => r.VehicleId == vehicleId && r.Status == "Dipinjam");
            if (existing == null)
            {
                lblStatus.Text = "Status: Kendaraan tidak ditemukan atau belum dipinjam.";
                return;
            }

            existing.Status = "Dikembalikan";
            existing.Tanggal = DateTime.Now;

            lblStatus.Text = "Status: Pengembalian berhasil.";
            RefreshDataGrid();
        }
    }

    public class RiwayatPeminjaman
    {
        public int Id { get; set; }
        public string VehicleId { get; set; }
        public string Status { get; set; }
        public DateTime Tanggal { get; set; }
    }
}