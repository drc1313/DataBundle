using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBundle.DAL.Migrations
{
    public partial class Bundle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIBundle",
                columns: table => new
                {
                    BundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BundleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BundleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIBundle", x => x.BundleId);
                });

            migrationBuilder.CreateTable(
                name: "APIBundleRequests",
                columns: table => new
                {
                    BundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestPriority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIBundleRequests", x => new { x.BundleId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_APIBundleRequests_APIBundle_BundleId",
                        column: x => x.BundleId,
                        principalTable: "APIBundle",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APIBundleRequests_APIRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "APIRequest",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APIBundleRequests_RequestId",
                table: "APIBundleRequests",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIBundleRequests");

            migrationBuilder.DropTable(
                name: "APIBundle");
        }
    }
}
