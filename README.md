# 🎬 Movie Archive App (WinForms – .NET 8)
Film ve dizi arşivleme, izleme listesi yönetimi, kullanıcı giriş sistemi ve admin paneli içeren masaüstü uygulaması.  
Windows Forms + Entity Framework Core ile geliştirilmiştir.

---

## 🚀 Özellikler

### 👤 Kullanıcı Yönetimi
- Kayıt olma
- Giriş yapma
- BCrypt ile şifre hashleme
- Oturum yönetimi (SessionManager)

### 🎞️ Film / Dizi Yönetimi
- Film listeleme
- Detay sayfası
- Arama ve filtreleme (tür, yıl, isim)
- Poster gösterimi

### ⭐ Kullanıcı Etkileşimleri
- İzleme listesine ekleme / kaldırma
- Film puanlama
- Ortalama puana göre sıralama

### 🛠️ Admin Modülü
- Film ekleme / düzenleme / silme
- Yetki kontrolü (Admin)
- Top 10 film listesi

### 📊 Raporlama
- LiveCharts2 ile grafikler
- En yüksek puanlı filmler
- En çok eklenen filmler

---

## 🧱 Mimari (Layered Architecture)

MovieApp/
│
├── Data/
│ ├── MovieDbContext.cs
│ ├── Entities/
│ │ ├── Movie.cs
│ │ ├── User.cs
│ │ ├── Rating.cs
│ │ ├── WatchListItem.cs
│ │ └── Category.cs
│
├── Services/
│ ├── Interfaces/
│ │ ├── IAuthService.cs
│ │ ├── IMovieService.cs
│ │ ├── IWatchlistService.cs
│ │ └── IRatingService.cs
│ ├── AuthService.cs
│ ├── MovieService.cs
│ ├── WatchlistService.cs
│ ├── RatingService.cs
│ └── Helpers/
│ └── SessionManager.cs
│
├── Views/
│ ├── frmLogin.cs
│ ├── frmSignUp.cs
│ ├── frmMain.cs
│ ├── frmHome.cs
│ ├── frmMovieDetail.cs
│ ├── frmWatchlist.cs
│ ├── frmAdmin.cs
│ ├── frmTopList.cs
│ └── UserControls/
│ └── ucInteraction.cs
│
└── Program.cs


---

## 📚 Kullanılan Teknolojiler

| Amaç | Teknoloji |
|------|-----------|
| UI | Windows Forms (.NET 8) |
| ORM | Entity Framework Core |
| Veritabanı | SQLite |
| Login / Hash | BCrypt.Net-Next |
| Grafik / Charts | LiveCharts2 |
| API (Opsiyonel) | TMDb API |
| Mimari | Katmanlı Mimari (Service Layer) |

---

## 👥 Ekip & Görev Dağılımı

### 1. Proje Altyapısı / Backend
- Proje oluşturma
- DbContext ve entity modelleri
- Servis arayüzleri

### 2. Kullanıcı İşlemleri (Auth)
- Login / Register
- BCrypt ile şifreleme
- Session yönetimi
- Login & Signup ekranları

### 3. Ana Sayfa & Detay
- Film listeleme
- Arama & filtreleme
- Film detay ekranı

### 4. Watchlist & Rating
- İzleme listesi ekleme/kaldırma
- Film puanlama sistemi
- Watchlist ekranı

### 5. Admin & Raporlama
- Film CRUD işlemleri
- Top 10 ekranı
- Grafikler

---

## 🔧 Kurulum

### 1) NuGet Paketleri
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Design
- BCrypt.Net-Next
- LiveChartsCore.SkiaSharpView.WinForms

### 2) EF Core Migration

Add-Migration InitialCreate
Update-Database


### 3) Uygulamayı Başlatma
`Program.cs` içinde:

```csharp
Application.Run(new frmLogin());
