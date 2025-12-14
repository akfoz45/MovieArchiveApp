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
        private readonly WatchListService _service = null!; // This service handles the Watch List.

        // 1. Parametresiz Constructor (Designer Hatası Almamak İçin)
        public frmWatchList() // Default constructor for the form.
        {
            InitializeComponent(); // Initialize the form components.
            this.Load += frmWatchList_Load; // Add Load event handler.
        }

        // 2. DI Constructor (Program.cs burayı kullanır)
        public frmWatchList(WatchListService service) : this() // Constructor with WatchList service.
        {
            _service = service; // Set the WatchList service.
        }

        private void frmWatchList_Load(object? sender, EventArgs e) // This runs when the Watch List form loads.
        {
            // Eğer _service null ise (tasarım modunda) işlem yapma
            if (_service != null) // Check if the service is ready.
            {
                ListeyiYukle(); // Load the movie list.
            }
        }

        private void ListeyiYukle() // This function loads the movies for the user.
        {
            // 1. ADIM: Kullanıcı Giriş Kontrolü
            int userId = SessionManager.CurrentUserId; // Get the ID of the logged-in user.

            // DEBUG: ID Kontrolü (Sorun çözülünce bu satırı silebilirsiniz)
            // MessageBox.Show($"Giriş yapan Kullanıcı ID: {userId}"); 

            if (userId == 0) // Check if the user ID is valid.
            {
                MessageBox.Show("Hata: Kullanıcı girişi algılanamadı (ID=0). Lütfen çıkış yapıp tekrar giriş yapınız."); // Show an error message.
                return; // Stop the function.
            }

            // 2. ADIM: Veritabanından Çekme
            List<Movie> myMovies = _service.GetUserWatchlist(userId); // Get the user's watch list movies.

            // DEBUG: Veri Sayısı Kontrolü (Sorun çözülünce bu satırı silebilirsiniz)
            if (myMovies.Count == 0) // Check if there are any movies.
            {
                MessageBox.Show("Sorgu çalıştı ancak veritabanından hiç film gelmedi. Kayıt yapılmamış olabilir."); // Show message if the list is empty.
            }

            // 3. ADIM: Ekrana Basma
            dgvList.DataSource = null; // Clear the old data.
            dgvList.DataSource = myMovies; // Show the new movie list in the table.

            // Sadece veri varsa kolon gizleme yap (Hata almamak için)
            if (myMovies.Count > 0) // Check if there are movies to display.
            {
                if (dgvList.Columns["Ratings"] != null) dgvList.Columns["Ratings"].Visible = false; // Hide the Ratings column.
                if (dgvList.Columns["PosterPath"] != null) dgvList.Columns["PosterPath"].Visible = false; // Hide the Poster Path column.
                if (dgvList.Columns["Description"] != null) dgvList.Columns["Description"].Visible = false; // Hide the Description column.
                // İlişkisel alanları da gizleyelim
                if (dgvList.Columns["WatchLists"] != null) dgvList.Columns["WatchLists"].Visible = false; // Hide the Watch Lists column.
            }
        }

        private void btnRemove_Click(object sender, EventArgs e) // This function runs when the 'Remove' button is clicked.
        {
            if (dgvList.CurrentRow == null) // Check if a movie is selected in the list.
            {
                MessageBox.Show("Lütfen silinecek filmi seçin."); // Show a warning if no movie is selected.
                return; // Stop the function.
            }

            // Hücre değerini güvenli şekilde al
            if (dgvList.CurrentRow.Cells["Id"].Value == null) return; // Stop if the selected movie ID is null.

            int movieId = Convert.ToInt32(dgvList.CurrentRow.Cells["Id"].Value); // Get the selected movie ID.
            int userId = SessionManager.CurrentUserId; // Get the current user ID.

            var cevap = MessageBox.Show("Bu filmi listeden kaldırmak istiyor musunuz?", // Ask for confirmation.
                                        "Onay", // Confirmation title.
                                        MessageBoxButtons.YesNo, // Show Yes and No buttons.
                                        MessageBoxIcon.Question); // Show a question icon.

            if (cevap == DialogResult.Yes) // If the user says Yes.
            {
                _service.RemoveFromWatchlist(userId, movieId); // Call the service to remove the movie.
                MessageBox.Show("Film listeden kaldırıldı."); // Show success message.
                ListeyiYukle(); // Refresh the movie list on the screen.
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e) // This function runs when a cell content is clicked.
        {

        } // This function is empty now.
    }
}