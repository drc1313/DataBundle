using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIAccounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiHostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumenationLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "APIRequest",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIRequest", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_APIRequest_APIAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "APIAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<int>(type: "int", nullable: false),
                    ConfigData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Tokens_APIRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "APIRequest",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APIRequest_AccountId",
                table: "APIRequest",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_RequestId",
                table: "Tokens",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIAccounts");
        }
    }
}
