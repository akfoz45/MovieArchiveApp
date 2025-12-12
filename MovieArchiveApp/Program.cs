using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieArchiveApp.Data;
using MovieArchiveApp.Data.Entities;
using MovieArchiveApp.Services;
using MovieArchiveApp.Services.Interfaces;
using MovieArchiveApp.Views;
using System;
using System.Linq;
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
                        options.UseSqlite("Data Source=MovieArchiveApp.db"), ServiceLifetime.Transient);

                    services.AddTransient<IMovieService, MovieService>();
                    services.AddTransient<IAuthService, AuthService>();
                    services.AddTransient<WatchListService>();
                    services.AddTransient<RatingService>();

                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmHome>();
                    services.AddTransient<frmMain>();
                    services.AddTransient<frmSignUp>();
                    services.AddTransient<frmMovieCrud>();
                    services.AddTransient<frmAdmin>();
                    services.AddTransient<frmWatchList>();
                    services.AddTransient<frmMovieDetail>();
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<MovieDbContext>();
                    dbContext.Database.EnsureCreated();

                    // Otomatik Admin Oluþturma
                    if (!dbContext.Users.Any(u => u.Username == "admin"))
                    {
                        var adminUser = new User
                        {
                            Username = "admin",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"),
                            IsAdmin = true
                        };
                        dbContext.Users.Add(adminUser);
                        dbContext.SaveChanges();
                    }

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