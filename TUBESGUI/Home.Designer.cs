namespace TUBESGUI
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            BtnLogout = new Button();
            BtnHistory = new Button();
            BtnReturnVehicle = new Button();
            BtnRentVehicle = new Button();
            BtnAllVehicle = new Button();
            BtnHome = new Button();
            mainpanel = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 59, 110);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(752, 34);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(37, 11);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(107, 17);
            label1.TabIndex = 3;
            label1.Text = "Vehicle Rental";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(8, 7);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 21);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(35, 59, 110);
            panel2.Controls.Add(BtnLogout);
            panel2.Controls.Add(BtnHistory);
            panel2.Controls.Add(BtnReturnVehicle);
            panel2.Controls.Add(BtnRentVehicle);
            panel2.Controls.Add(BtnAllVehicle);
            panel2.Controls.Add(BtnHome);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 34);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(752, 26);
            panel2.TabIndex = 1;
            // 
            // BtnLogout
            // 
            BtnLogout.BackColor = Color.FromArgb(192, 0, 0);
            BtnLogout.Dock = DockStyle.Right;
            BtnLogout.FlatAppearance.BorderSize = 0;
            BtnLogout.FlatStyle = FlatStyle.Flat;
            BtnLogout.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnLogout.ForeColor = Color.White;
            BtnLogout.Location = new Point(682, 0);
            BtnLogout.Margin = new Padding(2);
            BtnLogout.Name = "BtnLogout";
            BtnLogout.Size = new Size(70, 26);
            BtnLogout.TabIndex = 7;
            BtnLogout.Text = "Logout";
            BtnLogout.UseVisualStyleBackColor = false;
            BtnLogout.Click += BtnLogout_Click;
            // 
            // BtnHistory
            // 
            BtnHistory.BackColor = Color.FromArgb(35, 59, 110);
            BtnHistory.Dock = DockStyle.Left;
            BtnHistory.FlatAppearance.BorderSize = 0;
            BtnHistory.FlatStyle = FlatStyle.Flat;
            BtnHistory.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnHistory.ForeColor = Color.White;
            BtnHistory.Location = new Point(390, 0);
            BtnHistory.Margin = new Padding(2);
            BtnHistory.Name = "BtnHistory";
            BtnHistory.Size = new Size(95, 26);
            BtnHistory.TabIndex = 6;
            BtnHistory.Text = "History";
            BtnHistory.UseVisualStyleBackColor = false;
            BtnHistory.Click += BtnHistory_Click;
            // 
            // BtnReturnVehicle
            // 
            BtnReturnVehicle.BackColor = Color.FromArgb(35, 59, 110);
            BtnReturnVehicle.Dock = DockStyle.Left;
            BtnReturnVehicle.FlatAppearance.BorderSize = 0;
            BtnReturnVehicle.FlatStyle = FlatStyle.Flat;
            BtnReturnVehicle.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnReturnVehicle.ForeColor = Color.White;
            BtnReturnVehicle.Location = new Point(285, 0);
            BtnReturnVehicle.Margin = new Padding(2);
            BtnReturnVehicle.Name = "BtnReturnVehicle";
            BtnReturnVehicle.Size = new Size(105, 26);
            BtnReturnVehicle.TabIndex = 5;
            BtnReturnVehicle.Text = "Return Vehicle";
            BtnReturnVehicle.UseVisualStyleBackColor = false;
            BtnReturnVehicle.Click += BtnReturnVehicle_Click;
            // 
            // BtnRentVehicle
            // 
            BtnRentVehicle.BackColor = Color.FromArgb(35, 59, 110);
            BtnRentVehicle.Dock = DockStyle.Left;
            BtnRentVehicle.FlatAppearance.BorderSize = 0;
            BtnRentVehicle.FlatStyle = FlatStyle.Flat;
            BtnRentVehicle.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnRentVehicle.ForeColor = Color.White;
            BtnRentVehicle.Location = new Point(190, 0);
            BtnRentVehicle.Margin = new Padding(2);
            BtnRentVehicle.Name = "BtnRentVehicle";
            BtnRentVehicle.Size = new Size(95, 26);
            BtnRentVehicle.TabIndex = 4;
            BtnRentVehicle.Text = "Rent Vehicle";
            BtnRentVehicle.UseVisualStyleBackColor = false;
            BtnRentVehicle.Click += BtnRentVehicle_Click;
            // 
            // BtnAllVehicle
            // 
            BtnAllVehicle.BackColor = Color.FromArgb(35, 59, 110);
            BtnAllVehicle.Dock = DockStyle.Left;
            BtnAllVehicle.FlatAppearance.BorderSize = 0;
            BtnAllVehicle.FlatStyle = FlatStyle.Flat;
            BtnAllVehicle.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAllVehicle.ForeColor = Color.White;
            BtnAllVehicle.Location = new Point(95, 0);
            BtnAllVehicle.Margin = new Padding(2);
            BtnAllVehicle.Name = "BtnAllVehicle";
            BtnAllVehicle.Size = new Size(95, 26);
            BtnAllVehicle.TabIndex = 3;
            BtnAllVehicle.Text = "All Vehicle";
            BtnAllVehicle.UseVisualStyleBackColor = false;
            BtnAllVehicle.Click += BtnAllVehicle_Click;
            // 
            // BtnHome
            // 
            BtnHome.BackColor = Color.FromArgb(35, 59, 110);
            BtnHome.Dock = DockStyle.Left;
            BtnHome.FlatAppearance.BorderSize = 0;
            BtnHome.FlatStyle = FlatStyle.Flat;
            BtnHome.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnHome.ForeColor = Color.White;
            BtnHome.Location = new Point(0, 0);
            BtnHome.Margin = new Padding(2);
            BtnHome.Name = "BtnHome";
            BtnHome.Size = new Size(95, 26);
            BtnHome.TabIndex = 2;
            BtnHome.Text = "Home";
            BtnHome.UseVisualStyleBackColor = false;
            BtnHome.Click += BtnHome_Click;
            // 
            // mainpanel
            // 
            mainpanel.Location = new Point(0, 57);
            mainpanel.Name = "mainpanel";
            mainpanel.Size = new Size(752, 571);
            mainpanel.TabIndex = 2;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(752, 369);
            Controls.Add(mainpanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(2);
            Name = "Home";
            Text = "Home";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label label1;
        private Button BtnHome;
        private Button BtnAllVehicle;
        private Button BtnRentVehicle;
        private Button BtnReturnVehicle;
        private Button BtnHistory;
        private Button BtnLogout;
        private Panel mainpanel;
    }
}