using Microsoft.EntityFrameworkCore.Migrations;

namespace Chinook.BusinessModel.Migrations
{
    public partial class _008RelationInvoiceInvoiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "UnitPrice",
                table: "InvoiceDetail",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "InvoiceDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_Invoice_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_Invoice_InvoiceId",
                table: "InvoiceDetail");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "InvoiceDetail");

            migrationBuilder.AlterColumn<long>(
                name: "UnitPrice",
                table: "InvoiceDetail",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
