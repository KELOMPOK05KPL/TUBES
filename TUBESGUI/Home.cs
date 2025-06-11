using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Home : Form
    {
        private Form? _activeForm;

        private void LoadForm(Form childForm)
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            _activeForm = childForm; 
            childForm.TopLevel = false; 
            childForm.FormBorderStyle = FormBorderStyle.None; 
            childForm.Dock = DockStyle.Fill; 

            mainpanel.Controls.Clear(); 
            mainpanel.Controls.Add(childForm); 
            mainpanel.Tag = childForm; 

            childForm.BringToFront(); 
            childForm.Show(); 
        }

        public Home()
        {
            InitializeComponent();
            LoadForm(new WelcomingForm());
        }

        //Page home
        private void BtnHome_Click(object sender, EventArgs e)
        {
            LoadForm(new WelcomingForm());
        }

        //Page All Vehicle
        private void BtnAllVehicle_Click(object sender, EventArgs e)
        {
            LoadForm(new LihatSemuaKendaraan());
        }

        //Page Rent Vehicle
        private void BtnRentVehicle_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Rent Vehicle button clicked");
            LoadForm(new SewaKendaraan());
        }

        //Page Return Vehicle
        private void BtnReturnVehicle_Click(object sender, EventArgs e)
        {
            LoadForm(new ReturnKendaraan());
        }

        //Page History
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            LoadForm(new Riwayat());
        }

        //Logout button
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();

                Login loginForm = new Login();
                loginForm.Show();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
