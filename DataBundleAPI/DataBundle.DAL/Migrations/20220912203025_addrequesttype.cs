using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class addrequesttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "APIRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "APIRequest");
        }
    }
}
