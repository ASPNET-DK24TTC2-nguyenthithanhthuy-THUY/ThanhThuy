using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Orders",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 475, DateTimeKind.Local).AddTicks(2317));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 475, DateTimeKind.Local).AddTicks(4002));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 475, DateTimeKind.Local).AddTicks(4007));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 475, DateTimeKind.Local).AddTicks(4009));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 475, DateTimeKind.Local).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 472, DateTimeKind.Local).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 474, DateTimeKind.Local).AddTicks(3729));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 474, DateTimeKind.Local).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 474, DateTimeKind.Local).AddTicks(3749));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 12, 11, 59, 474, DateTimeKind.Local).AddTicks(3750));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Orders",
                newName: "Note");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 450, DateTimeKind.Local).AddTicks(5948));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 450, DateTimeKind.Local).AddTicks(7549));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 450, DateTimeKind.Local).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 450, DateTimeKind.Local).AddTicks(7557));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 450, DateTimeKind.Local).AddTicks(7558));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 447, DateTimeKind.Local).AddTicks(9244));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 449, DateTimeKind.Local).AddTicks(7368));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 449, DateTimeKind.Local).AddTicks(7386));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 449, DateTimeKind.Local).AddTicks(7388));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 57, 25, 449, DateTimeKind.Local).AddTicks(7390));
        }
    }
}
