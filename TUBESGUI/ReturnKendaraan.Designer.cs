namespace TUBESGUI
{
    partial class ReturnKendaraan
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
            label2 = new Label();
            txtVehicleId = new TextBox();
            btnReturn = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(306, 32);
            label1.TabIndex = 0;
            label1.Text = "Pengembalian Kendaraan";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(325, 21);
            label2.TabIndex = 1;
            label2.Text = "Masukkan ID Kendaraan yang ingin Dipinjam:";
            // 
            // txtVehicleId
            // 
            txtVehicleId.Location = new Point(12, 121);
            txtVehicleId.Name = "txtVehicleId";
            txtVehicleId.Size = new Size(100, 23);
            txtVehicleId.TabIndex = 2;
            // 
            // btnReturn
            // 
            btnReturn.BackColor = SystemColors.Highlight;
            btnReturn.ForeColor = SystemColors.ButtonHighlight;
            btnReturn.Location = new Point(250, 121);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(87, 23);
            btnReturn.TabIndex = 3;
            btnReturn.Text = "Kembalikan ";
            btnReturn.UseVisualStyleBackColor = false;
            btnReturn.Click += btnReturn_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(12, 175);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 21);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // ReturnKendaraan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(btnReturn);
            Controls.Add(txtVehicleId);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReturnKendaraan";
            Text = "ReturnKendaraan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtVehicleId;
        private Button btnReturn;
        private Label lblStatus;
    }
}