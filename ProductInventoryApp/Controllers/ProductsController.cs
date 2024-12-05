using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.Data;
using ProductInventoryApp.Models;
using System.Linq;

namespace ProductInventoryApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .OrderBy(p => p.Price)
                .ToList();

            var mostExpensiveProduct = products.OrderByDescending(p => p.Price).FirstOrDefault();

            ViewBag.MostExpensiveProduct = mostExpensiveProduct;

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

    }
}
