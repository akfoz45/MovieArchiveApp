// frmMain.cs
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Helpers;
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

namespace MovieArchiveApp.Views
{
    public partial class frmMain : Form
    {

        private readonly IAuthService _authService;

        public frmMain(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;

            CheckUserPermissions();
        }

        private void CheckUserPermissions()
        {
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Oturumu kapatma servisini çağır
            // Bu, SessionManager.CurrentUser'ı null yapar.
            _authService.Logout();

            MessageBox.Show("Başarıyla çıkış yaptınız.", "Çıkış", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 2. Formdaki yetki ve içeriği temizle
            CheckUserPermissions(); // Admin butonunu hemen gizler.

            // 3. Ana Formu gizle. 
            // Program.cs'teki Host döngüsü, bu form kapandığında frmLogin'i tekrar açacaktır.
            this.Close();

            // Not: Eğer uygulamanın tamamen kapanmasını istiyorsan, this.Close(); kullanabilirsin.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
