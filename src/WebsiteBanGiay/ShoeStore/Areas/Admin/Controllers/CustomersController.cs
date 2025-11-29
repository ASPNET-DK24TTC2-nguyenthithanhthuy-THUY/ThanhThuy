using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Customers
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));
                ViewBag.Search = search;
            }

            var pageSize = 10;
            var totalItems = await query.CountAsync();
            var customers = await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;

            return View(customers);
        }

        // GET: Admin/Customers/Details/id
        public async Task<IActionResult> Details(string id)
        {
            var customer = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            // Get customer orders
            ViewBag.Orders = await _context.Orders
                .Where(o => o.UserId == id)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            ViewBag.TotalOrders = await _context.Orders.CountAsync(o => o.UserId == id);
            ViewBag.TotalSpent = await _context.Orders
                .Where(o => o.UserId == id && o.Status == OrderStatus.Delivered)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            return View(customer);
        }
    }
}
