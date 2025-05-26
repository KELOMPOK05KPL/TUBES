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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Harap isi username dan password.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = await loginRegister.TriggerLoginAsync(username, password);

                if (success)
                {
                    MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ganti ke form utama setelah login sukses
                    Home home = new Home(); // Ganti dengan form utama milikmu
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
                MessageBox.Show($"Terjadi kesalahan saat login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide(); // Sembunyikan form login
        }

        private void Login_Load(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}