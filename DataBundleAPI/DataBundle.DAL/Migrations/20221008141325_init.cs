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
                    AccountName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumenationLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delimiter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIAccount", x => x.AccountName);
                });

            migrationBuilder.CreateTable(
                name: "APIRequest",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIRequest", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_APIRequest_APIAccount_AccountName",
                        column: x => x.AccountName,
                        principalTable: "APIAccount",
                        principalColumn: "AccountName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APIUsage",
                columns: table => new
                {
                    UsageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentUsage = table.Column<int>(type: "int", nullable: false),
                    MaxUsage = table.Column<int>(type: "int", nullable: false),
                    UsageDuration = table.Column<int>(type: "int", nullable: false),
                    LastCallDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIUsage", x => x.UsageID);
                    table.ForeignKey(
                        name: "FK_APIUsage_APIAccount_AccountName",
                        column: x => x.AccountName,
                        principalTable: "APIAccount",
                        principalColumn: "AccountName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APIRequestMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIRequestMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APIRequestMetadata_APIRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "APIRequest",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APIRequest_AccountName",
                table: "APIRequest",
                column: "AccountName");

            migrationBuilder.CreateIndex(
                name: "IX_APIRequestMetadata_RequestId",
                table: "APIRequestMetadata",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_APIUsage_AccountName",
                table: "APIUsage",
                column: "AccountName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIRequestMetadata");

            migrationBuilder.DropTable(
                name: "APIUsage");

            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIAccount");
        }
    }
}
