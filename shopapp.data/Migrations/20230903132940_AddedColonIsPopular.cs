using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopapp.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColonIsPopular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPopular",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "IsPopular" },
                values: new object[] { new DateTime(2023, 9, 3, 16, 29, 39, 997, DateTimeKind.Local).AddTicks(1818), true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "IsPopular" },
                values: new object[] { new DateTime(2023, 9, 3, 16, 29, 39, 997, DateTimeKind.Local).AddTicks(1833), false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "IsPopular" },
                values: new object[] { new DateTime(2023, 9, 3, 16, 29, 39, 997, DateTimeKind.Local).AddTicks(1835), true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAdded", "IsPopular" },
                values: new object[] { new DateTime(2023, 9, 3, 16, 29, 39, 997, DateTimeKind.Local).AddTicks(1837), false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAdded", "IsPopular" },
                values: new object[] { new DateTime(2023, 9, 3, 16, 29, 39, 997, DateTimeKind.Local).AddTicks(1838), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPopular",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7752));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7754));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 8, 29, 21, 5, 45, 654, DateTimeKind.Local).AddTicks(7756));
        }
    }
}
