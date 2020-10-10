using Microsoft.EntityFrameworkCore.Migrations;

namespace Chinook.BusinessModel.Migrations
{
    public partial class _009ChangeInvoiceTotalToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Total",
                table: "Invoice",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
