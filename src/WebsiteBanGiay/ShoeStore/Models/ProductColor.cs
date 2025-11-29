using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class ProductColor
    {
        [Key]
        public int ProductColorId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ColorName { get; set; } = string.Empty;

        [StringLength(7)]
        public string? ColorCode { get; set; } // Hex color code

        public bool IsAvailable { get; set; } = true;

        // Navigation property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
