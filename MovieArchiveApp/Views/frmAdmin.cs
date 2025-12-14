using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MovieArchiveApp.Views
{
    public partial class frmAdmin : Form
    {
        // 1. Bağımlılıkları tutmak için alanlar
        private readonly IMovieService _movieService; // This service manages movie data (CRUD).
        private readonly IServiceProvider _serviceProvider; // This helps to create forms using DI.

        // 2. Bağımlılık Enjeksiyonu için Constructor
        public frmAdmin(IMovieService movieService, IServiceProvider serviceProvider) // Constructor for the Admin form.
        {
            InitializeComponent(); // Initialize the form components.
            _movieService = movieService; // Set the movie service.
            _serviceProvider = serviceProvider; // Set the service provider.

            // Form açıldığında filmleri yükle
            this.Load += (s, e) => LoadMovies(); // Load movies when the form opens.
        }

        // 3. Film listesini yükleme metodu
        private void LoadMovies() // This function loads all movies into the data grid.
        {
            try // Try to load the movies.
            {
                // MovieService kullanarak tüm filmleri getir
                var movies = _movieService.GetAllMovies(); // Get all movies from the service.

                // DataGridView'a bağla
                dgvMovies.DataSource = movies; // Display the movies in the table.

                // DÜZELTME: Admin panelinde ID sütununu görünür yap
                if (dgvMovies.Columns.Contains("Id")) // Check for the ID column.
                {
                    dgvMovies.Columns["Id"].Visible = true; // Show the ID column.
                    dgvMovies.Columns["Id"].HeaderText = "ID"; // Set the header text.
                    dgvMovies.Columns["Id"].Width = 50; // Set the column width.
                }
                if (dgvMovies.Columns["Title"] != null) // Check for the Title column.
                    dgvMovies.Columns["Title"].HeaderText = "Film Adı"; // Set the header text.
                if (dgvMovies.Columns["Year"] != null) // Check for the Year column.
                    dgvMovies.Columns["Year"].HeaderText = "Yıl"; // Set the header text.
                if (dgvMovies.Columns["Genre"] != null) // Check for the Genre column.
                    dgvMovies.Columns["Genre"].HeaderText = "Tür"; // Set the header text.
                if (dgvMovies.Columns["Director"] != null) // Check for the Director column.
                    dgvMovies.Columns["Director"].HeaderText = "Yönetmen"; // Set the header text.
                if (dgvMovies.Columns["Description"] != null) // Check for the Description column.
                    dgvMovies.Columns["Description"].Visible = false; // Hide the column.
                if (dgvMovies.Columns["PosterUrl"] != null) // Check for the PosterUrl column.
                    dgvMovies.Columns["PosterUrl"].Visible = false; // Hide the column.
            }
            catch (Exception ex) // Catch an error if loading movies fails.
            {
                MessageBox.Show($"Filmler yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
            }
        }

        // 4. Yeni Film Ekleme İşlemi
        private void btnAdd_Click(object sender, EventArgs e) // This runs when the 'Add' button is clicked.
        {
            // DI container'dan frmMovieCrud'un ekleme modunu başlatan ctor'ını al
            var frmCrud = _serviceProvider.GetRequiredService<frmMovieCrud>(); // Open the Movie CRUD form for adding.

            // Form başarılı bir şekilde kapanırsa (DialogResult.OK), listeyi yenile
            if (frmCrud.ShowDialog() == DialogResult.OK) // Show the form and check the result.
            {
                LoadMovies(); // Refresh the movie list after successful adding.
            }
        }

        // 5. Film Düzenleme İşlemi
        private void btnEdit_Click(object sender, EventArgs e) // This runs when the 'Edit' button is clicked.
        {
            // DataGridView'de seçili bir film var mı kontrol et
            if (dgvMovies.SelectedRows.Count > 0) // Check if a row is selected.
            {
                // Seçili satırdan Movie nesnesini al
                var selectedMovie = dgvMovies.SelectedRows[0].DataBoundItem as Movie; // Get the selected movie object.

                if (selectedMovie != null) // Check if the movie object is valid.
                {
                    // Düzenleme modu için frmMovieCrud'un 3 parametreli constructor'ını kullan
                    var frmCrud = new frmMovieCrud(_movieService, selectedMovie); // Open the Movie CRUD form for editing.

                    if (frmCrud.ShowDialog() == DialogResult.OK) // Show the form and check the result.
                    {
                        LoadMovies(); // Refresh the movie list after successful editing.
                    }
                }
            }
            else // If no row is selected.
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz filmi listeden seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning message.
            }
        }

        // 6. Film Silme İşlemi
        private void btnDelete_Click(object sender, EventArgs e) // This runs when the 'Delete' button is clicked.
        {
            if (dgvMovies.SelectedRows.Count > 0) // Check if a row is selected.
            {
                var selectedMovie = dgvMovies.SelectedRows[0].DataBoundItem as Movie; // Get the selected movie object.

                if (selectedMovie != null) // Check if the movie object is valid.
                {
                    // Kullanıcıdan onay al
                    var dialogResult = MessageBox.Show( // Ask for confirmation.
                        $"{selectedMovie.Title} filmini silmek istediğinizden emin misiniz?", // Confirmation text.
                        "Silme Onayı", // Confirmation title.
                        MessageBoxButtons.YesNo, // Show Yes/No buttons.
                        MessageBoxIcon.Warning // Show a warning icon.
                    );

                    if (dialogResult == DialogResult.Yes) // If the user confirms deletion.
                    {
                        try // Try to delete the movie.
                        {
                            // Servis metodunu çağır
                            _movieService.DeleteMovie(selectedMovie.Id); // Call the service to delete the movie.
                            MessageBox.Show("Film başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message.
                            LoadMovies(); // Refresh the list.
                        }
                        catch (Exception ex) // Catch an error if deletion fails.
                        {
                            MessageBox.Show($"Silme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
                        }
                    }
                }
            }
            else // If no row is selected.
            {
                MessageBox.Show("Lütfen silmek istediğiniz filmi listeden seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning message.
            }
        }
    }
}