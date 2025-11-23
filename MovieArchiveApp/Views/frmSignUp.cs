using MovieArchiveApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieArchiveApp.Data.Entities;

namespace MovieArchiveApp.Views
{
    public partial class frmSignUp : Form
    {
        private readonly IAuthService _authService;
        private readonly frmMain _frmMain;

        public frmSignUp(IAuthService authService, frmMain frmMain)
        {
            InitializeComponent();
            _authService = authService;
            _frmMain = frmMain;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = frmSignUpUsernameTextBox.Text.Trim();
            string password = frmSignUpPasswordTextBox.Text;
            string passwordConfirm = frmSignUpPasswordAgainTextBox.Text;

            // 1. Alan Kontrolü
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(passwordConfirm))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Şifre Eşleşme Kontrolü
            if (password != passwordConfirm)
            {
                MessageBox.Show("Şifreler eşleşmiyor. Lütfen kontrol ediniz.", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmSignUpPasswordTextBox.Clear();
                frmSignUpPasswordAgainTextBox.Clear();
                return;
            }

            // 3. Servisi Çağır (isAdmin: false olarak kaydediyoruz)
            User? newUser = _authService.SignUp(username, password, isAdmin: false);

            if (newUser != null)
            {
                // Kayıt ve Oturum Başarılı
                MessageBox.Show($"Kayıt başarılı! Hoş geldiniz, {newUser.Username}.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ana formu göster ve kayıt formunu kapat
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Kayıt Başarısız (Kullanıcı adı muhtemelen zaten var)
                MessageBox.Show("Bu kullanıcı adı zaten sistemde kayıtlı.", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmSignUpUsernameTextBox.Focus();
            }
        }

        private void frmSignUpGoBackBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SignUpShowHideButton_Click(object sender, EventArgs e)
        {
            // Mevcut durumunu kontrol et: PasswordChar boş mu? (yani şifre şu an gösteriliyor mu?)
            // Eğer PasswordChar özelliği '\0' değilse, şifre gizlidir.
            bool isPasswordHidden = frmSignUpPasswordTextBox.PasswordChar != '\0';
            bool isPasswordAgainHidden = frmSignUpPasswordAgainTextBox.PasswordChar != '\0';

            if (isPasswordHidden && isPasswordAgainHidden)
            {
                // 1. Şifreyi GÖSTER: Karakteri boş (null) karaktere ayarlayarak göster.
                frmSignUpPasswordTextBox.PasswordChar = '\0';
                frmSignUpPasswordAgainTextBox.PasswordChar = '\0';

                // 2. Buton metnini değiştir.
                SignUpShowHideButton.Text = "Hide";
            }
            else
            {
                // 1. Şifreyi GİZLE: Karakteri '*' veya sistemin kullandığı '.' karakterine ayarla.
                // Genellikle güvenli olması için '*' kullanılır.
                frmSignUpPasswordTextBox.PasswordChar = '●';
                frmSignUpPasswordAgainTextBox.PasswordChar = '●';

                // 2. Buton metnini değiştir.
                SignUpShowHideButton.Text = "Show";
            }
        }
    }
}
