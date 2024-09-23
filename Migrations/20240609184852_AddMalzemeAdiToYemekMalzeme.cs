using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekTarifiApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMalzemeAdiToYemekMalzeme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MalzemeAdi",
                table: "YemekMalzemeler",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Miktar",
                table: "YemekMalzemeler",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MalzemeAdi",
                table: "YemekMalzemeler");

            migrationBuilder.DropColumn(
                name: "Miktar",
                table: "YemekMalzemeler");
        }
    }
}
