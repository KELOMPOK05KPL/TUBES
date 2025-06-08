namespace TUBESGUI
{
    partial class AdminDashboard
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
            Header = new Panel();
            labelTitle = new Label();
            button1 = new Button();
            Btn_Create = new Button();
            VehicleData = new DataGridView();
            panel1 = new Panel();
            Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleData).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Header
            // 
            Header.BackColor = Color.FromArgb(35, 59, 110);
            Header.Controls.Add(labelTitle);
            Header.Dock = DockStyle.Top;
            Header.Location = new Point(0, 0);
            Header.Name = "Header";
            Header.Size = new Size(842, 57);
            Header.TabIndex = 0;
            Header.Paint += Header_Paint;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            labelTitle.ForeColor = SystemColors.ButtonHighlight;
            labelTitle.Location = new Point(12, 18);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(186, 23);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Admin Dashboard";
            labelTitle.Click += labelTitle_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(615, 9);
            button1.Name = "button1";
            button1.Size = new Size(97, 42);
            button1.TabIndex = 2;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Btn_Create
            // 
            Btn_Create.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btn_Create.BackColor = Color.DarkGreen;
            Btn_Create.FlatStyle = FlatStyle.Flat;
            Btn_Create.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            Btn_Create.ForeColor = SystemColors.ButtonHighlight;
            Btn_Create.ImageAlign = ContentAlignment.TopRight;
            Btn_Create.Location = new Point(733, 9);
            Btn_Create.Name = "Btn_Create";
            Btn_Create.Size = new Size(97, 42);
            Btn_Create.TabIndex = 1;
            Btn_Create.Text = "+ Create";
            Btn_Create.UseVisualStyleBackColor = false;
            Btn_Create.Click += Btn_Create_Click;
            // 
            // VehicleData
            // 
            VehicleData.AllowUserToAddRows = false;
            VehicleData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            VehicleData.BackgroundColor = SystemColors.ButtonHighlight;
            VehicleData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            VehicleData.Dock = DockStyle.Fill;
            VehicleData.GridColor = SystemColors.InfoText;
            VehicleData.Location = new Point(0, 57);
            VehicleData.Name = "VehicleData";
            VehicleData.ReadOnly = true;
            VehicleData.RowHeadersWidth = 51;
            VehicleData.Size = new Size(842, 390);
            VehicleData.TabIndex = 1;
            VehicleData.CellContentClick += VehicleData_CellContentClick;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(Btn_Create);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 393);
            panel1.Name = "panel1";
            panel1.Size = new Size(842, 54);
            panel1.TabIndex = 2;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 447);
            Controls.Add(panel1);
            Controls.Add(VehicleData);
            Controls.Add(Header);
            Margin = new Padding(2);
            Name = "AdminDashboard";
            Text = "AdminDashboard";
            WindowState = FormWindowState.Maximized;
            Load += AdminDashboard_LoadAsync;
            Header.ResumeLayout(false);
            Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleData).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel Header;
        private Label labelTitle;
        private DataGridView VehicleData;
        private Button Btn_Create;
        private Button button1;
        private Panel panel1;
    }
}