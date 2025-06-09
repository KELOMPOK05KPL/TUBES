namespace TUBESGUI
{
    partial class SewaKendaraan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            DataKendaraan = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxId = new TextBox();
            textBoxNama = new TextBox();
            textBoxDurasi = new TextBox();
            ButtonCekHarga = new Button();
            lblHarga = new Label();
            ButtonSewa = new Button();
            ((System.ComponentModel.ISupportInitialize)DataKendaraan).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(25, 26);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 0;
            label1.Text = "Sewa Kendaraan";
            // 
            // DataKendaraan
            // 
            DataKendaraan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataKendaraan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataKendaraan.Location = new Point(25, 69);
            DataKendaraan.Name = "DataKendaraan";
            DataKendaraan.ReadOnly = true;
            DataKendaraan.Size = new Size(404, 134);
            DataKendaraan.TabIndex = 1;
            DataKendaraan.CellContentClick += DataKendaraan_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 225);
            label2.Name = "label2";
            label2.Size = new Size(137, 15);
            label2.TabIndex = 2;
            label2.Text = "Masukan ID Kendaraan : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 266);
            label3.Name = "label3";
            label3.Size = new Size(146, 15);
            label3.TabIndex = 3;
            label3.Text = "Masukan Nama Penyewa :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 309);
            label4.Name = "label4";
            label4.Size = new Size(149, 15);
            label4.TabIndex = 4;
            label4.Text = "Durasi Peminjaman (Hari) :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 356);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 5;
            label5.Text = "Total Harga :";
            // 
            // textBoxId
            // 
            textBoxId.Location = new Point(182, 219);
            textBoxId.Name = "textBoxId";
            textBoxId.Size = new Size(246, 23);
            textBoxId.TabIndex = 6;
            // 
            // textBoxNama
            // 
            textBoxNama.Location = new Point(182, 263);
            textBoxNama.Name = "textBoxNama";
            textBoxNama.Size = new Size(246, 23);
            textBoxNama.TabIndex = 7;
            // 
            // textBoxDurasi
            // 
            textBoxDurasi.Location = new Point(182, 306);
            textBoxDurasi.Name = "textBoxDurasi";
            textBoxDurasi.Size = new Size(246, 23);
            textBoxDurasi.TabIndex = 8;
            // 
            // ButtonCekHarga
            // 
            ButtonCekHarga.BackColor = Color.MidnightBlue;
            ButtonCekHarga.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonCekHarga.ForeColor = SystemColors.ButtonHighlight;
            ButtonCekHarga.Location = new Point(337, 348);
            ButtonCekHarga.Name = "ButtonCekHarga";
            ButtonCekHarga.Size = new Size(92, 31);
            ButtonCekHarga.TabIndex = 9;
            ButtonCekHarga.Text = " Cek Harga";
            ButtonCekHarga.UseVisualStyleBackColor = false;
            ButtonCekHarga.Click += ButtonCekHarga_Click;
            // 
            // lblHarga
            // 
            lblHarga.AutoSize = true;
            lblHarga.Location = new Point(182, 356);
            lblHarga.Name = "lblHarga";
            lblHarga.Size = new Size(0, 15);
            lblHarga.TabIndex = 10;
            // 
            // ButtonSewa
            // 
            ButtonSewa.BackColor = Color.MidnightBlue;
            ButtonSewa.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonSewa.ForeColor = SystemColors.ButtonHighlight;
            ButtonSewa.Location = new Point(31, 394);
            ButtonSewa.Name = "ButtonSewa";
            ButtonSewa.Size = new Size(398, 35);
            ButtonSewa.TabIndex = 11;
            ButtonSewa.Text = "Sewa";
            ButtonSewa.UseVisualStyleBackColor = false;
            ButtonSewa.Click += ButtonSewa_Click;
            // 
            // SewaKendaraan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonSewa);
            Controls.Add(lblHarga);
            Controls.Add(ButtonCekHarga);
            Controls.Add(textBoxDurasi);
            Controls.Add(textBoxNama);
            Controls.Add(textBoxId);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(DataKendaraan);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SewaKendaraan";
            Text = "SewaKendaraan";
            ((System.ComponentModel.ISupportInitialize)DataKendaraan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView DataKendaraan;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxId;
        private TextBox textBoxNama;
        private TextBox textBoxDurasi;
        private Button ButtonCekHarga;
        private Label lblHarga;
        private Button ButtonSewa;
    }
}