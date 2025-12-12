using Microsoft.Extensions.DependencyInjection;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Helpers;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MovieArchiveApp.Views
{
    public partial class frmHome : Form
    {
        private readonly IMovieService _movieService = null!;
        private readonly IServiceProvider _serviceProvider = null!;

        // Tasarımcı (Designer) için boş constructor
        public frmHome()
        {
            InitializeComponent();
        }

        // Dependency Injection Constructor
        public frmHome(IMovieService movieService, IServiceProvider serviceProvider) : this()
        {
            _movieService = movieService;
            _serviceProvider = serviceProvider;

            ConfigureDataGridView();
            CheckUserPermissions();

            dgvMovies.CellDoubleClick += DgvMovies_CellDoubleClick;
        }

        private void DgvMovies_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedMovie = (Movie)dgvMovies.Rows[e.RowIndex].DataBoundItem;

            if (selectedMovie != null)
            {
                var watchListService = _serviceProvider.GetRequiredService<WatchListService>();

                using (var detailForm = new frmMovieDetail(selectedMovie, watchListService))
                {
                    detailForm.ShowDialog();
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovies.MultiSelect = false;
            dgvMovies.ReadOnly = true;
        }

        private void CheckUserPermissions()
        {
            if (!SessionManager.IsAdmin)
            {
                if (btnAddMovie != null) btnAddMovie.Visible = false;
                if (btnEditMovie != null) btnEditMovie.Visible = false;
                if (btnDeleteMovie != null) btnDeleteMovie.Visible = false;
            }
        }

        private void LoadMovies(string? searchQuery = null)
        {
            // _movieService tasarım modunda null olabilir, kontrol ediyoruz.
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

            // Kolon Ayarları
            if (dgvMovies.Columns["Id"] != null)
            {
                dgvMovies.Columns["Id"].Visible = true;
                dgvMovies.Columns["Id"].HeaderText = "ID";
                dgvMovies.Columns["Id"].Width = 50;
            }
            if (dgvMovies.Columns["Title"] != null)
                dgvMovies.Columns["Title"].HeaderText = "Film Adı";

            // ReleaseYear veya Year kontrolü
            if (dgvMovies.Columns["ReleaseYear"] != null)
                dgvMovies.Columns["ReleaseYear"].HeaderText = "Yıl";
            if (dgvMovies.Columns["Year"] != null)
                dgvMovies.Columns["Year"].HeaderText = "Yıl";

            if (dgvMovies.Columns["Genre"] != null)
                dgvMovies.Columns["Genre"].HeaderText = "Tür";
            if (dgvMovies.Columns["Director"] != null)
                dgvMovies.Columns["Director"].HeaderText = "Yönetmen";

            // Gizlenecek Kolonlar
            if (dgvMovies.Columns["Description"] != null)
                dgvMovies.Columns["Description"].Visible = false;
            if (dgvMovies.Columns["PosterPath"] != null)
                dgvMovies.Columns["PosterPath"].Visible = false;
            if (dgvMovies.Columns["PosterUrl"] != null)
                dgvMovies.Columns["PosterUrl"].Visible = false;
            if (dgvMovies.Columns["Ratings"] != null)
                dgvMovies.Columns["Ratings"].Visible = false;
        }

        private void frmHome_Load(object? sender, EventArgs e)
        {
            if (_movieService != null)
            {
                LoadMovies();
            }
        }

        // --- CRUD OPERASYONLARI ---

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMovies(txtSearch.Text);
        }
    }
}