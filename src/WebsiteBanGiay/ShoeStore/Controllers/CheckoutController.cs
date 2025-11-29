using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using ShoeStore.ViewModels;
using ShoeStore.Helpers;

namespace ShoeStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "ShoppingCart";

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Checkout
        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            var user = await _userManager.GetUserAsync(User);
            var model = new CheckoutViewModel
            {
                FullName = user?.FullName ?? "",
                Email = user?.Email ?? "",
                PhoneNumber = user?.PhoneNumber ?? "",
                Address = user?.Address ?? "",
                CartItems = cart
            };

            return View(model);
        }

        // POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(CheckoutViewModel model)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                model.CartItems = cart;
                return View("Index", model);
            }

            var user = await _userManager.GetUserAsync(User);
            
            // Calculate totals
            var subtotal = cart.Sum(item => item.Total);
            var shippingFee = subtotal >= 500000 ? 0 : 30000;
            var total = subtotal + shippingFee;

            // Create order
            var order = new Order
            {
                UserId = user!.Id,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending,
                ShippingMethod = model.ShippingMethod,
                ShippingAddress = model.Address,
                ShippingFee = shippingFee,
                SubTotal = subtotal,
                TotalAmount = total,
                CustomerName = model.FullName,
                CustomerEmail = model.Email,
                CustomerPhone = model.PhoneNumber,
                Notes = model.Notes
            };

            // Add order details and update stock
            foreach (var item in cart)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    TotalPrice = item.Total,
                    Size = item.Size,
                    Color = item.Color
                });

                // Update stock if size is specified
                if (!string.IsNullOrEmpty(item.Size))
                {
                    var productSize = await _context.ProductSizes
                        .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.Size == item.Size);
                    
                    if (productSize != null)
                    {
                        productSize.StockQuantity -= item.Quantity;
                        // Ensure stock doesn't go negative (though validation should prevent this)
                        if (productSize.StockQuantity < 0) productSize.StockQuantity = 0; 
                        _context.Update(productSize);
                    }
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear cart
            HttpContext.Session.Remove(CartSessionKey);

            TempData["SuccessMessage"] = "Đặt hàng thành công! Mã đơn hàng của bạn là: " + order.OrderId;
            return RedirectToAction("Confirmation", new { id = order.OrderId });
        }

        // GET: Checkout/Confirmation
        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // Check if order belongs to current user
            var user = await _userManager.GetUserAsync(User);
            if (order.UserId != user?.Id)
            {
                return Forbid();
            }

            return View(order);
        }

        // Helper methods
        private List<CartItemViewModel> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>(CartSessionKey) 
                   ?? new List<CartItemViewModel>();
        }
    }
}
