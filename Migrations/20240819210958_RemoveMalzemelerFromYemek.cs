using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekTarifiApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMalzemelerFromYemek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Malzemeler_Yemekler_YemekId",
                table: "Malzemeler");

            migrationBuilder.DropIndex(
                name: "IX_Malzemeler_YemekId",
                table: "Malzemeler");

            migrationBuilder.DropColumn(
                name: "YemekId",
                table: "Malzemeler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YemekId",
                table: "Malzemeler",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Malzemeler_YemekId",
                table: "Malzemeler",
                column: "YemekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Malzemeler_Yemekler_YemekId",
                table: "Malzemeler",
                column: "YemekId",
                principalTable: "Yemekler",
                principalColumn: "Id");
        }
    }
}
