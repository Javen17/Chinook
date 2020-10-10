using Microsoft.EntityFrameworkCore.Migrations;

namespace Chinook.BusinessModel.Migrations
{
    public partial class _007RelationEmployeeClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SupportEmployeeId",
                table: "Client",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_SupportEmployeeId",
                table: "Client",
                column: "SupportEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Employee_SupportEmployeeId",
                table: "Client",
                column: "SupportEmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Employee_SupportEmployeeId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_SupportEmployeeId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "SupportEmployeeId",
                table: "Client");
        }
    }
}
