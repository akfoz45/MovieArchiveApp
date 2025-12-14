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
        private readonly IAuthService _authService; // Service for authentication (Sign Up/Login).
        private readonly frmMain _frmMain; // Main form instance.

        public frmSignUp(IAuthService authService, frmMain frmMain) // Constructor for the Sign Up form.
        {
            InitializeComponent(); // Initialize the form components.
            _authService = authService; // Set the authentication service.
            _frmMain = frmMain; // Set the main form instance.

            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Runs when the 'Sign Up' button is clicked.
        {
            // Get the values from the text boxes.
            string username = frmSignUpUsernameTextBox.Text.Trim();
            string password = frmSignUpPasswordTextBox.Text;
            string passwordConfirm = frmSignUpPasswordAgainTextBox.Text;

            // 1. Field Check: Check if all fields are filled.
            if (string.IsNullOrWhiteSpace(username) ||
        string.IsNullOrWhiteSpace(password) ||
        string.IsNullOrWhiteSpace(passwordConfirm))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop the process.
            }

            // 2. Password Match Check.
            if (password != passwordConfirm)
            {
                MessageBox.Show("Şifreler eşleşmiyor. Lütfen kontrol ediniz.", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                frmSignUpPasswordTextBox.Clear(); // Clear password fields on mismatch.
                frmSignUpPasswordAgainTextBox.Clear();
                return;
            }

            // 3. Call the Service (isAdmin: false is saved).
            User? newUser = _authService.SignUp(username, password, isAdmin: false); // Try to sign up the new user.

            if (newUser != null) // If sign up is successful.
            {
                // Sign Up and Session are successful.
                MessageBox.Show($"Kayıt başarılı! Hoş geldiniz, {newUser.Username}.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message.

                // Show the main form and close the sign up form.
                this.DialogResult = DialogResult.OK; // Set dialog result to OK.
                this.Close(); // Close the form.
            }
            else // If sign up fails (e.g., username already exists).
            {
                // Sign Up Failed (Username probably already exists).
                MessageBox.Show("Bu kullanıcı adı zaten sistemde kayıtlı.", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show error message.
                frmSignUpUsernameTextBox.Focus(); // Set focus to username field.
            }
        }

        private void frmSignUpGoBackBtn_Click(object sender, EventArgs e) // Runs when the 'Cancel/Go Back' button is clicked.
        {
            this.DialogResult = DialogResult.Cancel; // Set dialog result to Cancel.
            this.Close(); // Close the form, returning to the login screen.
        }

        private void SignUpShowHideButton_Click(object sender, EventArgs e) // Runs when the 'Show/Hide Password' button is clicked.
        {
            // Check the current state: Is PasswordChar not '\0'? (Is the password currently hidden?)
            bool isPasswordHidden = frmSignUpPasswordTextBox.PasswordChar != '\0';
            bool isPasswordAgainHidden = frmSignUpPasswordAgainTextBox.PasswordChar != '\0';

            if (isPasswordHidden && isPasswordAgainHidden) // If both passwords are hidden.
            {
                // 1. SHOW the password: Set the character to null character to show it.
                frmSignUpPasswordTextBox.PasswordChar = '\0';
                frmSignUpPasswordAgainTextBox.PasswordChar = '\0';

                // 2. Change the button text.
                SignUpShowHideButton.Text = "Hide";
            }
            else // If the passwords are visible.
            {
                // 1. HIDE the password: Set the character to '●'.
                frmSignUpPasswordTextBox.PasswordChar = '●';
                frmSignUpPasswordAgainTextBox.PasswordChar = '●';

                // 2. Change the button text.
                SignUpShowHideButton.Text = "Show";
            }
        }
    }
}