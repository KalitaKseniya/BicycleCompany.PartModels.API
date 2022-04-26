using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BicycleCompany.PartModels.API.Migrations
{
    public partial class AddImageUrlToPartModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PartModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PartModels");
        }
    }
}
