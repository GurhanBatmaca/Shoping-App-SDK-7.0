using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shopapp.data.Migrations
{
    /// <inheritdoc />
    public partial class InitinalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAproved = table.Column<bool>(type: "bit", nullable: false),
                    IsHome = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Kaşar", "kasar" },
                    { 2, "Eski Kaşar", "eski-kasar" },
                    { 3, "Yeni Kaşar", "yeni-kasar" },
                    { 4, "Süzme Bal", "suzme-bal" },
                    { 5, "Petek Bal", "petek-bal" },
                    { 6, "Kara Kovan Bal", "kara-kovan-bal" },
                    { 7, "Çiçek Bal", "cicek-bal" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "Description", "ImageUrl", "IsAproved", "IsHome", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7269), "Yeni Kaşar", "1.jpg", true, true, "Yeni Kaşar", 250.0, "yeni-kasar" },
                    { 2, new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7744), "Eski Kaşar", "2.jpg", true, true, "Eski Kaşar", 280.0, "eski-kasar" },
                    { 3, new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7752), "Kara Kovan Balı", "3.jpg", true, true, "Kara Kovan Balı", 280.0, "kara-kovan-bali" },
                    { 4, new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7754), "Petek Çiçek Balı", "4.jpg", true, true, "Petek Çiçek Balı", 280.0, "petek-cicek-bali" },
                    { 5, new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7756), "Süzme Çiçek Balı", "5.jpg", true, true, "Süzme Çiçek Balı", 280.0, "suzme-cicek-bali" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 3 },
                    { 4, 4 },
                    { 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
