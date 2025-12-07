using Microsoft.Extensions.DependencyInjection;
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
        // Yeni Eklendi: Diğer formları DI ile yüklemek için IServiceProvider
        private readonly IServiceProvider _serviceProvider;

        // Constructor güncellendi: Şimdi IServiceProvider da alıyor
        public frmMain(IAuthService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider; // Service Provider'ı sakla

            CheckUserPermissions();
        }

        // YENİ METOT: Formları pnlContent içine yüklemek için
        private void LoadChildForm<T>() where T : Form
        {
            // Tüm mevcut kontrolleri (önceki formu) temizle
            // Varsayım: Ana içerik paneli adı pnlContent'tir. 
            // Eğer sizde farklı bir panel varsa (örneğin panel1), pnlContent yerine onu kullanın.
            if (pnlContent.Controls.Count > 0)
            {
                pnlContent.Controls.Clear();
            }

            // DI ile formu çöz (frmHome veya frmWatchList gibi)
            var form = _serviceProvider.GetRequiredService<T>();

            // Form ayarları
            form.TopLevel = false; // MDI yerine panel içine yerleştirmek için
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Panelin içine ekle ve göster
            pnlContent.Controls.Add(form);
            form.Show();
        }

        private void CheckUserPermissions()
        {
            // Bu metot, örneğin Admin paneli butonunu SessionManager'a göre göstermek için kullanılacaktır.
            // Şimdilik sadece Home butonu her zaman açık kalır.

            // YENİ: Admin butonu görünürlüğünü kontrol et
            // Varsayım: Admin paneli butonu 'btnOpenAdminPanel' adındadır.
            if (btnOpenAdminPanel != null)
            {
                btnOpenAdminPanel.Visible = SessionManager.IsAdmin;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // YENİ EKLENDİ: Form yüklendiğinde varsayılan olarak frmHome'u aç.
            // Bu, CRUD butonlarının görünmesini sağlayacaktır.
            LoadChildForm<frmHome>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Çıkış yapma mantığı
            _authService.Logout();
            MessageBox.Show("Başarıyla çıkış yaptınız.", "Çıkış", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CheckUserPermissions();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // İzleme Listesi Butonu Mantığı Güncellendi (DI kullanmak için)
        private void btnOpenWatchList_Click(object sender, EventArgs e)
        {
            // Artık DI ile yükleniyor.
            LoadChildForm<frmWatchList>();
        }

        // Yeni Eklendi: Ana Sayfa Butonu (frmHome'u tekrar yüklemek için)
        private void btnOpenHome_Click(object sender, EventArgs e)
        {
            LoadChildForm<frmHome>();
        }

        // YENİ EKLENDİ: Admin Paneli Butonu (frmAdmin'i yüklemek için)
        private void btnOpenAdminPanel_Click(object sender, EventArgs e)
        {
            // Sadece adminler için
            if (SessionManager.IsAdmin)
            {
                LoadChildForm<frmAdmin>();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}