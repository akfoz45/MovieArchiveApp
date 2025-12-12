using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System;
using System.Windows.Forms;

namespace MovieArchiveApp.Views
{
    public partial class frmMovieCrud : Form
    {
        private readonly IMovieService _movieService = null!;
        private Movie _movieToEdit;

        public frmMovieCrud()
        {
            InitializeComponent();
            _movieToEdit = new Movie();
        }

        public frmMovieCrud(IMovieService movieService) : this() 
        {
            _movieService = movieService;
            this.Text = "Yeni Film Ekle";
            
            _movieToEdit = new Movie();
        }

        public frmMovieCrud(IMovieService movieService, Movie movie) : this(movieService)
        {
            _movieToEdit = movie;
            this.Text = "Film Düzenle: " + movie.Title;
            LoadMovieData(movie);
        }

        private void LoadMovieData(Movie movie)
        {
            txtTitle.Text = movie.Title;
            txtDescription.Text = movie.Description;
            txtGenre.Text = movie.Genre;
            txtYear.Text = movie.ReleaseYear.ToString();
            txtDirector.Text = movie.Director;
            txtPosterUrl.Text = movie.PosterPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_movieService == null) return;

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || !int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Lütfen Film Adı ve Yayın Yılı alanlarını doğru girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isUpdate = _movieToEdit != null && _movieToEdit.Id > 0;

            try
            {
                if (_movieToEdit == null) _movieToEdit = new Movie();

                _movieToEdit.Title = txtTitle.Text;
                _movieToEdit.Description = txtDescription.Text;
                _movieToEdit.Genre = txtGenre.Text;
                _movieToEdit.ReleaseYear = year;
                _movieToEdit.Director = txtDirector.Text;
                _movieToEdit.PosterPath = txtPosterUrl.Text;

                if (isUpdate)
                {
                    _movieService.UpdateMovie(_movieToEdit);
                    MessageBox.Show("Film başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _movieService.AddMovie(_movieToEdit);
                    MessageBox.Show("Film başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}