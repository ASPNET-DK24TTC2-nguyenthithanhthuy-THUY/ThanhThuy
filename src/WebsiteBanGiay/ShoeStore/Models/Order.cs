using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public enum OrderStatus
    {
        Pending = 0,        // Đang chờ xử lý
        Confirmed = 1,      // Đã xác nhận
        Processing = 2,     // Đang xử lý
        Shipping = 3,       // Đang giao hàng
        Delivered = 4,      // Đã giao hàng
        Cancelled = 5,      // Đã hủy
        Returned = 6        // Đã trả hàng
    }

    public enum PaymentMethod
    {
        COD = 0,            // Thanh toán khi nhận hàng
        BankTransfer = 1,   // Chuyển khoản ngân hàng
        CreditCard = 2,     // Thẻ tín dụng
        EWallet = 3         // Ví điện tử
    }

    public enum PaymentStatus
    {
        Pending = 0,        // Chờ thanh toán
        Paid = 1,           // Đã thanh toán
        Failed = 2,         // Thanh toán thất bại
        Refunded = 3        // Đã hoàn tiền
    }

    public enum ShippingMethod
    {
        Standard = 0,       // Tiêu chuẩn
        Express = 1,        // Nhanh
        SuperExpress = 2    // Siêu tốc
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CustomerEmail { get; set; }

        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentMethod PaymentMethod { get; set; }
        
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public ShippingMethod ShippingMethod { get; set; } = ShippingMethod.Standard;

        public bool IsPaid { get; set; } = false;

        public DateTime? PaidAt { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int? CouponId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("CouponId")]
        public virtual Coupon? Coupon { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
