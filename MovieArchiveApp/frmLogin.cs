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
        // The 'readonly' keyword means these values can only be set once in the constructor.
        private readonly IAuthService _authService; // This service handles login and registration.
        private readonly frmMain _frmMain; // This is the main application form.
        public frmLogin(IAuthService authService, frmMain frmMain) // Constructor for the Login form.
        {
            InitializeComponent(); // Initialize the form components.
            // NOTE: This area will be filled by Member 2.
            // AuthService will be injected here and buttons will be connected.
            _authService = authService; // Set the authentication service.
            _frmMain = frmMain; // Set the main form instance.

            // Make the form open in the center of the screen.
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

        private void LogInButton_Click(object sender, EventArgs e) // Runs when the 'Log In' button is clicked.
        {
            // 1. Get the data.
            string username = frmLoginUsernameTextBox.Text.Trim(); // Get username text.
            string password = frmLoginPasswordTextBox.Text; // Get password text.

            // 2. Pre-Check: Check for empty fields.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizi giriniz.", "Giriş Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop the process here.
            }

            // 3. Call the Service: Authenticate the user.
            // _authService is the AuthService instance we got from the constructor.
            // This method checks the database and returns the user if login is successful.
            User? user = _authService.Login(username, password); // Try to log in.

            if (user != null) // If login is successful.
            {
                // 4a. Login Success: Session is started (SessionManager is called in AuthService).
                MessageBox.Show($"Hoş geldiniz, {user.Username}!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a welcome message.

                // Show the Main Form and hide this form (Login).
                _frmMain.Show(); // Show the main form.
                this.Hide(); // Hide the login form.

                // Clear the input fields for security and cleanup.
                frmLoginUsernameTextBox.Clear();
                frmLoginPasswordTextBox.Clear();
            }
            else // If login fails.
            {
                // 4b. Login Fail: Username or password is wrong.
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
                frmLoginPasswordTextBox.Clear(); // Clear the password field.
            }
        }

        private void frmLoginGoToSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Runs when the 'Sign Up' link is clicked.
        {
            this.Hide(); // Hide the login form.

            using (var signUpForm = new frmSignUp(_authService, _frmMain)) // Create the Sign Up form.
            {
                if (signUpForm.ShowDialog() == DialogResult.OK) // Show the Sign Up form and check the result.
                {
                    // Sign up successful -> Bring back the Login form.
                    this.Show();
                }
                else
                {
                    // Sign up cancelled -> Bring back the Login form.
                    this.Show();
                }
            }
        }

        private void ShowHideButton_Click(object sender, EventArgs e) // Runs when the 'Show/Hide' button is clicked.
        {
            // Check the current state: Is PasswordChar empty? (Is the password currently visible?)
            // If the PasswordChar property is not '\0', the password is hidden.
            bool isPasswordHidden = frmLoginPasswordTextBox.PasswordChar != '\0';

            if (isPasswordHidden) // If the password is hidden.
            {
                // 1. SHOW the password: Set the character to null character to show it.
                frmLoginPasswordTextBox.PasswordChar = '\0';

                // 2. Change the button text.
                LogInShowHideButton.Text = "Hide";
            }
            else // If the password is visible.
            {
                // 1. HIDE the password: Set the character to '*'.
                // '*' is often used for security.
                frmLoginPasswordTextBox.PasswordChar = '●';

                // 2. Change the button text.
                LogInShowHideButton.Text = "Show";
            }
        }

        private void frmLoginPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}