using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System;
using System.Windows.Forms;

namespace MovieArchiveApp.Views
{
    public partial class frmMovieCrud : Form
    {
        private readonly IMovieService _movieService = null!; // This service handles movie data operations.
        private Movie _movieToEdit; // This object holds the movie data for editing or adding.

        public frmMovieCrud() // Default constructor.
        {
            InitializeComponent(); // Initialize the form components.
            _movieToEdit = new Movie(); // Create a new empty movie object.
        }

        public frmMovieCrud(IMovieService movieService) : this() // Constructor for adding a new movie.
        {
            _movieService = movieService; // Set the movie service.
            this.Text = "Yeni Film Ekle"; // Set the form title to "Add New Movie".

            _movieToEdit = new Movie(); // Ensure it's a new Movie object.
        }

        public frmMovieCrud(IMovieService movieService, Movie movie) : this(movieService) // Constructor for editing an existing movie.
        {
            _movieToEdit = movie; // Set the movie to the selected movie.
            this.Text = "Film Düzenle: " + movie.Title; // Set the form title to "Edit Movie: [Movie Title]".
            LoadMovieData(movie); // Load the movie's current data to the form fields.
        }

        private void LoadMovieData(Movie movie) // This function puts movie data into text fields.
        {
            txtTitle.Text = movie.Title; // Load movie title.
            txtDescription.Text = movie.Description; // Load movie description.
            txtGenre.Text = movie.Genre; // Load movie genre.
            txtYear.Text = movie.ReleaseYear.ToString(); // Load release year.
            txtDirector.Text = movie.Director; // Load movie director.
            txtPosterUrl.Text = movie.PosterPath; // Load poster URL/Path.
        }

        private void btnSave_Click(object sender, EventArgs e) // This function runs when the 'Save' button is clicked.
        {
            if (_movieService == null) return; // Stop if the movie service is null.

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || !int.TryParse(txtYear.Text, out int year)) // Check if title and year are entered correctly.
            {
                MessageBox.Show("Lütfen Film Adı ve Yayın Yılı alanlarını doğru girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show error message.
                return; // Stop the function.
            }

            bool isUpdate = _movieToEdit != null && _movieToEdit.Id > 0; // Check if it is an update operation.

            try // Try to save the movie data.
            {
                if (_movieToEdit == null) _movieToEdit = new Movie(); // Create a new movie object if it is null.

                _movieToEdit.Title = txtTitle.Text; // Set the title from the text box.
                _movieToEdit.Description = txtDescription.Text; // Set the description.
                _movieToEdit.Genre = txtGenre.Text; // Set the genre.
                _movieToEdit.ReleaseYear = year; // Set the release year.
                _movieToEdit.Director = txtDirector.Text; // Set the director.
                _movieToEdit.PosterPath = txtPosterUrl.Text; // Set the poster path.

                if (isUpdate) // If it is an update.
                {
                    _movieService.UpdateMovie(_movieToEdit); // Update the movie in the database.
                    MessageBox.Show("Film başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message for update.
                }
                else // If it is a new movie (add).
                {
                    _movieService.AddMovie(_movieToEdit); // Add the new movie to the database.
                    MessageBox.Show("Film başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message for add.
                }

                this.DialogResult = DialogResult.OK; // Set the form result to OK.
                this.Close(); // Close the form.
            }
            catch (Exception ex) // Catch an error if saving fails.
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message.
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // This function runs when the 'Cancel' button is clicked.
        {
            this.Close(); // Close the form without saving.
        }

    }
}