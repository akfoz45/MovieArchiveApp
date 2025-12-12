using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Views;
using System;
using System.Windows.Forms;

namespace MovieArchiveApp
{
    public partial class frmLogin : Form
    {
        // readonly anahtar kelimesi, bu değerlerin sadece yapıcı metot (constructor) içinde bir kez atanabileceği ve sonradan değiştirilemeyeceği anlamına gelir.
        private readonly IAuthService _authService;
        private readonly frmMain _frmMain;
        public frmLogin(IAuthService authService, frmMain frmMain)
        {
            InitializeComponent();
            // NOT: Burası Üye 2 tarafından doldurulacak.
            // AuthService buraya inject edilecek ve butonlar bağlanacak.
            _authService = authService;
            _frmMain = frmMain;

            // Formun ekranın ortasında açılmasını sağlayalım
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            // 1. Verileri Al
            string username = frmLoginUsernameTextBox.Text.Trim();
            string password = frmLoginPasswordTextBox.Text;

            // 2. Ön Kontrol: Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizi giriniz.", "Giriş Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi burada durdur
            }

            // 3. Servisi Çağır: Kimlik Doğrulama
            // _authService, constructor ile aldığımız AuthService örneği.
            // Bu metot, veritabanına gider, şifreyi BCrypt ile doğrular ve başarılıysa kullanıcıyı döndürür.
            User? user = _authService.Login(username, password);

            if (user != null)
            {
                // 4a. Giriş Başarılı: Oturum başlatıldı (AuthService içinde SessionManager çağrıldı)
                MessageBox.Show($"Hoş geldiniz, {user.Username}!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ana Formu göster ve bu formu (Login) gizle
                _frmMain.Show();
                this.Hide();

                // Güvenlik ve temizlik için alanları temizle
                frmLoginUsernameTextBox.Clear();
                frmLoginPasswordTextBox.Clear();
            }
            else
            {
                // 4b. Giriş Başarısız: Kullanıcı adı veya şifre yanlış.
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmLoginPasswordTextBox.Clear(); // Şifreyi temizle
            }
        }

        private void frmLoginGoToSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (var signUpForm = new frmSignUp(_authService, _frmMain))
            {
                if (signUpForm.ShowDialog() == DialogResult.OK)
                {
                    // Kayıt başarılı → Login formunu geri getir
                    this.Show();
                }
                else
                {
                    // Kayıt iptal edildi → Login formunu geri getir
                    this.Show();
                }
            }
        }

        private void ShowHideButton_Click(object sender, EventArgs e)
        {
            // Mevcut durumunu kontrol et: PasswordChar boş mu? (yani şifre şu an gösteriliyor mu?)
            // Eğer PasswordChar özelliği '\0' değilse, şifre gizlidir.
            bool isPasswordHidden = frmLoginPasswordTextBox.PasswordChar != '\0';

            if (isPasswordHidden)
            {
                // 1. Şifreyi GÖSTER: Karakteri boş (null) karaktere ayarlayarak göster.
                frmLoginPasswordTextBox.PasswordChar = '\0';

                // 2. Buton metnini değiştir.
                LogInShowHideButton.Text = "Hide";
            }
            else
            {
                // 1. Şifreyi GİZLE: Karakteri '*' veya sistemin kullandığı '.' karakterine ayarla.
                // Genellikle güvenli olması için '*' kullanılır.
                frmLoginPasswordTextBox.PasswordChar = '●';

                // 2. Buton metnini değiştir.
                LogInShowHideButton.Text = "Show";
            }
        }

        private void frmLoginPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}