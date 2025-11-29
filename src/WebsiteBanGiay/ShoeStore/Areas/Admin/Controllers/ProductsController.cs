using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(string search, int? categoryId, int? brandId, int page = 1)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.ProductCode.Contains(search));
                ViewBag.Search = search;
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
                ViewBag.CategoryId = categoryId;
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId);
                ViewBag.BrandId = brandId;
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Brands = await _context.Brands.ToListAsync();

            var pageSize = 10;
            var totalItems = await query.CountAsync();
            var products = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;

            return View(products);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? mainImage)
        {
            ModelState.Remove("Category");
            ModelState.Remove("Brand");
            
            // Remove validation for navigation properties in collections
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("ProductSizes") && k.Contains(".Product")).ToList();
            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }
            
            keysToRemove = ModelState.Keys.Where(k => k.StartsWith("ProductColors") && k.Contains(".Product")).ToList();
            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }
            
            // Debug: Log received sizes and colors
            Console.WriteLine($"ProductSizes Count: {product.ProductSizes?.Count ?? 0}");
            Console.WriteLine($"ProductColors Count: {product.ProductColors?.Count ?? 0}");
            
            if (product.ProductSizes != null)
            {
                foreach (var size in product.ProductSizes)
                {
                    Console.WriteLine($"Size: {size.Size}, Stock: {size.StockQuantity}");
                }
            }
            
            if (product.ProductColors != null)
            {
                foreach (var color in product.ProductColors)
                {
                    Console.WriteLine($"Color: {color.ColorName}, Code: {color.ColorCode}");
                }
            }
            
            if (ModelState.IsValid)
            {
                if (mainImage != null)
                {
                    string folder = "images/products/";
                    folder += Guid.NewGuid().ToString() + "_" + mainImage.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await mainImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    product.MainImageUrl = "/" + folder;
                }

                product.CreatedAt = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            // If we got here, something failed, redisplay form
            Console.WriteLine("ModelState is invalid:");
            foreach (var error in ModelState)
            {
                if (error.Value.Errors.Any())
                {
                    Console.WriteLine($"  {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? mainImage)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            ModelState.Remove("Category");
            ModelState.Remove("Brand");
            
            // Remove validation for navigation properties in collections
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("ProductSizes") && k.Contains(".Product")).ToList();
            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }
            
            keysToRemove = ModelState.Keys.Where(k => k.StartsWith("ProductColors") && k.Contains(".Product")).ToList();
            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Products
                        .Include(p => p.ProductSizes)
                        .Include(p => p.ProductColors)
                        .FirstOrDefaultAsync(p => p.ProductId == id);

                    if (existingProduct == null) return NotFound();

                    // Update basic info
                    existingProduct.Name = product.Name;
                    existingProduct.ProductCode = product.ProductCode;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.DiscountPercent = product.DiscountPercent;
                    existingProduct.SalePrice = product.SalePrice; // Should be calculated but let's trust the binder or recalc
                    // Recalculate SalePrice if DiscountPercent changed
                    if (existingProduct.DiscountPercent.HasValue)
                    {
                        existingProduct.SalePrice = existingProduct.Price * (100 - existingProduct.DiscountPercent.Value) / 100;
                    }
                    else
                    {
                        existingProduct.SalePrice = null;
                    }

                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.BrandId = product.BrandId;
                    existingProduct.IsFeatured = product.IsFeatured;
                    existingProduct.IsBestSeller = product.IsBestSeller;
                    existingProduct.IsActive = product.IsActive;
                    existingProduct.UpdatedAt = DateTime.Now;

                    if (mainImage != null)
                    {
                        string folder = "images/products/";
                        folder += Guid.NewGuid().ToString() + "_" + mainImage.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        await mainImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        existingProduct.MainImageUrl = "/" + folder;
                    }

                    // Update Sizes
                    // 1. Remove deleted sizes
                    var incomingSizeIds = product.ProductSizes.Select(s => s.ProductSizeId).Where(sid => sid != 0).ToList();
                    var sizesToRemove = existingProduct.ProductSizes.Where(s => !incomingSizeIds.Contains(s.ProductSizeId)).ToList();
                    foreach (var size in sizesToRemove)
                    {
                        _context.ProductSizes.Remove(size);
                    }

                    // 2. Add or Update sizes
                    foreach (var size in product.ProductSizes)
                    {
                        if (size.ProductSizeId == 0)
                        {
                            // New
                            existingProduct.ProductSizes.Add(size);
                        }
                        else
                        {
                            // Update
                            var existingSize = existingProduct.ProductSizes.FirstOrDefault(s => s.ProductSizeId == size.ProductSizeId);
                            if (existingSize != null)
                            {
                                existingSize.Size = size.Size;
                                existingSize.StockQuantity = size.StockQuantity;
                            }
                        }
                    }

                    // Update Colors
                    // 1. Remove deleted colors
                    var incomingColorIds = product.ProductColors.Select(c => c.ProductColorId).Where(cid => cid != 0).ToList();
                    var colorsToRemove = existingProduct.ProductColors.Where(c => !incomingColorIds.Contains(c.ProductColorId)).ToList();
                    foreach (var color in colorsToRemove)
                    {
                        _context.ProductColors.Remove(color);
                    }

                    // 2. Add or Update colors
                    foreach (var color in product.ProductColors)
                    {
                        if (color.ProductColorId == 0)
                        {
                            // New
                            existingProduct.ProductColors.Add(color);
                        }
                        else
                        {
                            // Update
                            var existingColor = existingProduct.ProductColors.FirstOrDefault(c => c.ProductColorId == color.ProductColorId);
                            if (existingColor != null)
                            {
                                existingColor.ColorName = color.ColorName;
                                existingColor.ColorCode = color.ColorCode;
                            }
                        }
                    }
                    
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            // If we got here, something failed, redisplay form
            Console.WriteLine("ModelState is invalid in Edit:");
            foreach (var error in ModelState)
            {
                if (error.Value.Errors.Any())
                {
                    Console.WriteLine($"  {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            return View(product);
        }

        // POST: Admin/Products/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã {(product.IsActive ? "kích hoạt" : "vô hiệu hóa")} sản phẩm!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Products/ToggleFeatured/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFeatured(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.IsFeatured = !product.IsFeatured;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã {(product.IsFeatured ? "đánh dấu" : "bỏ đánh dấu")} sản phẩm nổi bật!";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
