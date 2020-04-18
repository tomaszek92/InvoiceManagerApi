using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceManagerApi.Migrations
{
    public partial class RemoveInvoiceRowUnitPriceGrossColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPriceGross",
                table: "InvoiceRows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPriceGross",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
