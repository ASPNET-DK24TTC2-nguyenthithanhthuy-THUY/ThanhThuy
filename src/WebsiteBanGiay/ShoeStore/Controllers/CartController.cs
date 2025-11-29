using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using ShoeStore.ViewModels;
using ShoeStore.Helpers;

namespace ShoeStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "ShoppingCart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string? size, string? color, int quantity = 1)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Validate Size
            if (product.ProductSizes.Any())
            {
                if (string.IsNullOrEmpty(size))
                {
                    TempData["ErrorMessage"] = "Vui lòng chọn kích thước!";
                    return RedirectToAction("Details", "Products", new { id = productId });
                }

                var selectedSize = product.ProductSizes.FirstOrDefault(s => s.Size == size);
                if (selectedSize == null)
                {
                    TempData["ErrorMessage"] = "Kích thước không hợp lệ!";
                    return RedirectToAction("Details", "Products", new { id = productId });
                }

                if (selectedSize.StockQuantity < quantity)
                {
                    TempData["ErrorMessage"] = $"Kích thước {size} chỉ còn {selectedSize.StockQuantity} sản phẩm!";
                    return RedirectToAction("Details", "Products", new { id = productId });
                }
            }

            // Validate Color
            if (product.ProductColors.Any())
            {
                if (string.IsNullOrEmpty(color))
                {
                    TempData["ErrorMessage"] = "Vui lòng chọn màu sắc!";
                    return RedirectToAction("Details", "Products", new { id = productId });
                }

                var selectedColor = product.ProductColors.FirstOrDefault(c => c.ColorName == color || c.ColorCode == color); // Check by name or code if passed
                // The view passes ColorName or Title. Let's assume ColorName based on JS: this.querySelector('strong').textContent for size, and title for color.
                // In Details.cshtml: title="@color.ColorName"
                // So it passes ColorName.
                
                if (selectedColor == null)
                {
                    // Try matching by code just in case
                     selectedColor = product.ProductColors.FirstOrDefault(c => c.ColorCode == color);
                }

                if (selectedColor == null)
                {
                    TempData["ErrorMessage"] = "Màu sắc không hợp lệ!";
                    return RedirectToAction("Details", "Products", new { id = productId });
                }
            }

            var cart = GetCart();

            // Check if item already exists in cart
            var existingItem = cart.FirstOrDefault(c => 
                c.ProductId == productId && 
                c.Size == size && 
                c.Color == color);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItemViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    ProductImage = product.MainImageUrl,
                    Price = product.SalePrice ?? product.Price,
                    Quantity = quantity,
                    Size = size,
                    Color = color
                };
                cart.Add(cartItem);
            }

            SaveCart(cart);

            TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, string? size, string? color, int quantity)
        {
            if (quantity < 1)
            {
                return BadRequest("Số lượng phải lớn hơn 0");
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => 
                c.ProductId == productId && 
                c.Size == size && 
                c.Color == color);

            if (item != null)
            {
                item.Quantity = quantity;
                SaveCart(cart);
            }

            return Json(new { success = true, total = cart.Sum(c => c.Total) });
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string? size, string? color)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => 
                c.ProductId == productId && 
                c.Size == size && 
                c.Color == color);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/Clear
        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            TempData["SuccessMessage"] = "Đã xóa toàn bộ giỏ hàng!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/GetCartCount
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Json(new { count = cart.Sum(c => c.Quantity) });
        }

        // Helper methods
        private List<CartItemViewModel> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>(CartSessionKey) 
                   ?? new List<CartItemViewModel>();
        }

        private void SaveCart(List<CartItemViewModel> cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
        }
    }
}
