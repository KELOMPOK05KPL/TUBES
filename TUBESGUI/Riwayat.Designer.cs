using System;
using System.Drawing;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Riwayat : Form
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtVehicleId;
        private Button btnPinjam;
        private Button btnKembalikan;
        private DataGridView dgvRiwayat;
        private Label lblStatus;
        private Panel panel1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtVehicleId = new TextBox();
            btnPinjam = new Button();
            btnKembalikan = new Button();
            dgvRiwayat = new DataGridView();
            lblStatus = new Label();
            panel1 = new Panel();


            panel1.Controls.Add(txtVehicleId);
            panel1.Controls.Add(btnPinjam);
            panel1.Controls.Add(btnKembalikan);
            panel1.Controls.Add(dgvRiwayat);
            panel1.Controls.Add(lblStatus);
            panel1.Dock = DockStyle.Fill;

            txtVehicleId.BackColor = Color.LightSkyBlue;
            txtVehicleId.Font = new System.Drawing.Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtVehicleId.ForeColor = SystemColors.ControlText;
            txtVehicleId.Location = new Point(20, 20);
            txtVehicleId.Size = new Size(300, 30);
            txtVehicleId.PlaceholderText = "Masukkan ID Kendaraan";
            txtVehicleId.TabIndex = 0;

            btnPinjam.Font = new System.Drawing.Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPinjam.ForeColor = Color.DarkSlateGray;
            btnPinjam.Location = new Point(350, 20);
            btnPinjam.Size = new Size(120, 40);
            btnPinjam.Text = "Pinjam";
            btnPinjam.Click += BtnPinjam_Click;
            btnPinjam.TabIndex = 1;

            btnKembalikan.Font = new System.Drawing.Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnKembalikan.ForeColor = Color.DarkSlateGray;
            btnKembalikan.Location = new Point(490, 20);
            btnKembalikan.Size = new Size(120, 40);
            btnKembalikan.Text = "Kembalikan";
            btnKembalikan.Click += BtnKembalikan_Click;
            btnKembalikan.TabIndex = 2;

            dgvRiwayat.Location = new Point(20, 80);
            dgvRiwayat.Size = new Size(740, 300);
            dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRiwayat.ReadOnly = true;
            dgvRiwayat.AllowUserToAddRows = false;
            dgvRiwayat.AllowUserToDeleteRows = false;
            dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRiwayat.TabIndex = 3;

            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.DarkSlateGray;
            lblStatus.Location = new Point(20, 400);
            lblStatus.Size = new Size(60, 20);
            lblStatus.Text = "Status: ";

            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(panel1);
            this.Text = "Riwayat Peminjaman";
            this.StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void BtnPinjam_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proses Peminjaman!");
        }

        private void BtnKembalikan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proses Pengembalian!");
        }
    }
}