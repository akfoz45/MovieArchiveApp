using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MovieArchiveApp.Views
{
    public partial class frmMovieDetail : Form
    {
        private readonly WatchListService _watchListService = null!;
        private Movie _movie = null!;

        private bool _isInWatchList = false;

        public frmMovieDetail()
        {
            InitializeComponent();
        }

        // Asıl Constructor (Sadece Film ve WatchListService alıyor)
        public frmMovieDetail(Movie movie, WatchListService watchListService) : this()
        {
            _movie = movie;
            _watchListService = watchListService;

            LoadMovieDetails();
        }

        private void LoadMovieDetails()
        {
            // 1. Film Bilgilerini Ekrana Bas
            lblTitle.Text = _movie.Title;
            lblDirector.Text = $"Yönetmen: {_movie.Director}";
            lblGenre.Text = $"Tür: {_movie.Genre}";
            lblYear.Text = $"Yıl: {_movie.ReleaseYear}";
            txtDescription.Text = _movie.Description;

            // Poster yükleme ve Puanlama kısımları kaldırıldı.

            // 2. Kullanıcı Giriş Kontrolü ve Liste Durumu
            int userId = SessionManager.CurrentUserId;
            if (userId != 0)
            {
                // Kullanıcının listesinde bu film var mı kontrol et
                var userList = _watchListService.GetUserWatchlist(userId);
                _isInWatchList = userList.Any(m => m.Id == _movie.Id);
                UpdateWatchListButton();
            }
            else
            {
                // Giriş yapılmadıysa butonu gizle veya pasif yap
                btnWatchList.Enabled = false;
                btnWatchList.Text = "Giriş Yapılmadı";
            }
        }

        private void UpdateWatchListButton()
        {
            if (_isInWatchList)
            {
                btnWatchList.Text = "Listeden Çıkar";
                btnWatchList.BackColor = Color.IndianRed;
                btnWatchList.ForeColor = Color.White;
            }
            else
            {
                btnWatchList.Text = "İzleme Listesine Ekle";
                btnWatchList.BackColor = Color.MediumSeaGreen;
                btnWatchList.ForeColor = Color.White;
            }
        }

        private void btnWatchList_Click(object sender, EventArgs e)
        {
            int userId = SessionManager.CurrentUserId;
            if (userId == 0) return;

            if (_isInWatchList)
            {
                // Listeden Çıkar
                _watchListService.RemoveFromWatchlist(userId, _movie.Id);
                _isInWatchList = false;
                MessageBox.Show("Film listenizden çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Listeye Ekle
                bool success = _watchListService.AddToWatchlist(userId, _movie.Id);
                if (success)
                {
                    _isInWatchList = true;
                    MessageBox.Show("Film izleme listenize eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            UpdateWatchListButton();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}