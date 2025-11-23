// MovieArchiveApp.Migrations/AddIsAdminToUser.cs

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieArchiveApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAdminToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ YENİ KOLONU EKLEME KOMUTU
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",                          // Kolon Adı
                table: "Users",                           // Hangi Tabloya Eklenecek
                type: "INTEGER",                          // SQLite'ta bool, INTEGER olarak saklanır
                nullable: false,                          // Bu kolon boş (null) bırakılamaz
                defaultValue: false);                     // Varsayılan değeri (false) olsun
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ❌ GERİ ALMA KOMUTU (Bu migration geri alınırsa yapılacak iş)
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");
        }
    }
}