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
        private readonly IMovieService _movieService = null!; // This service handles movie data.
        private readonly IServiceProvider _serviceProvider = null!; // This helps to create other forms/services.

        // Tasarımcı (Designer) için boş constructor
        public frmHome() // Constructor for the designer.
        {
            InitializeComponent(); // Initialize the form components.
        }

        // Dependency Injection Constructor
        public frmHome(IMovieService movieService, IServiceProvider serviceProvider) : this() // Constructor with services.
        {
            _movieService = movieService; // Set the movie service.
            _serviceProvider = serviceProvider; // Set the service provider.

            ConfigureDataGridView(); // Set up the movie list table (Data Grid View).
            CheckUserPermissions(); // Check if the user is an admin.

            dgvMovies.CellDoubleClick += DgvMovies_CellDoubleClick; // Add event for double click on a movie.
        }

        private void DgvMovies_CellDoubleClick(object? sender, DataGridViewCellEventArgs e) // This runs when you double-click a movie. (Like clicking 'Details')
        {
            if (e.RowIndex < 0) return; // Do nothing if it's the header row.

            var selectedMovie = (Movie)dgvMovies.Rows[e.RowIndex].DataBoundItem; // Get the selected movie object.

            if (selectedMovie != null) // Check if a movie is selected.
            {
                var watchListService = _serviceProvider.GetRequiredService<WatchListService>(); // Get the Watch List service.

                using (var detailForm = new frmMovieDetail(selectedMovie, watchListService)) // Create the movie detail form.
                {
                    detailForm.ShowDialog(); // Show the detail form.
                }
            }
        }

        private void ConfigureDataGridView() // This function sets the style for the movie list table.
        {
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select the whole row.
            dgvMovies.MultiSelect = false; // Cannot select multiple rows.
            dgvMovies.ReadOnly = true; // The table is read-only.
        }

        private void CheckUserPermissions() // This function shows/hides admin buttons.
        {
            if (!SessionManager.IsAdmin) // If the user is not an admin.
            {
                if (btnAddMovie != null) btnAddMovie.Visible = false; // Hide the 'Add Movie' button.
                if (btnEditMovie != null) btnEditMovie.Visible = false; // Hide the 'Edit Movie' button.
                if (btnDeleteMovie != null) btnDeleteMovie.Visible = false; // Hide the 'Delete Movie' button.
            }
        }

        private void LoadMovies(string? searchQuery = null) // This function gets the movies and shows them in the table.
        {
            // _movieService tasarım modunda null olabilir, kontrol ediyoruz.
            if (_movieService == null) return; // Stop if the movie service is null.

            List<Movie> movies; // Declare a list for movies.

            if (string.IsNullOrWhiteSpace(searchQuery)) // If there is no search query.
            {
                movies = _movieService.GetAllMovies(); // Get all movies.
            }
            else // If there is a search query.
            {
                movies = _movieService.SearchMovies(searchQuery); // Search for movies.
            }

            dgvMovies.DataSource = movies; // Set the movie list as the data source for the table.

            // Kolon Ayarları (Column Settings)
            if (dgvMovies.Columns["Id"] != null) // Check for ID column.
            {
                dgvMovies.Columns["Id"].Visible = true; // Show the ID column.
                dgvMovies.Columns["Id"].HeaderText = "ID"; // Set column header text.
                dgvMovies.Columns["Id"].Width = 50; // Set column width.
            }
            if (dgvMovies.Columns["Title"] != null) // Check for Title column.
                dgvMovies.Columns["Title"].HeaderText = "Film Adı"; // Set column header text.

            // ReleaseYear veya Year kontrolü
            if (dgvMovies.Columns["ReleaseYear"] != null) // Check for ReleaseYear column.
                dgvMovies.Columns["ReleaseYear"].HeaderText = "Yıl"; // Set column header text.
            if (dgvMovies.Columns["Year"] != null) // Check for Year column.
                dgvMovies.Columns["Year"].HeaderText = "Yıl"; // Set column header text.

            if (dgvMovies.Columns["Genre"] != null) // Check for Genre column.
                dgvMovies.Columns["Genre"].HeaderText = "Tür"; // Set column header text.
            if (dgvMovies.Columns["Director"] != null) // Check for Director column.
                dgvMovies.Columns["Director"].HeaderText = "Yönetmen"; // Set column header text.

            // Gizlenecek Kolonlar (Hidden Columns)
            if (dgvMovies.Columns["Description"] != null) // Hide Description column.
                dgvMovies.Columns["Description"].Visible = false;
            if (dgvMovies.Columns["PosterPath"] != null) // Hide PosterPath column.
                dgvMovies.Columns["PosterPath"].Visible = false;
            if (dgvMovies.Columns["PosterUrl"] != null) // Hide PosterUrl column.
                dgvMovies.Columns["PosterUrl"].Visible = false;
            if (dgvMovies.Columns["Ratings"] != null) // Hide Ratings column.
                dgvMovies.Columns["Ratings"].Visible = false;
        }

        private void frmHome_Load(object? sender, EventArgs e) // This function runs when the Home form loads.
        {
            if (_movieService != null) // Check if the service is ready.
            {
                LoadMovies(); // Load all movies into the table.
            }
        }

        // --- CRUD OPERASYONLARI (CRUD OPERATIONS) ---

        private void btnAddMovie_Click(object sender, EventArgs e) // This is the click event for the 'Add Movie' button.
        {
            using (var frm = new frmMovieCrud(_movieService)) // Open the Movie CRUD form to add a new movie.
            {
                if (frm.ShowDialog() == DialogResult.OK) // If the movie is added successfully.
                {
                    LoadMovies(); // Refresh the movie list.
                }
            }
        }

        private void btnEditMovie_Click(object sender, EventArgs e) // This is the click event for the 'Edit Movie' button.
        {
            if (dgvMovies.SelectedRows.Count == 0) // Check if a row is selected.
            {
                MessageBox.Show("Lütfen düzenlemek için bir film seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning message.
                return; // Stop the function.
            }

            try // Try to edit the movie.
            {
                var selectedMovie = (Movie)dgvMovies.SelectedRows[0].DataBoundItem; // Get the selected movie object.

                using (var frm = new frmMovieCrud(_movieService, selectedMovie)) // Open the Movie CRUD form with the selected movie data.
                {
                    if (frm.ShowDialog() == DialogResult.OK) // If the movie is edited successfully.
                    {
                        LoadMovies(); // Refresh the movie list.
                    }
                }
            }
            catch (Exception ex) // Catch an error if editing fails.
            {
                MessageBox.Show($"Filmi düzenlerken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
            }
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e) // This is the click event for the 'Delete Movie' button.
        {
            if (dgvMovies.SelectedRows.Count == 0) // Check if a row is selected.
            {
                MessageBox.Show("Lütfen silmek için bir film seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning message.
                return; // Stop the function.
            }

            var confirmResult = MessageBox.Show("Seçili filmi silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Ask for confirmation.

            if (confirmResult == DialogResult.Yes) // If the user confirms to delete.
            {
                try // Try to delete the movie.
                {
                    var selectedMovie = (Movie)dgvMovies.SelectedRows[0].DataBoundItem; // Get the selected movie object.
                    int movieId = selectedMovie.Id; // Get the movie ID.

                    _movieService.DeleteMovie(movieId); // Call the service to delete the movie.

                    MessageBox.Show("Film başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message.

                    LoadMovies(); // Refresh the movie list.
                }
                catch (Exception ex) // Catch an error if deleting fails.
                {
                    MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) // This is the click event for the 'Search' button.
        {
        } // This function is empty now.
    }
}