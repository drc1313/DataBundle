using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class accountname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APIRequest_APIAccount_ApiHostName",
                table: "APIRequest");

            migrationBuilder.RenameColumn(
                name: "ApiHostName",
                table: "APIRequest",
                newName: "AccountName");

            migrationBuilder.RenameIndex(
                name: "IX_APIRequest_ApiHostName",
                table: "APIRequest",
                newName: "IX_APIRequest_AccountName");

            migrationBuilder.RenameColumn(
                name: "ApiHostName",
                table: "APIAccount",
                newName: "AccountName");

            migrationBuilder.AddForeignKey(
                name: "FK_APIRequest_APIAccount_AccountName",
                table: "APIRequest",
                column: "AccountName",
                principalTable: "APIAccount",
                principalColumn: "AccountName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APIRequest_APIAccount_AccountName",
                table: "APIRequest");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "APIRequest",
                newName: "ApiHostName");

            migrationBuilder.RenameIndex(
                name: "IX_APIRequest_AccountName",
                table: "APIRequest",
                newName: "IX_APIRequest_ApiHostName");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "APIAccount",
                newName: "ApiHostName");

            migrationBuilder.AddForeignKey(
                name: "FK_APIRequest_APIAccount_ApiHostName",
                table: "APIRequest",
                column: "ApiHostName",
                principalTable: "APIAccount",
                principalColumn: "ApiHostName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
