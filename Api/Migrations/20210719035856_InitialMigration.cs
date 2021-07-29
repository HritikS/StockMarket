using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Brief = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Brief = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockExchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Turnover = table.Column<int>(nullable: false),
                    CEO = table.Column<string>(nullable: true),
                    BOD = table.Column<string>(nullable: true),
                    SectorId = table.Column<int>(nullable: false),
                    Brief = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStockExchanges",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    StockExchangeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStockExchanges", x => new { x.CompanyId, x.StockExchangeId });
                    table.ForeignKey(
                        name: "FK_CompanyStockExchanges_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyStockExchanges_StockExchanges_StockExchangeId",
                        column: x => x.StockExchangeId,
                        principalTable: "StockExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IPODetails",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    StockExchangeId = table.Column<int>(nullable: false),
                    PPS = table.Column<int>(nullable: false),
                    TNOS = table.Column<int>(nullable: false),
                    OpenDateTime = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPODetails", x => new { x.CompanyId, x.StockExchangeId });
                    table.ForeignKey(
                        name: "FK_IPODetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IPODetails_StockExchanges_StockExchangeId",
                        column: x => x.StockExchangeId,
                        principalTable: "StockExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPrices",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false),
                    StockExchangeId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    CurrentPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPrices", x => new { x.CompanyId, x.StockExchangeId, x.DateTime });
                    table.ForeignKey(
                        name: "FK_StockPrices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPrices_StockExchanges_StockExchangeId",
                        column: x => x.StockExchangeId,
                        principalTable: "StockExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Brief", "Name" },
                values: new object[,]
                {
                    { 1, "1", "Sector1" },
                    { 2, "2", "Sector2" },
                    { 3, "3", "Sector3" }
                });

            migrationBuilder.InsertData(
                table: "StockExchanges",
                columns: new[] { "Id", "Address", "Brief", "Name", "Remarks" },
                values: new object[,]
                {
                    { 1, "A1", "1", "SE1", "R1" },
                    { 2, "A2", "2", "SE2", "R2" },
                    { 3, "A3", "3", "SE3", "R3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Confirmed", "Email", "MobileNumber", "Password", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, true, "user1@gmail.com", "1111111111", "user1", true, "user1" },
                    { 2, true, "user2@gmail.com", "2222222222", "user2", false, "user2" },
                    { 3, true, "user3@gmail.com", "3333333333", "user3", false, "user3" },
                    { 4, false, "user4@gmail.com", "4444444444", "user4", false, "user4" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BOD", "Brief", "CEO", "Name", "SectorId", "Turnover" },
                values: new object[] { 1, "BOD1", "1", "CEO1", "Company1", 1, 1 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BOD", "Brief", "CEO", "Name", "SectorId", "Turnover" },
                values: new object[] { 2, "BOD2", "2", "CEO2", "Company2", 2, 2 });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BOD", "Brief", "CEO", "Name", "SectorId", "Turnover" },
                values: new object[] { 3, "BOD3", "3", "CEO3", "Company3", 3, 3 });

            migrationBuilder.InsertData(
                table: "CompanyStockExchanges",
                columns: new[] { "CompanyId", "StockExchangeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "IPODetails",
                columns: new[] { "CompanyId", "StockExchangeId", "OpenDateTime", "PPS", "Remarks", "TNOS" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 7, 19, 9, 28, 55, 995, DateTimeKind.Local).AddTicks(8636), 1, "R1", 1 },
                    { 2, 2, new DateTime(2021, 7, 19, 9, 28, 55, 997, DateTimeKind.Local).AddTicks(2123), 2, "R2", 2 },
                    { 3, 3, new DateTime(2021, 7, 19, 9, 28, 55, 997, DateTimeKind.Local).AddTicks(2253), 3, "R3", 3 }
                });

            migrationBuilder.InsertData(
                table: "StockPrices",
                columns: new[] { "CompanyId", "StockExchangeId", "DateTime", "CurrentPrice" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 7, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2021, 7, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, new DateTime(2021, 7, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStockExchanges_StockExchangeId",
                table: "CompanyStockExchanges",
                column: "StockExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_IPODetails_StockExchangeId",
                table: "IPODetails",
                column: "StockExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_StockExchangeId",
                table: "StockPrices",
                column: "StockExchangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyStockExchanges");

            migrationBuilder.DropTable(
                name: "IPODetails");

            migrationBuilder.DropTable(
                name: "StockPrices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "StockExchanges");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
