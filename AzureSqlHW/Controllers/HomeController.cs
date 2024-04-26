using AzureSqlHW.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AzureSqlHW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MuradovContext _context;

        public HomeController(ILogger<HomeController> logger, MuradovContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? categoryId)
        {
            IQueryable<Product> products = _context.Products.Include(p => p.ProductCategory);

            if (categoryId.HasValue && categoryId > 0)
                products = products.Where(p => p.ProductCategoryId == categoryId || p.ProductCategory.ParentProductCategoryId == categoryId);


            var categories = _context.ProductCategories.ToList();
            ViewData["Categories"] = categories;
            var productsList = products.ToList();
            return View(productsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
