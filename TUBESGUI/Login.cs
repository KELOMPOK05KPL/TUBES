using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Login : Form
    {
        private readonly LoginRegister _loginRegister;

        public Login()
        {
            InitializeComponent();
            _loginRegister = new LoginRegister();
        }

        // Saat tombol login ditekan
        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text.Trim();
            string password = textBox1.Text.Trim();

            if (IsInputInvalid(username, password))
            {
                MessageBox.Show("Harap isi username dan password.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false;

            try
            {
                await HandleLoginAsync(username, password);
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

        // Fungsi login utama (admin/user)
        private async Task HandleLoginAsync(string username, string password)
        {
            var user = await _loginRegister.GetUserByCredentialsAsync(username, password);

            if (user != null)
            {
                MessageBox.Show($"Login berhasil! Selamat datang, {user.Username}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateToDashboard(user.Role);
            }
            else
            {
                MessageBox.Show("User belum terdaftar atau password salah!", "Gagal Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Navigasi berdasarkan role
        private void NavigateToDashboard(string role)
        {
            Form nextForm = role.Equals("admin", StringComparison.OrdinalIgnoreCase)
                ? new AdminDashboard()
                : new Home();

            nextForm.Show();
            Hide();
        }

        // Validasi input kosong
        private static bool IsInputInvalid(string username, string password)
        {
            return string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password);
        }

        // Arahkan ke form register
        private void label1_Click(object sender, EventArgs e)
        {
            var registerForm = new Register();
            registerForm.Show();
            Hide();
        }

        // Pusatkan panel saat form load
        private void Login_Load(object sender, EventArgs e)
        {
            CenterPanel();
            Resize += (s, _) => CenterPanel();
        }

        // Fungsi memusatkan panel login
        private void CenterPanel()
        {
            if (panel1 != null)
            {
                panel1.Left = (ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (ClientSize.Height - panel1.Height) / 2;
            }
        }

        // Event tidak digunakan
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
