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
            panel2 = new Panel();
            SearchBar = new TextBox();
            lblSearch = new Label();
            panelBtn = new Panel();
            panelCreate = new Panel();
            panelLogout = new Panel();
            panel5 = new Panel();
            Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleData).BeginInit();
            panel2.SuspendLayout();
            panelBtn.SuspendLayout();
            panelCreate.SuspendLayout();
            panelLogout.SuspendLayout();
            panel5.SuspendLayout();
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
            button1.BackColor = Color.Red;
            button1.Dock = DockStyle.Fill;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(10, 10);
            button1.Name = "button1";
            button1.Size = new Size(97, 37);
            button1.TabIndex = 2;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Btn_Create
            // 
            Btn_Create.BackColor = Color.DarkGreen;
            Btn_Create.Dock = DockStyle.Fill;
            Btn_Create.FlatStyle = FlatStyle.Flat;
            Btn_Create.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            Btn_Create.ForeColor = SystemColors.ButtonHighlight;
            Btn_Create.ImageAlign = ContentAlignment.TopRight;
            Btn_Create.Location = new Point(10, 10);
            Btn_Create.Name = "Btn_Create";
            Btn_Create.Size = new Size(120, 37);
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
            VehicleData.Location = new Point(0, 0);
            VehicleData.Name = "VehicleData";
            VehicleData.ReadOnly = true;
            VehicleData.RowHeadersWidth = 51;
            VehicleData.Size = new Size(842, 333);
            VehicleData.TabIndex = 1;
            VehicleData.CellContentClick += VehicleData_CellContentClick;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(842, 0);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(SearchBar);
            panel2.Controls.Add(lblSearch);
            panel2.Controls.Add(panelBtn);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 57);
            panel2.Name = "panel2";
            panel2.Size = new Size(842, 57);
            panel2.TabIndex = 3;
            // 
            // SearchBar
            // 
            SearchBar.Location = new Point(96, 15);
            SearchBar.Name = "SearchBar";
            SearchBar.Size = new Size(379, 27);
            SearchBar.TabIndex = 2;
            SearchBar.TextChanged += SearchBar_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(35, 59, 110);
            lblSearch.Location = new Point(21, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(59, 20);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Search:";
            // 
            // panelBtn
            // 
            panelBtn.AutoSize = true;
            panelBtn.Controls.Add(panelCreate);
            panelBtn.Controls.Add(panelLogout);
            panelBtn.Dock = DockStyle.Right;
            panelBtn.Location = new Point(585, 0);
            panelBtn.Name = "panelBtn";
            panelBtn.Size = new Size(257, 57);
            panelBtn.TabIndex = 0;
            // 
            // panelCreate
            // 
            panelCreate.Controls.Add(Btn_Create);
            panelCreate.Dock = DockStyle.Right;
            panelCreate.Location = new Point(117, 0);
            panelCreate.Name = "panelCreate";
            panelCreate.Padding = new Padding(10);
            panelCreate.Size = new Size(140, 57);
            panelCreate.TabIndex = 2;
            // 
            // panelLogout
            // 
            panelLogout.Controls.Add(button1);
            panelLogout.Dock = DockStyle.Left;
            panelLogout.Location = new Point(0, 0);
            panelLogout.Name = "panelLogout";
            panelLogout.Padding = new Padding(10);
            panelLogout.Size = new Size(117, 57);
            panelLogout.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(VehicleData);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 114);
            panel5.Name = "panel5";
            panel5.Size = new Size(842, 333);
            panel5.TabIndex = 4;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 447);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(Header);
            Margin = new Padding(2);
            Name = "AdminDashboard";
            Text = "AdminDashboard";
            WindowState = FormWindowState.Maximized;
            Load += AdminDashboard_LoadAsync;
            Header.ResumeLayout(false);
            Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleData).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelBtn.ResumeLayout(false);
            panelCreate.ResumeLayout(false);
            panelLogout.ResumeLayout(false);
            panel5.ResumeLayout(false);
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
        private Panel panel2;
        private Panel panelBtn;
        private Panel panelLogout;
        private Panel panel5;
        private Panel panelCreate;
        private TextBox SearchBar;
        private Label lblSearch;
    }
}