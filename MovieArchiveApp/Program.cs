using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities; // User entity için gerekli
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Views;
using System;
using System.Linq; // Any() metodu için gerekli
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

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<MovieDbContext>(options =>
                        options.UseSqlite("Data Source=MovieArchiveApp.db"));

                    services.AddScoped<IMovieService, MovieService>();
                    services.AddScoped<IAuthService, AuthService>();

                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmHome>();
                    services.AddTransient<frmMain>();
                    services.AddTransient<frmSignUp>();
                    services.AddTransient<frmMovieCrud>();
                    services.AddTransient<frmAdmin>();
                    services.AddTransient<frmWatchList>();
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<MovieDbContext>();
                    dbContext.Database.EnsureCreated();

                    // --- EKLENEN KISIM: OTOMATÝK ADMIN OLUÞTURMA ---
                    // Eðer 'admin' adýnda bir kullanýcý yoksa oluþtur.
                    if (!dbContext.Users.Any(u => u.Username == "admin"))
                    {
                        var adminUser = new User
                        {
                            Username = "admin",
                            // Þifreyi '123' olarak hashleyip kaydediyoruz.
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"),
                            IsAdmin = true // Admin yetkisi veriyoruz
                        };
                        dbContext.Users.Add(adminUser);
                        dbContext.SaveChanges();
                    }
                    // -----------------------------------------------

                    var initialForm = services.GetRequiredService<frmLogin>();
                    Application.Run(initialForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Baþlangýç hatasý: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}