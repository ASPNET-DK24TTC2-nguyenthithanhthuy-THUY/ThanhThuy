using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoeStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "CreatedAt", "Description", "DiscountPercent", "IsActive", "IsBestSeller", "IsFeatured", "MainImageUrl", "Name", "Price", "ProductCode", "SalePrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giày thể thao Nike Air Max 270 với đệm khí tối đa mang lại sự thoải mái vượt trội cho mọi hoạt động", 10m, true, true, true, "https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/awjogtdnqxniqqk0wpgf/air-max-270-shoes-2V5C4p.png", "Nike Air Max 270", 3500000m, "NK-AM270-001", 3150000m, null },
                    { 2, 2, 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giày chạy bộ Adidas Ultraboost 22 với công nghệ Boost độc quyền mang đến năng lượng vô tận", 15m, true, true, true, "https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/fbaf991a78bc4896a3e9ad7800abcec6_9366/Ultraboost_22_Shoes_Black_GZ0127_01_standard.jpg", "Adidas Ultraboost 22", 4200000m, "AD-UB22-001", 3570000m, null },
                    { 3, 3, 1, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giày sneaker Puma RS-X phong cách retro hiện đại, thiết kế độc đáo và nổi bật", null, true, false, true, "https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa,w_600,h_600/global/380462/01/sv01/fnd/PNA/fmt/png/RS-X-Reinvention-Sneakers", "Puma RS-X", 2800000m, "PM-RSX-001", null, null },
                    { 4, 4, 1, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giày Converse Chuck Taylor All Star kinh điển, phong cách bất biến theo năm tháng", null, true, true, false, "https://www.converse.com/dw/image/v2/BCZC_PRD/on/demandware.static/-/Sites-cnv-master-catalog/default/dw0d236c0e/images/a_107/M9160_A_107X1.jpg", "Converse Chuck Taylor All Star", 1500000m, "CV-CT-001", null, null },
                    { 5, 5, 1, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giày Vans Old Skool với thiết kế sọc đặc trưng, phong cách skate đường phố", null, true, true, false, "https://images.vans.com/is/image/Vans/VN000D3HY28-HERO", "Vans Old Skool", 1800000m, "VN-OS-001", null, null },
                    { 6, 1, 1, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nike Air Force 1 - Biểu tượng sneaker huyền thoại với thiết kế cổ điển", 20m, true, true, true, "https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/b7d9211c-26e7-431a-ac24-b0540fb3c00f/air-force-1-07-shoes-WrLlWX.png", "Nike Air Force 1", 2900000m, "NK-AF1-001", 2320000m, null },
                    { 7, 2, 1, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adidas Stan Smith - Giày tennis cổ điển với phong cách tối giản thanh lịch", null, true, false, true, "https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/3f9882d9b4e744daa0daae7c0115d3f1_9366/Stan_Smith_Shoes_White_FX5500_01_standard.jpg", "Adidas Stan Smith", 2500000m, "AD-SS-001", null, null },
                    { 8, 3, 1, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Puma Suede Classic - Giày da lộn cao cấp với thiết kế vintage đầy phong cách", 5m, true, true, false, "https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa,w_600,h_600/global/374915/01/sv01/fnd/PNA/fmt/png/Suede-Classic-XXI-Sneakers", "Puma Suede Classic", 2200000m, "PM-SC-001", 2090000m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 133, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 133, DateTimeKind.Local).AddTicks(3846));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 133, DateTimeKind.Local).AddTicks(3852));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 133, DateTimeKind.Local).AddTicks(3853));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "BrandId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 133, DateTimeKind.Local).AddTicks(3855));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 130, DateTimeKind.Local).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 132, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 132, DateTimeKind.Local).AddTicks(3059));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 132, DateTimeKind.Local).AddTicks(3061));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 11, 49, 47, 132, DateTimeKind.Local).AddTicks(3063));
        }
    }
}
