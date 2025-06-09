using System;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Register : Form
    {
        private readonly LoginRegister loginRegister;

        public Register()
        {
            InitializeComponent();
            loginRegister = new LoginRegister();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            // Pusatkan panel ke tengah form
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;

            this.Resize += (s, ev) =>
            {
                panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text.Trim();
            string password = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Harap isi username dan password.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = await loginRegister.TriggerRegisterAsync(username, password);

                if (success)
                {
                    MessageBox.Show("Registrasi berhasil! Silakan login.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Alihkan kembali ke form login
                    Login loginForm = new Login();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Registrasi gagal. Username mungkin sudah digunakan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat registrasi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Alihkan ke form login jika user klik tulisan "Sudah punya akun?"
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
