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
            int userId = SessionManager.CurrentUserId;
            if (userId == 0) return;

            List<Movie> myMovies = _service.GetUserWatchlist(userId);

            dgvList.DataSource = null;
            dgvList.DataSource = myMovies;

            // Gereksiz kolonları gizle
            if (dgvList.Columns["Ratings"] != null) dgvList.Columns["Ratings"].Visible = false;
            if (dgvList.Columns["PosterPath"] != null) dgvList.Columns["PosterPath"].Visible = false;
            if (dgvList.Columns["Description"] != null) dgvList.Columns["Description"].Visible = false;
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
    }
}