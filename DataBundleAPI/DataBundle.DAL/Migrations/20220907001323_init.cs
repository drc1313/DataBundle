using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIAccount",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiHostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumenationLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delimiter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIAccount", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "APIRequest",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIRequest", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_APIRequest_APIAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "APIAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APIRequest_AccountId",
                table: "APIRequest",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIAccount");
        }
    }
}
