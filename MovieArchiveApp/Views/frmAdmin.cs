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
        private readonly IMovieService _movieService;
        private readonly IServiceProvider _serviceProvider; // Formları DI ile oluşturmak için

        // 2. Bağımlılık Enjeksiyonu için Constructor
        public frmAdmin(IMovieService movieService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _movieService = movieService;
            _serviceProvider = serviceProvider;

            // Form açıldığında filmleri yükle
            this.Load += (s, e) => LoadMovies();
        }

        // 3. Film listesini yükleme metodu
        private void LoadMovies()
        {
            try
            {
                // MovieService kullanarak tüm filmleri getir
                var movies = _movieService.GetAllMovies();

                // DataGridView'a bağla
                dgvMovies.DataSource = movies;

                // DÜZELTME: Admin panelinde ID sütununu görünür yap
                if (dgvMovies.Columns.Contains("Id"))
                {
                    dgvMovies.Columns["Id"].Visible = true; // Görünür yapıldı
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
            catch (Exception ex)
            {
                MessageBox.Show($"Filmler yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Yeni Film Ekleme İşlemi
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // DI container'dan frmMovieCrud'un ekleme modunu başlatan ctor'ını al
            var frmCrud = _serviceProvider.GetRequiredService<frmMovieCrud>();

            // Form başarılı bir şekilde kapanırsa (DialogResult.OK), listeyi yenile
            if (frmCrud.ShowDialog() == DialogResult.OK)
            {
                LoadMovies();
            }
        }

        // 5. Film Düzenleme İşlemi
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // DataGridView'de seçili bir film var mı kontrol et
            if (dgvMovies.SelectedRows.Count > 0)
            {
                // Seçili satırdan Movie nesnesini al
                var selectedMovie = dgvMovies.SelectedRows[0].DataBoundItem as Movie;

                if (selectedMovie != null)
                {
                    // Düzenleme modu için frmMovieCrud'un 3 parametreli constructor'ını kullan
                    var frmCrud = new frmMovieCrud(_movieService, selectedMovie);

                    if (frmCrud.ShowDialog() == DialogResult.OK)
                    {
                        LoadMovies();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz filmi listeden seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 6. Film Silme İşlemi
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count > 0)
            {
                var selectedMovie = dgvMovies.SelectedRows[0].DataBoundItem as Movie;

                if (selectedMovie != null)
                {
                    // Kullanıcıdan onay al
                    var dialogResult = MessageBox.Show(
                        $"{selectedMovie.Title} filmini silmek istediğinizden emin misiniz?",
                        "Silme Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            // Servis metodunu çağır
                            _movieService.DeleteMovie(selectedMovie.Id);
                            MessageBox.Show("Film başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMovies(); // Listeyi yenile
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Silme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz filmi listeden seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}