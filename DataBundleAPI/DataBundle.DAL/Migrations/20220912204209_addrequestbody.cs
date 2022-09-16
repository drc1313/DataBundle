using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class addrequestbody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                table: "APIRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestBody",
                table: "APIRequest");
        }
    }
}
