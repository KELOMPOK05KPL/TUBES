using System;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Login : Form
    {
        private LoginRegister loginRegister;

        public Login()
        {
            InitializeComponent();
            loginRegister = new LoginRegister();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Harap isi username dan password.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false; // Hindari klik ganda

            try
            {
                bool success = await loginRegister.TriggerLoginAsync(username, password);

                if (success)
                {
                    MessageBox.Show($"Login berhasil! Selamat datang, {password}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Pindah ke form utama setelah login berhasil
                    var home = new Home(); // Pastikan form 'Home' tersedia
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User belum terdaftar atau password salah!", "Gagal Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat login:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var registerForm = new Register(); // Pastikan form 'Register' tersedia
            registerForm.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CenterPanel();
            this.Resize += (s, ev) => CenterPanel();
        }

        private void CenterPanel()
        {
            if (panel1 != null)
            {
                panel1.Left = (ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (ClientSize.Height - panel1.Height) / 2;
            }
        }

        // Event kosong bisa dihapus jika tidak digunakan
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
