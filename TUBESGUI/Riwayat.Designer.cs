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

            // panel1
            panel1.Controls.Add(txtVehicleId);
            panel1.Controls.Add(btnPinjam);
            panel1.Controls.Add(btnKembalikan);
            panel1.Controls.Add(dgvRiwayat);
            panel1.Controls.Add(lblStatus);
            panel1.Dock = DockStyle.Fill;

            // txtVehicleId
            txtVehicleId.BackColor = Color.LightSkyBlue;
            txtVehicleId.Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtVehicleId.ForeColor = SystemColors.ControlText;
            txtVehicleId.Location = new Point(20, 20);
            txtVehicleId.Size = new Size(300, 30);
            txtVehicleId.PlaceholderText = "Masukkan ID Kendaraan";
            txtVehicleId.TabIndex = 0;

            // btnPinjam
            btnPinjam.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPinjam.ForeColor = Color.DarkSlateGray;
            btnPinjam.Location = new Point(350, 20);
            btnPinjam.Size = new Size(120, 40);
            btnPinjam.Text = "Pinjam";
            btnPinjam.TabIndex = 1;
            btnPinjam.UseVisualStyleBackColor = true;
            btnPinjam.Click += BtnPinjam_Click;

            // btnKembalikan
            btnKembalikan.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnKembalikan.ForeColor = Color.DarkSlateGray;
            btnKembalikan.Location = new Point(490, 20);
            btnKembalikan.Size = new Size(120, 40);
            btnKembalikan.Text = "Kembalikan";
            btnKembalikan.TabIndex = 2;
            btnKembalikan.UseVisualStyleBackColor = true;
            btnKembalikan.Click += BtnKembalikan_Click;

            // dgvRiwayat
            dgvRiwayat.Location = new Point(20, 80);
            dgvRiwayat.Size = new Size(740, 300);
            dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRiwayat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRiwayat.ReadOnly = true;
            dgvRiwayat.AllowUserToAddRows = false;
            dgvRiwayat.AllowUserToDeleteRows = false;
            dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRiwayat.TabIndex = 3;

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.DarkSlateGray;
            lblStatus.Location = new Point(20, 400);
            lblStatus.Size = new Size(60, 20);
            lblStatus.Text = "Status: ";

            // Riwayat
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Text = "Riwayat Peminjaman";
            StartPosition = FormStartPosition.CenterScreen;

            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }
    }
}