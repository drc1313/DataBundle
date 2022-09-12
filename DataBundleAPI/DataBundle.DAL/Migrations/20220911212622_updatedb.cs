using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_APIUsage_AccountName",
                table: "APIUsage",
                column: "AccountName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIUsage");
        }
    }
}
