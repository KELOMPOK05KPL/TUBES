using System.Drawing;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Riwayat : Form
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvRiwayat;
        private Label lblStatus;
        private Panel panel1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvRiwayat = new DataGridView();
            this.lblStatus = new Label();
            this.panel1 = new Panel();

            // panel1
            this.panel1.Controls.Add(this.dgvRiwayat);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Dock = DockStyle.Fill;

            // dgvRiwayat
            this.dgvRiwayat.Location = new Point(20, 20);
            this.dgvRiwayat.Size = new Size(740, 350);
            this.dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRiwayat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRiwayat.ReadOnly = true;
            this.dgvRiwayat.AllowUserToAddRows = false;
            this.dgvRiwayat.AllowUserToDeleteRows = false;
            this.dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Times New Roman", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblStatus.ForeColor = Color.DarkSlateGray;
            this.lblStatus.Location = new Point(20, 380);
            this.lblStatus.Size = new Size(60, 20);
            this.lblStatus.Text = "Status: ";

            // Riwayat
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Text = "Riwayat Peminjaman";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiwayat)).EndInit();
            this.ResumeLayout(false);
        }
    }
}