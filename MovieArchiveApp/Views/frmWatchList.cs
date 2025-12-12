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
        // 'null!' diyerek derleyiciye "biliyorum boş görünüyor ama dolacak" diyoruz.
        private readonly WatchListService _service = null!;

        // 1. Parametresiz Constructor (Designer Hatası Almamak İçin)
        public frmWatchList()
        {
            InitializeComponent();
            this.Load += frmWatchList_Load;
        }

        // 2. DI Constructor (Program.cs burayı kullanır)
        public frmWatchList(WatchListService service) : this()
        {
            _service = service;
        }

        private void frmWatchList_Load(object? sender, EventArgs e)
        {
            // Eğer _service null ise (tasarım modunda) işlem yapma
            if (_service != null)
            {
                ListeyiYukle();
            }
        }

        private void ListeyiYukle()
        {
            // 1. ADIM: Kullanıcı Giriş Kontrolü
            int userId = SessionManager.CurrentUserId;

            // DEBUG: ID Kontrolü (Sorun çözülünce bu satırı silebilirsiniz)
            // MessageBox.Show($"Giriş yapan Kullanıcı ID: {userId}"); 

            if (userId == 0)
            {
                MessageBox.Show("Hata: Kullanıcı girişi algılanamadı (ID=0). Lütfen çıkış yapıp tekrar giriş yapınız.");
                return;
            }

            // 2. ADIM: Veritabanından Çekme
            List<Movie> myMovies = _service.GetUserWatchlist(userId);

            // DEBUG: Veri Sayısı Kontrolü (Sorun çözülünce bu satırı silebilirsiniz)
            if (myMovies.Count == 0)
            {
                MessageBox.Show("Sorgu çalıştı ancak veritabanından hiç film gelmedi. Kayıt yapılmamış olabilir.");
            }

            // 3. ADIM: Ekrana Basma
            dgvList.DataSource = null;
            dgvList.DataSource = myMovies;

            // Sadece veri varsa kolon gizleme yap (Hata almamak için)
            if (myMovies.Count > 0)
            {
                if (dgvList.Columns["Ratings"] != null) dgvList.Columns["Ratings"].Visible = false;
                if (dgvList.Columns["PosterPath"] != null) dgvList.Columns["PosterPath"].Visible = false;
                if (dgvList.Columns["Description"] != null) dgvList.Columns["Description"].Visible = false;
                // İlişkisel alanları da gizleyelim
                if (dgvList.Columns["WatchLists"] != null) dgvList.Columns["WatchLists"].Visible = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek filmi seçin.");
                return;
            }

            // Hücre değerini güvenli şekilde al
            if (dgvList.CurrentRow.Cells["Id"].Value == null) return;

            int movieId = Convert.ToInt32(dgvList.CurrentRow.Cells["Id"].Value);
            int userId = SessionManager.CurrentUserId;

            var cevap = MessageBox.Show("Bu filmi listeden kaldırmak istiyor musunuz?",
                                        "Onay",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                _service.RemoveFromWatchlist(userId, movieId);
                MessageBox.Show("Film listeden kaldırıldı.");
                ListeyiYukle();
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}