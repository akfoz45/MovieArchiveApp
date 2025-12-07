using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieArchiveApp.Data;
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Views;
using System;
using System.Windows.Forms;

namespace MovieArchiveApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Host oluþturma (Dependency Injection için)
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // 1. Veritabaný Baðlantýsý (SQLite)
                    services.AddDbContext<MovieDbContext>(options =>
                        options.UseSqlite("Data Source=MovieArchiveApp.db")); // Kendi baðlantý cümlenizi buraya yazýn

                    // 2. Servis Kayýtlarý (Singleton/Scoped/Transient)
                    // Tüm kullanýcýlarýn CRUD yapabilmesi için MovieService'i kaydettik.
                    services.AddScoped<IMovieService, MovieService>(); // Yeni eklenen servis
                    services.AddScoped<IAuthService, AuthService>();
                    // Not: Diðer servisler (RatingService, WatchlistService vb.) de projenin tamamýnda bu þekilde kaydedilmelidir.

                    // 3. Form Kayýtlarý (Tüm formlar geçici (Transient) olarak kaydedilmeli)
                    // Formlarýn constructor'ýnda servisleri alabilmesi için kayýt gereklidir.
                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmHome>();
                    services.AddTransient<frmMain>();
                    services.AddTransient<frmSignUp>();
                    services.AddTransient<frmMovieCrud>(); // Yeni eklenen CRUD formu
                    services.AddTransient<frmAdmin>(); // Mevcut admin formu da DI'a eklendi
                    services.AddTransient<frmWatchList>(); // Mevcut izleme listesi formu da DI'a eklendi


                })
                .Build();

            // Formlar artýk DI container'dan alýnarak çalýþtýrýlýr.
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    // Uygulama baþlangýcýný frmLogin ile baþlat.
                    var initialForm = services.GetRequiredService<frmLogin>();
                    Application.Run(initialForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Uygulama baþlatýlýrken kritik bir hata oluþtu. Lütfen NuGet paketlerinin (Microsoft.Extensions.Hosting, Microsoft.EntityFrameworkCore.Sqlite vb.) kurulu olduðundan emin olun. Hata: {ex.Message}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}