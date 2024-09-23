using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekTarifiApp.Migrations
{
    /// <inheritdoc />
    public partial class YapilisEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "YemekMalzemeler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Yapilis",
                table: "Yemekler",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "YemekMalzemeler");

            migrationBuilder.DropColumn(
                name: "Yapilis",
                table: "Yemekler");
        }
    }
}
