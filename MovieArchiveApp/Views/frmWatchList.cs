using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Helpers;

namespace MovieArchiveApp.Views
{
    public partial class frmWatchList : Form
    {
        // 1. Servisimizi tanımlıyoruz
        private WatchListService _service;

        public frmWatchList()
        {
            InitializeComponent();
            _service = new WatchListService();
        }

        // 2. Form açılınca çalışacak metod
        private void frmWatchList_Load(object sender, EventArgs e)
        {
            ListeyiYukle();
        }

        // 3. Listeyi veritabanından çekip ekrana basan yardımcı metod
        private void ListeyiYukle()
        {
            // Giriş yapan kullanıcının ID'sini alıyoruz
            int userId = SessionManager.CurrentUserId;

            // Eğer kimse giriş yapmamışsa (ID = 0 ise) boşver
            if (userId == 0) return;

            // Servisten listeyi çek
            List<Movie> myMovies = _service.GetUserWatchlist(userId);

            // Gride bağla (Önce null yapıp temizliyoruz ki yenilensin)
            dgvList.DataSource = null;
            dgvList.DataSource = myMovies;

            // İstersen ID, Resim Yolu gibi gereksiz kolonları gizle:
            if (dgvList.Columns["Ratings"] != null) dgvList.Columns["Ratings"].Visible = false;
            if (dgvList.Columns["PosterPath"] != null) dgvList.Columns["PosterPath"].Visible = false;
            if (dgvList.Columns["Description"] != null) dgvList.Columns["Description"].Visible = false;
        }

        // 4. "Listeden Kaldır" butonu
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Listeden bir satır seçilmiş mi?
            if (dgvList.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek filmi seçin.");
                return;
            }

            // Seçili satırdan Film ID'sini al
            // (Hata almamak için ID hücresinin değerini int'e çeviriyoruz)
            int movieId = Convert.ToInt32(dgvList.CurrentRow.Cells["Id"].Value);
            int userId = SessionManager.CurrentUserId;

            // Kullanıcıya son bir kez soralım
            var cevap = MessageBox.Show("Bu filmi listeden kaldırmak istiyor musunuz?",
                                        "Onay",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                // Servisi çağırıp sildiriyoruz
                _service.RemoveFromWatchlist(userId, movieId);

                MessageBox.Show("Film listeden kaldırıldı.");

                // Listeyi yenile ki silinen film ekrandan gitsin
                ListeyiYukle();
            }
        }
    }
}