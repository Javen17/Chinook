using Microsoft.EntityFrameworkCore.Migrations;

namespace Chinook.BusinessModel.Migrations
{
    public partial class _005recursiverelationshipfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Album_AlbumId1",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Album_AlbumId1",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "AlbumId1",
                table: "Album");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlbumId1",
                table: "Album",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Album_AlbumId1",
                table: "Album",
                column: "AlbumId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Album_AlbumId1",
                table: "Album",
                column: "AlbumId1",
                principalTable: "Album",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
