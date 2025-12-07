using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MovieArchiveApp.Services.Helpers; // SessionManager için eklendi

namespace MovieArchiveApp.Views
{
    public partial class frmHome : Form
    {
        private readonly IMovieService _movieService;

        // YENİ EKLENEN: Tasarımcının (Designer) Formu Başlatması İçin Parametresiz Constructor
        public frmHome()
        {
            // Bu constructor sadece tasarım zamanında kullanılır.
            InitializeComponent();
        }

        // Dependency Injection için Constructor (Çalışma Zamanında Kullanılan)
        public frmHome(IMovieService movieService) : this() // Parametresiz constructor'ı çağırır
        {
            _movieService = movieService;
            ConfigureDataGridView();
            CheckUserPermissions(); // YENİ: İzinleri kontrol et
        }

        // DataGridView ayarlarını yapar.
        private void ConfigureDataGridView()
        {
            // Kolon ayarlamaları burada yapılır.
        }

        // YENİ METOT: Kullanıcı yetkilerine göre CRUD butonlarını ayarlar
        private void CheckUserPermissions()
        {
            // Admin değilse CRUD butonlarını gizle.
            if (!SessionManager.IsAdmin)
            {
                if (btnAddMovie != null) btnAddMovie.Visible = false;
                if (btnEditMovie != null) btnEditMovie.Visible = false;
                if (btnDeleteMovie != null) btnDeleteMovie.Visible = false;
            }
        }

        // Filmleri yükleyen merkezi metot (READ operasyonu)
        private void LoadMovies(string searchQuery = null)
        {
            // Kontrol eklendi: Eğer tasarım anındaysak, _movieService null olabilir.
            if (_movieService == null) return;

            List<Movie> movies;

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                movies = _movieService.GetAllMovies();
            }
            else
            {
                movies = _movieService.SearchMovies(searchQuery);
            }

            dgvMovies.DataSource = movies;

            // Kolon başlıklarını kullanıcı dostu hale getir
            if (dgvMovies.Columns["Id"] != null)
            {
                dgvMovies.Columns["Id"].Visible = true;
                dgvMovies.Columns["Id"].HeaderText = "ID";
                dgvMovies.Columns["Id"].Width = 50;
            }
            if (dgvMovies.Columns["Title"] != null)
                dgvMovies.Columns["Title"].HeaderText = "Film Adı";
            if (dgvMovies.Columns["Year"] != null)
                dgvMovies.Columns["Year"].HeaderText = "Yıl";
            if (dgvMovies.Columns["Genre"] != null)
                dgvMovies.Columns["Genre"].HeaderText = "Tür";
            if (dgvMovies.Columns["Director"] != null)
                dgvMovies.Columns["Director"].HeaderText = "Yönetmen";
            if (dgvMovies.Columns["Description"] != null)
                dgvMovies.Columns["Description"].Visible = false;
            if (dgvMovies.Columns["PosterUrl"] != null)
                dgvMovies.Columns["PosterUrl"].Visible = false;
        }

        // Form ilk yüklendiğinde filmleri getir.
        private void frmHome_Load(object sender, EventArgs e)
        {
            if (_movieService != null)
            {
                LoadMovies();
            }
        }

        // --- CRUD OPERASYONLARI ---

        // Yeni Film Ekle butonu (CREATE operasyonu)
        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            using (var frm = new frmMovieCrud(_movieService))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadMovies();
                }
            }
        }

        // Filmi Düzenle butonu (UPDATE operasyonu)
        private void btnEditMovie_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek için bir film seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedMovie = (Movie)dgvMovies.SelectedRows[0].DataBoundItem;

                using (var frm = new frmMovieCrud(_movieService, selectedMovie))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadMovies();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filmi düzenlerken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filmi Sil butonu (DELETE operasyonu)
        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir film seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Seçili filmi silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var selectedMovie = (Movie)dgvMovies.SelectedRows[0].DataBoundItem;
                    int movieId = selectedMovie.Id;

                    _movieService.DeleteMovie(movieId);

                    MessageBox.Show("Film başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadMovies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Arama butonu (READ/Search operasyonu)
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMovies(txtSearch.Text);
        }
    }
}