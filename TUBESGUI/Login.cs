using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUBESGUI
{
    public partial class Login : Form
    {
        // Instance untuk menangani proses login
        private readonly LoginRegister _loginRegister;

        public Login()
        {
            InitializeComponent();
            _loginRegister = new LoginRegister();
        }

        // Event handler saat tombol login diklik
        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Validasi input kosong
            if (IsInputInvalid(username, password))
            {
                MessageBox.Show("Harap isi username dan password.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false; // Mencegah klik ganda

            try
            {
                // Pemeriksaan kredensial admin
                if (IsAdminCredentials(username, password))
                {
                    HandleAdminLogin();
                    return;
                }

                // Proses login untuk user biasa
                await HandleUserLoginAsync(username, password);
            }
            catch (Exception ex)
            {
                // Penanganan error tidak terduga
                MessageBox.Show($"Terjadi kesalahan saat login:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true; // Aktifkan kembali tombol setelah proses login selesai
            }
        }

        // Validasi input kosong
        private static bool IsInputInvalid(string username, string password)
        {
            return string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password);
        }

        // Pemeriksaan kredensial admin (harcoded — sebaiknya gunakan metode yang lebih aman)
        private static bool IsAdminCredentials(string username, string password)
        {
            return username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin4321";
        }

        // Penanganan login khusus admin
        private void HandleAdminLogin()
        {
            MessageBox.Show("Login Admin berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var adminForm = new adminForm(); // Form khusus admin, pastikan sudah dibuat
            adminForm.Show();
            Hide(); // Sembunyikan form login
        }

        // Proses login untuk user biasa
        private async Task HandleUserLoginAsync(string username, string password)
        {
            bool success = await _loginRegister.TriggerLoginAsync(username, password);

            if (success)
            {
                MessageBox.Show($"Login berhasil! Selamat datang, {username}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var homeForm = new Home(); // Form utama user
                homeForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("User belum terdaftar atau password salah!", "Gagal Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event saat label "Register" diklik
        private void label1_Click(object sender, EventArgs e)
        {
            var registerForm = new Register(); // Form registrasi user
            registerForm.Show();
            Hide();
        }

        // Event saat form login dimuat
        private void Login_Load(object sender, EventArgs e)
        {
            CenterPanel(); // Posisikan panel ke tengah
            Resize += (s, _) => CenterPanel(); // Responsif saat form diubah ukurannya
        }

        // Fungsi untuk memusatkan panel pada form
        private void CenterPanel()
        {
            if (panel1 != null)
            {
                panel1.Left = (ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (ClientSize.Height - panel1.Height) / 2;
            }
        }

        // Event kosong yang tidak digunakan dapat dihapus
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
