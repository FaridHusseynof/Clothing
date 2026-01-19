using Clothing.Data;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.Controllers
{
    public class HomeController : Controller
    {
        private ClothingDbContext _context { get; }
        public HomeController(ClothingDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c=>!c.IsDeleted));
        }
    }
}
