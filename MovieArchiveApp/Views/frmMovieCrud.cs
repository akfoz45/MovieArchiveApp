using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services.Interfaces;
using System;
using System.Windows.Forms;

namespace MovieArchiveApp.Views
{
    public partial class frmMovieCrud : Form
    {
        // IMovieService bağımlılığını saklamak için alan.
        private readonly IMovieService _movieService;

        // Düzenlenecek film. Yeni film ekleniyorsa bu null olacaktır.
        private Movie _movieToEdit;

        // YENİ EKLENEN: Tasarımcının (Designer) Formu Başlatması İçin Parametresiz Constructor
        // Bu, Visual Studio Tasarımcısı'nın formu yüklemesini sağlar.
        public frmMovieCrud()
        {
            InitializeComponent();
        }

        // Ekleme modu için constructor
        public frmMovieCrud(IMovieService movieService) : this() // Parametresiz constructor'ı çağır
        {
            _movieService = movieService;
            // Ekleme modunda film başlığını ayarla.
            this.Text = "Yeni Film Ekle";
            // Yeni bir film nesnesi oluştur.
            // Movie nesnesinin tüm alanlarını bilmiyorum, varsayılan olarak ID=0'ı kullanıyorum.
            _movieToEdit = new Movie();
        }

        // Güncelleme işlemi için kullanılan Constructor (Movie nesnesi alır)
        public frmMovieCrud(IMovieService movieService, Movie movie) : this(movieService)
        {
            // Bu, önce 1 parametreli ctor'ı (ekleme modu) çağırır, sonra aşağıdaki kodu çalıştırır.
            _movieToEdit = movie;
            // Güncelleme modunda başlığı ve alanları doldur.
            this.Text = "Film Düzenle: " + movie.Title;
            LoadMovieData(movie);
        }

        // Film verilerini form bileşenlerine yükler
        private void LoadMovieData(Movie movie)
        {
            txtTitle.Text = movie.Title;
            txtDescription.Text = movie.Description;
            txtGenre.Text = movie.Genre;
            txtYear.Text = movie.Year.ToString();
            txtDirector.Text = movie.Director;
            txtPosterUrl.Text = movie.PosterUrl;
        }

        // Kaydet butonu click olayı (ADD veya UPDATE işlemini gerçekleştirir)
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Eğer _movieService null ise (tasarım anında) işlemi durdur.
            if (_movieService == null) return;

            // Alan doğrulama (Basit kontrol: Film Adı ve Yayın Yılı zorunlu)
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || !int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Lütfen Film Adı ve Yayın Yılı alanlarını doğru girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Filmin ID'si 0'dan büyükse GÜNCELLEME (Update), 0 ise EKLEME (Add)
            bool isUpdate = _movieToEdit != null && _movieToEdit.Id > 0;

            try
            {
                // Formdaki verileri _movieToEdit nesnesine aktar.
                if (_movieToEdit == null) _movieToEdit = new Movie();

                _movieToEdit.Title = txtTitle.Text;
                _movieToEdit.Description = txtDescription.Text;
                _movieToEdit.Genre = txtGenre.Text;
                _movieToEdit.Year = year;
                _movieToEdit.Director = txtDirector.Text;
                _movieToEdit.PosterUrl = txtPosterUrl.Text;

                // Servis katmanını çağır
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

                // Kayıt başarılı olduğunda formu kapat ve DialogResult'u OK yap.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // İptal butonu click olayı
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}