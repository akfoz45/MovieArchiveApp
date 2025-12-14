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
        private readonly IAuthService _authService; // This is the service for user login/logout.
        private readonly IServiceProvider _serviceProvider; // This helps to get forms and services.

        // Constructor güncellendi: Şimdi IServiceProvider da alıyor
        public frmMain(IAuthService authService, IServiceProvider serviceProvider) // This is the form constructor.
        {
            InitializeComponent(); // This starts the form components.
            _authService = authService; // Set the authentication service.
            _serviceProvider = serviceProvider; // Save the service provider.

            CheckUserPermissions(); // Check if the user is an admin or a regular user.
        }

        // YENİ METOT: Formları pnlContent içine yüklemek için
        private void LoadChildForm<T>() where T : Form // This function opens a form inside the main panel.
        {
            // Tüm mevcut kontrolleri (önceki formu) temizle
            // Varsayım: Ana içerik paneli adı pnlContent'tir. 
            // Eğer sizde farklı bir panel varsa (örneğin panel1), pnlContent yerine onu kullanın.
            if (pnlContent.Controls.Count > 0) // Check if there are other controls in the panel.
            {
                pnlContent.Controls.Clear(); // Clear the panel. Remove the old form.
            }

            // DI ile formu çöz (frmHome veya frmWatchList gibi)
            var form = _serviceProvider.GetRequiredService<T>(); // Get the new form using Dependency Injection (DI).

            // Form ayarları
            form.TopLevel = false; // Set the form not as top level.
            form.FormBorderStyle = FormBorderStyle.None; // Remove the border of the form.
            form.Dock = DockStyle.Fill; // Fill the parent panel with the form.

            // Panelin içine ekle ve göster
            pnlContent.Controls.Add(form); // Add the form to the panel.
            form.Show(); // Show the new form.
        }

        private void CheckUserPermissions() // This function checks user roles.
        {
            // Bu metot, örneğin Admin paneli butonunu SessionManager'a göre göstermek için kullanılacaktır.
            // Şimdilik sadece Home butonu her zaman açık kalır.

            if (btnOpenAdminPanel != null) // Check if the Admin Panel button exists.
            {
                btnOpenAdminPanel.Visible = SessionManager.IsAdmin; // Show the button if the user is an admin.
            }
        }

        private void frmMain_Load(object sender, EventArgs e) // This function runs when the main form loads.
        {
            LoadChildForm<frmHome>(); // Load the Home form first.
        }

        private void button1_Click(object sender, EventArgs e) // This is the click event for the Logout button.
        {
            // Çıkış yapma mantığı
            _authService.Logout(); // Call the service to log out the user.
            MessageBox.Show("Başarıyla çıkış yaptınız.", "Çıkış", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message.
            CheckUserPermissions(); // Re-check permissions (not necessary here but good practice).
            this.Close(); // Close the main form.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // İzleme Listesi Butonu Mantığı Güncellendi (DI kullanmak için)
        private void btnOpenWatchList_Click(object sender, EventArgs e) // This is the click event for the Watch List button.
        {
            // Artık DI ile yükleniyor.
            LoadChildForm<frmWatchList>(); // Open the Watch List form.
        }

        private void btnOpenHome_Click(object sender, EventArgs e) // This is the click event for the Home button.
        {
            LoadChildForm<frmHome>(); // Open the Home form.
        }

        private void btnOpenAdminPanel_Click(object sender, EventArgs e) // This is the click event for the Admin Panel button.
        {
            // Sadece adminler için
            if (SessionManager.IsAdmin) // Check if the user is an administrator.
            {
                LoadChildForm<frmAdmin>(); // If admin, open the Admin form.
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}