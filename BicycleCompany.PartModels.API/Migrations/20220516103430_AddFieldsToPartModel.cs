using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BicycleCompany.PartModels.API.Migrations
{
    public partial class AddFieldsToPartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "PartModels",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "WeightInKg",
                table: "PartModels",
                type: "float(15)",
                precision: 15,
                scale: 3,
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "PartModels");

            migrationBuilder.DropColumn(
                name: "WeightInKg",
                table: "PartModels");
        }
    }
}
