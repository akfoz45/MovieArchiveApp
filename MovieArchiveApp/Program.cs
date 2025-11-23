// Program.cs
using Microsoft.EntityFrameworkCore;// IHostBuilder ve Host yapýsý için GEREKLÝ
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; // Formlar için
using MovieArchiveApp.Data;             // DbContext için
using MovieArchiveApp.Services;         // AuthService için
using MovieArchiveApp.Services.Interfaces; // IAuthService için
using MovieArchiveApp.Views;
using System.Linq; // Genellikle DbContext metodlarý için GEREKLÝ (First, Any vb.)// Migrate için
using System.Threading;
using System.Windows.Forms;
// ... diðer using'leriniz ...

namespace MovieArchiveApp
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(localAppData, "MovieArchiveApp");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            string dbPath = Path.Combine(appFolder, "MovieArchiveApp.db");

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MovieDbContext>();
                    var authService = services.GetRequiredService<IAuthService>();

                    context.Database.Migrate();
                    SeedAdminUser(authService);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kritik Baþlangýç Hatasý: {ex.Message}");
                }
            }

            // *** BURASI YOKTU — FORM HÝÇ AÇILMIYORDU ***
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var frmLogin = services.GetRequiredService<frmLogin>();
                    Application.Run(frmLogin);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception)?.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Uygulama Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Program.cs içinde, sýnýfýn sonunda veya baþka bir yerde tanýmlanacak
        // Program.cs içindeki CreateHostBuilder metodu

        // Program.cs içinde, static IHostBuilder CreateHostBuilder() metodu

        static IHostBuilder CreateHostBuilder()
        {
            // 1. Veritabaný dosyasýnýn (MovieArchiveApp.db) güvenli ve sabit yolunu oluþtur.
            // Bu, verilerin her zaman ayný yerde saklanmasýný garanti eder.
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolderName = "MovieArchiveApp";

            // Klasör yolunu oluþtur: C:\Users\<User>\AppData\Local\MovieArchiveApp
            string appFolderPath = Path.Combine(localAppData, appFolderName);

            // Klasör yoksa oluþtur.
            if (!Directory.Exists(appFolderPath))
            {
                Directory.CreateDirectory(appFolderPath);
            }

            // Tam DB dosya yolu: C:\Users\<User>\AppData\Local\MovieArchiveApp\MovieArchiveApp.db
            string dbPath = Path.Combine(appFolderPath, "MovieArchiveApp.db");

            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // 2. EF Core DbContext Kaydý
                    // Baðlantý dizesi olarak sabitlenmiþ (fixed) yolu kullan.
                    services.AddDbContext<MovieDbContext>(options =>
                        options.UseSqlite("Data Source=MovieArchiveApp.db"),
                        // DbContext'i Scope'ta tutmak, AuthService'in her çaðrýsýnda ayný DbContext'i görmesini saðlar.
                        ServiceLifetime.Scoped);

                    // 3. Servis Katmaný Kayýtlarý
                    // AuthService'i Transient yapmak, ObjectDisposedException hatasýný önler.
                    services.AddTransient<IAuthService, AuthService>();

                    // 4. Form Kayýtlarý
                    // Formlarý Transient yapmak, DI tarafýndan yeni örnekler oluþturulmasýný saðlar.
                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmMain>();
                    services.AddTransient<frmSignUp>();
                });
        }
        // Program.cs içinde, sýnýfýn bir parçasý olarak

        private static void SeedAdminUser(IAuthService authService)
        {
            // Eðer 'admin' kullanýcýsý henüz yoksa, kaydolmayý denerken null dönecektir.
            if (authService.Login("admin", "123456") == null)
            {
                // Yeni Admin kullanýcýsýný oluþtur (isAdmin: true)
                authService.SignUp("admin", "123456", isAdmin: true);
                MessageBox.Show("Ýlk admin kullanýcýsý (admin/123456) otomatik oluþturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}