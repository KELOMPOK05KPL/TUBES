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
        public Home()
        {
            InitializeComponent();
        }

        //Page home
        private void BtnHome_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Home button clicked");
        }

        //Page All Vehicle
        private void BtnAllVehicle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Vehicle button clicked");
        }

        //Page Rent Vehicle
        private void BtnRentVehicle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rent Vehicle button clicked");
        }

        //Page Return Vehicle
        private void BtnReturnVehicle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Return Vehicle button clicked");
        }

        //Page History
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("History button clicked");
        }

        //Logout button
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close(); // atau tampilkan form login
            }
        }
    }
}
