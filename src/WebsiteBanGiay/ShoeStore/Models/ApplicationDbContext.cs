using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoeStore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Coupon)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CouponId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Giày Sneaker", Description = "Giày thể thao sneaker thời trang", IsActive = true },
                new Category { CategoryId = 2, Name = "Giày Thể Thao", Description = "Giày thể thao chuyên dụng", IsActive = true },
                new Category { CategoryId = 3, Name = "Giày Da", Description = "Giày da cao cấp", IsActive = true },
                new Category { CategoryId = 4, Name = "Giày Lười", Description = "Giày lười tiện lợi", IsActive = true },
                new Category { CategoryId = 5, Name = "Dép & Sandal", Description = "Dép và sandal thoải mái", IsActive = true }
            );

            // Seed Brands
            modelBuilder.Entity<Brand>().HasData(
                new Brand { BrandId = 1, Name = "Nike", Description = "Thương hiệu thể thao hàng đầu thế giới", IsActive = true },
                new Brand { BrandId = 2, Name = "Adidas", Description = "Thương hiệu thể thao nổi tiếng", IsActive = true },
                new Brand { BrandId = 3, Name = "Puma", Description = "Thương hiệu thể thao cao cấp", IsActive = true },
                new Brand { BrandId = 4, Name = "Converse", Description = "Thương hiệu giày sneaker kinh điển", IsActive = true },
                new Brand { BrandId = 5, Name = "Vans", Description = "Thương hiệu giày thể thao đường phố", IsActive = true }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product 
                { 
                    ProductId = 1,
                    Name = "Nike Air Max 270",
                    ProductCode = "NK-AM270-001",
                    Description = "Giày thể thao Nike Air Max 270 với đệm khí tối đa mang lại sự thoải mái vượt trội cho mọi hoạt động",
                    Price = 3500000,
                    DiscountPercent = 10,
                    SalePrice = 3150000,
                    MainImageUrl = "https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/awjogtdnqxniqqk0wpgf/air-max-270-shoes-2V5C4p.png",
                    CategoryId = 1,
                    BrandId = 1,
                    IsFeatured = true,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Product 
                { 
                    ProductId = 2,
                    Name = "Adidas Ultraboost 22",
                    ProductCode = "AD-UB22-001",
                    Description = "Giày chạy bộ Adidas Ultraboost 22 với công nghệ Boost độc quyền mang đến năng lượng vô tận",
                    Price = 4200000,
                    DiscountPercent = 15,
                    SalePrice = 3570000,
                    MainImageUrl = "https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/fbaf991a78bc4896a3e9ad7800abcec6_9366/Ultraboost_22_Shoes_Black_GZ0127_01_standard.jpg",
                    CategoryId = 2,
                    BrandId = 2,
                    IsFeatured = true,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 2)
                },
                new Product 
                { 
                    ProductId = 3,
                    Name = "Puma RS-X",
                    ProductCode = "PM-RSX-001",
                    Description = "Giày sneaker Puma RS-X phong cách retro hiện đại, thiết kế độc đáo và nổi bật",
                    Price = 2800000,
                    MainImageUrl = "https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa,w_600,h_600/global/380462/01/sv01/fnd/PNA/fmt/png/RS-X-Reinvention-Sneakers",
                    CategoryId = 1,
                    BrandId = 3,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 3)
                },
                new Product 
                { 
                    ProductId = 4,
                    Name = "Converse Chuck Taylor All Star",
                    ProductCode = "CV-CT-001",
                    Description = "Giày Converse Chuck Taylor All Star kinh điển, phong cách bất biến theo năm tháng",
                    Price = 1500000,
                    MainImageUrl = "https://www.converse.com/dw/image/v2/BCZC_PRD/on/demandware.static/-/Sites-cnv-master-catalog/default/dw0d236c0e/images/a_107/M9160_A_107X1.jpg",
                    CategoryId = 1,
                    BrandId = 4,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 4)
                },
                new Product 
                { 
                    ProductId = 5,
                    Name = "Vans Old Skool",
                    ProductCode = "VN-OS-001",
                    Description = "Giày Vans Old Skool với thiết kế sọc đặc trưng, phong cách skate đường phố",
                    Price = 1800000,
                    MainImageUrl = "https://images.vans.com/is/image/Vans/VN000D3HY28-HERO",
                    CategoryId = 1,
                    BrandId = 5,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 5)
                },
                new Product 
                { 
                    ProductId = 6,
                    Name = "Nike Air Force 1",
                    ProductCode = "NK-AF1-001",
                    Description = "Nike Air Force 1 - Biểu tượng sneaker huyền thoại với thiết kế cổ điển",
                    Price = 2900000,
                    DiscountPercent = 20,
                    SalePrice = 2320000,
                    MainImageUrl = "https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/b7d9211c-26e7-431a-ac24-b0540fb3c00f/air-force-1-07-shoes-WrLlWX.png",
                    CategoryId = 1,
                    BrandId = 1,
                    IsFeatured = true,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 6)
                },
                new Product 
                { 
                    ProductId = 7,
                    Name = "Adidas Stan Smith",
                    ProductCode = "AD-SS-001",
                    Description = "Adidas Stan Smith - Giày tennis cổ điển với phong cách tối giản thanh lịch",
                    Price = 2500000,
                    MainImageUrl = "https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/3f9882d9b4e744daa0daae7c0115d3f1_9366/Stan_Smith_Shoes_White_FX5500_01_standard.jpg",
                    CategoryId = 1,
                    BrandId = 2,
                    IsFeatured = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 7)
                },
                new Product 
                { 
                    ProductId = 8,
                    Name = "Puma Suede Classic",
                    ProductCode = "PM-SC-001",
                    Description = "Puma Suede Classic - Giày da lộn cao cấp với thiết kế vintage đầy phong cách",
                    Price = 2200000,
                    DiscountPercent = 5,
                    SalePrice = 2090000,
                    MainImageUrl = "https://images.puma.com/image/upload/f_auto,q_auto,b_rgb:fafafa,w_600,h_600/global/374915/01/sv01/fnd/PNA/fmt/png/Suede-Classic-XXI-Sneakers",
                    CategoryId = 1,
                    BrandId = 3,
                    IsBestSeller = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 8)
                }
            );
        }
    }
}
