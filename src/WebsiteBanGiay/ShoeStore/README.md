# Website Bán Giày - ShoeStore 👟

## 📖 Giới thiệu
**ShoeStore** là một nền tảng thương mại điện tử chuyên nghiệp dành cho việc kinh doanh giày dép, được xây dựng trên nền tảng công nghệ mạnh mẽ **ASP.NET Core 9.0 MVC**. Dự án cung cấp một giải pháp toàn diện từ trải nghiệm mua sắm mượt mà cho khách hàng đến hệ thống quản trị hiệu quả cho chủ cửa hàng.

Dự án được thiết kế với kiến trúc **Monolithic**, tuân thủ các nguyên tắc thiết kế hiện đại, dễ dàng mở rộng và bảo trì.

## 🚀 Công nghệ sử dụng

### Backend
*   **Framework**: ASP.NET Core 9.0 MVC
*   **Ngôn ngữ**: C# 12
*   **Database**: SQL Server
*   **ORM**: Entity Framework Core 9.0 (Code First Approach)
*   **Authentication & Authorization**: ASP.NET Core Identity
*   **Dependency Injection**: Built-in Container

### Frontend
*   **View Engine**: Razor Views (.cshtml)
*   **CSS Framework**: Bootstrap 5 (Responsive Design)
*   **Scripting**: JavaScript (ES6+), jQuery
*   **UI Components**: FontAwesome Icons, Google Fonts

### Tools & DevOps
*   **IDE**: Visual Studio 2022 / Visual Studio Code
*   **Version Control**: Git
*   **Package Manager**: NuGet, LibMan

## ✨ Chức năng chính

### 👤 Phân hệ Người dùng (Customer Site)
1.  **Trải nghiệm Mua sắm**:
    *   **Trang chủ**: Hiển thị Banner, Sản phẩm nổi bật, Sản phẩm mới nhất.
    *   **Danh sách sản phẩm**: Phân trang, Lọc theo Danh mục, Thương hiệu, Khoảng giá.
    *   **Chi tiết sản phẩm**: Hình ảnh sắc nét, mô tả chi tiết, chọn Size/Màu sắc.
    *   **Tìm kiếm**: Tìm kiếm sản phẩm theo tên nhanh chóng.

2.  **Giỏ hàng & Thanh toán**:
    *   **Giỏ hàng (Cart)**: Thêm/Sửa/Xóa sản phẩm, tự động tính tổng tiền.
    *   **Thanh toán (Checkout)**: Quy trình đặt hàng đơn giản, hỗ trợ nhập thông tin giao hàng.
    *   **Lịch sử đơn hàng**: Theo dõi trạng thái đơn hàng đã đặt.

3.  **Tiện ích Cá nhân**:
    *   **Tài khoản**: Đăng ký, Đăng nhập, Quản lý hồ sơ cá nhân.
    *   **Wishlist (Yêu thích)**: Lưu lại các sản phẩm quan tâm để mua sau.

### 🛠 Phân hệ Quản trị (Admin Panel)
*Truy cập qua đường dẫn `/Admin`*

1.  **Dashboard (Tổng quan)**:
    *   Thống kê nhanh số lượng đơn hàng, doanh thu, khách hàng mới.
    *   Biểu đồ hoặc danh sách tóm tắt hoạt động kinh doanh.

2.  **Quản lý Sản phẩm (Products)**:
    *   Danh sách sản phẩm với đầy đủ thông tin.
    *   Thêm mới sản phẩm với hình ảnh, giá, mô tả.
    *   Chỉnh sửa hoặc xóa sản phẩm.

3.  **Quản lý Đơn hàng (Orders)**:
    *   Tiếp nhận đơn hàng mới.
    *   Cập nhật trạng thái đơn hàng (Pending -> Processing -> Shipped -> Completed).
    *   Xem chi tiết thông tin người mua và sản phẩm trong đơn.

4.  **Quản lý Khách hàng (Customers)**:
    *   Xem danh sách khách hàng đã đăng ký thành viên.
    *   Quản lý thông tin cơ bản của khách hàng.

## 📝 Use Cases (Trường hợp sử dụng điển hình)

### UC1: Khách vãng lai mua hàng (Guest Checkout)
1.  Người dùng truy cập trang web, xem các sản phẩm "Hot".
2.  Người dùng chọn một đôi giày ưng ý, chọn size 42 và bấm "Thêm vào giỏ".
3.  Người dùng vào Giỏ hàng kiểm tra và bấm "Thanh toán".
4.  Hệ thống yêu cầu đăng nhập hoặc cho phép nhập thông tin giao hàng trực tiếp (tùy cấu hình).
5.  Người dùng hoàn tất đặt hàng và nhận thông báo thành công.

### UC2: Thành viên lưu sản phẩm yêu thích
1.  Người dùng đăng nhập vào hệ thống.
2.  Khi lướt xem sản phẩm, người dùng thấy một đôi giày đẹp nhưng chưa muốn mua ngay.
3.  Người dùng bấm vào biểu tượng "Trái tim" (Wishlist).
4.  Sản phẩm được lưu vào trang "Sản phẩm yêu thích" để xem lại sau.

### UC3: Admin xử lý đơn hàng
1.  Admin đăng nhập vào trang quản trị.
2.  Admin nhận thấy có đơn hàng mới ở trạng thái "Pending".
3.  Admin kiểm tra kho, đóng gói hàng.
4.  Admin cập nhật trạng thái đơn hàng sang "Shipped" để thông báo cho khách biết hàng đang đi.

## ⚙️ Hướng dẫn Cài đặt & Chạy dự án

### Yêu cầu hệ thống
*   .NET SDK 9.0
*   SQL Server (LocalDB hoặc SQL Server Express/Enterprise)

### Các bước triển khai

1.  **Clone dự án**:
    ```bash
    git clone https://github.com/your-username/ShoeStore.git
    cd ShoeStore
    ```

2.  **Cấu hình Database**:
    *   Mở file `appsettings.json`.
    *   Kiểm tra chuỗi kết nối `DefaultConnection` xem đã phù hợp với máy của bạn chưa.

3.  **Khởi tạo Database & Seed Data**:
    ```bash
    dotnet restore
    dotnet ef database update
    ```
    *Lệnh này sẽ tạo database và các bảng cần thiết, đồng thời nạp dữ liệu mẫu (nếu có).*

4.  **Chạy ứng dụng**:
    ```bash
    dotnet run
    ```
    *   Website User: `https://localhost:5001`
    *   Trang Admin: `https://localhost:5001/Admin`

### Tài khoản Admin mặc định (nếu có Seed Data)
*   **Email**: `admin@shoestore.com`
*   **Password**: `Admin@123` (Hoặc mật khẩu được cấu hình trong `DbInitializer`)

## 📂 Cấu trúc thư mục

```
ShoeStore/
├── Areas/Admin/        # Phân hệ quản trị (Controllers, Views)
├── Controllers/        # Controllers cho người dùng (Home, Product, Cart...)
├── Data/               # DbContext, Migrations
├── Models/             # Entity Classes (Product, Order, User...)
├── ViewModels/         # Models phục vụ riêng cho View
├── Views/              # Giao diện người dùng (Razor)
├── wwwroot/            # Static files (CSS, JS, Images)
├── Program.cs          # Cấu hình ứng dụng & Middleware
└── appsettings.json    # Cấu hình môi trường & Database
```

---
**ShoeStore** - Nâng niu bàn chân Việt.
*Dự án được phát triển cho mục đích học tập và thực hành ASP.NET Core MVC.*
