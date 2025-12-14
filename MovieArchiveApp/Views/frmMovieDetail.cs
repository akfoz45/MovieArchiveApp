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
        private readonly WatchListService _watchListService = null!; // This service manages the user's watch list.
        private Movie _movie = null!; // This object holds the details of the current movie.

        private bool _isInWatchList = false; // This flag checks if the movie is on the list.

        public frmMovieDetail() // Default constructor.
        {
            InitializeComponent(); // Initialize the form components.
        }

        // Asıl Constructor (Sadece Film ve WatchListService alıyor)
        public frmMovieDetail(Movie movie, WatchListService watchListService) : this() // Constructor for showing movie details.
        {
            _movie = movie; // Set the current movie object.
            _watchListService = watchListService; // Set the Watch List service.

            LoadMovieDetails(); // Load and display the movie information.
        }

        private void LoadMovieDetails() // This function loads movie data to the screen.
        {
            // 1. Film Bilgilerini Ekrana Bas
            lblTitle.Text = _movie.Title; // Set the movie title.
            lblDirector.Text = $"Yönetmen: {_movie.Director}"; // Set the movie director.
            lblGenre.Text = $"Tür: {_movie.Genre}"; // Set the movie genre.
            lblYear.Text = $"Yıl: {_movie.ReleaseYear}"; // Set the movie release year.
            txtDescription.Text = _movie.Description; // Set the movie description.

            // Poster yükleme ve Puanlama kısımları kaldırıldı.

            // 2. Kullanıcı Giriş Kontrolü ve Liste Durumu
            int userId = SessionManager.CurrentUserId; // Get the ID of the logged-in user.
            if (userId != 0) // Check if a user is logged in.
            {
                // Kullanıcının listesinde bu film var mı kontrol et
                var userList = _watchListService.GetUserWatchlist(userId); // Get the user's watch list.
                _isInWatchList = userList.Any(m => m.Id == _movie.Id); // Check if the movie is in the list.
                UpdateWatchListButton(); // Update the button text and color.
            }
            else // If no user is logged in.
            {
                // Giriş yapılmadıysa butonu gizle veya pasif yap
                btnWatchList.Enabled = false; // Disable the button.
                btnWatchList.Text = "Giriş Yapılmadı"; // Change button text.
            }
        }

        private void UpdateWatchListButton() // This function changes the button's appearance.
        {
            if (_isInWatchList) // If the movie is in the watch list.
            {
                btnWatchList.Text = "Listeden Çıkar"; // Set button text to "Remove from List".
                btnWatchList.BackColor = Color.IndianRed; // Set background color to red.
                btnWatchList.ForeColor = Color.White; // Set text color to white.
            }
            else // If the movie is NOT in the watch list.
            {
                btnWatchList.Text = "İzleme Listesine Ekle"; // Set button text to "Add to Watch List".
                btnWatchList.BackColor = Color.MediumSeaGreen; // Set background color to green.
                btnWatchList.ForeColor = Color.White; // Set text color to white.
            }
        }

        private void btnWatchList_Click(object sender, EventArgs e) // This function runs when the Watch List button is clicked.
        {
            int userId = SessionManager.CurrentUserId; // Get the user ID.
            if (userId == 0) return; // Stop if user ID is 0 (not logged in).

            if (_isInWatchList) // If the movie is currently in the list.
            {
                // Listeden Çıkar
                _watchListService.RemoveFromWatchlist(userId, _movie.Id); // Remove the movie from the watch list.
                _isInWatchList = false; // Set the flag to false.
                MessageBox.Show("Film listenizden çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message.
            }
            else // If the movie is NOT in the list.
            {
                // Listeye Ekle
                bool success = _watchListService.AddToWatchlist(userId, _movie.Id); // Add the movie to the watch list.
                if (success) // If adding was successful.
                {
                    _isInWatchList = true; // Set the flag to true.
                    MessageBox.Show("Film izleme listenize eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message.
                }
            }
            UpdateWatchListButton(); // Update the button's appearance.
        }

        private void btnClose_Click(object sender, EventArgs e) // This function runs when the 'Close' button is clicked.
        {
            this.Close(); // Close the movie detail form.
        }
    }
}