using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class header : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Headers",
                table: "APIAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headers",
                table: "APIAccount");
        }
    }
}
