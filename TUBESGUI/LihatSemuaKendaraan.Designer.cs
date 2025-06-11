namespace TUBESGUI
{
    partial class LihatSemuaKendaraan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            vehiclesDataGridView = new DataGridView();
            headerPanel = new Panel();
            titleLabel = new Label();
            footerPanel = new Panel();
            refreshButton = new Button();
            closeButton = new Button();
            loadingPanel = new Panel();
            loadingLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)vehiclesDataGridView).BeginInit();
            headerPanel.SuspendLayout();
            footerPanel.SuspendLayout();
            loadingPanel.SuspendLayout();
            SuspendLayout();
            // 
            // vehiclesDataGridView
            // 
            vehiclesDataGridView.AllowUserToAddRows = false;
            vehiclesDataGridView.AllowUserToDeleteRows = false;
            vehiclesDataGridView.BackgroundColor = Color.White;
            vehiclesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            vehiclesDataGridView.Dock = DockStyle.Fill;
            vehiclesDataGridView.Location = new Point(0, 71);
            vehiclesDataGridView.Margin = new Padding(3, 4, 3, 4);
            vehiclesDataGridView.Name = "vehiclesDataGridView";
            vehiclesDataGridView.ReadOnly = true;
            vehiclesDataGridView.RowHeadersWidth = 62;
            vehiclesDataGridView.RowTemplate.Height = 28;
            vehiclesDataGridView.Size = new Size(1074, 429);
            vehiclesDataGridView.TabIndex = 0;
            // 
            // headerPanel
            // 
            //headerPanel.BackColor = Color.FromArgb(35, 59, 110);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Margin = new Padding(3, 4, 3, 4);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1074, 71);
            headerPanel.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.ForeColor = Color.FromArgb(35, 59, 110);
            titleLabel.Location = new Point(13, 22);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(142, 28);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Vehicle List";
            // 
            // footerPanel
            // 
            footerPanel.BackColor = Color.WhiteSmoke;
            footerPanel.Controls.Add(refreshButton);
            footerPanel.Controls.Add(closeButton);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Location = new Point(0, 500);
            footerPanel.Margin = new Padding(3, 4, 3, 4);
            footerPanel.Name = "footerPanel";
            footerPanel.Size = new Size(1074, 62);
            footerPanel.TabIndex = 2;
            // 
            // refreshButton
            // 
            refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refreshButton.BackColor = Color.FromArgb(35, 59, 110);
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            refreshButton.ForeColor = Color.White;
            //refreshButton.Location = new Point(818, 8);
            refreshButton.Location = new Point(949, 8);
            refreshButton.Margin = new Padding(3, 4, 3, 4);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(111, 44);
            refreshButton.TabIndex = 1;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += OnRefreshButtonClick;
          
            // 
            // loadingPanel
            // 
            loadingPanel.BackColor = Color.FromArgb(0, 0, 0, 100);
            loadingPanel.Controls.Add(loadingLabel);
            loadingPanel.Dock = DockStyle.Fill;
            loadingPanel.Location = new Point(0, 71);
            loadingPanel.Margin = new Padding(3, 4, 3, 4);
            loadingPanel.Name = "loadingPanel";
            loadingPanel.Size = new Size(1074, 429);
            loadingPanel.TabIndex = 3;
            loadingPanel.Visible = false;
            // 
            // loadingLabel
            // 
            loadingLabel.Dock = DockStyle.Fill;
            loadingLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loadingLabel.ForeColor = Color.White;
            loadingLabel.Location = new Point(0, 0);
            loadingLabel.Name = "loadingLabel";
            loadingLabel.Size = new Size(1074, 429);
            loadingLabel.TabIndex = 0;
            loadingLabel.Text = "Loading vehicle data...";
            loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LihatSemuaKendaraan
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1074, 562);
            Controls.Add(loadingPanel);
            Controls.Add(vehiclesDataGridView);
            Controls.Add(headerPanel);
            Controls.Add(footerPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LihatSemuaKendaraan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vehicle List";
            ((System.ComponentModel.ISupportInitialize)vehiclesDataGridView).EndInit();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            footerPanel.ResumeLayout(false);
            loadingPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView vehiclesDataGridView;
        private Panel headerPanel;
        private Label titleLabel;
        private Panel footerPanel;
        private Button closeButton;
        private Button refreshButton;
        private Panel loadingPanel;
        private Label loadingLabel;
    }
}