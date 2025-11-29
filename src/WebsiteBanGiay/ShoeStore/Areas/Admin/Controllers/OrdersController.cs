using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index(OrderStatus? status, int page = 1)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
                ViewBag.Status = status;
            }

            var pageSize = 10;
            var totalItems = await query.CountAsync();
            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;

            // Count by status
            ViewBag.PendingCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Pending);
            ViewBag.ConfirmedCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Confirmed);
            ViewBag.ProcessingCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Processing);
            ViewBag.ShippingCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Shipping);
            ViewBag.DeliveredCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Delivered);
            ViewBag.CancelledCount = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Cancelled);

            return View(orders);
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            order.UpdatedAt = DateTime.Now;

            // If delivered, mark as paid
            if (status == OrderStatus.Delivered && !order.IsPaid)
            {
                order.IsPaid = true;
                order.PaidAt = DateTime.Now;
                order.PaymentStatus = PaymentStatus.Paid;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã cập nhật trạng thái đơn hàng!";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
