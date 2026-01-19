using Clothing.Areas.AdminPanel.ViewModels;
using Clothing.Data;
using Clothing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="Admin, SuperAdmin")]
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private ClothingDbContext _context { get; }
        public ProductController(ClothingDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c => !c.IsDeleted));
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM vm) 
        {
            if (!ModelState.IsValid) return View(vm);
            Product product = new Product
            {
                Title=vm.title,
                Description=vm.description,
                Price=vm.price,
                Review=vm.review,
                IsDeleted=false
            };
            if (vm.imageFile!=null) 
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create)) 
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL=fileName;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            if (id==null) return BadRequest();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i=>i.Id==id);
            if (product ==null) return NotFound();
            product.IsDeleted=true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id==null) return BadRequest();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==id);
            if (product ==null) return NotFound();
            UpdateVM vm = new UpdateVM
            {
                title=product.Title,
                description=product.Description,
                price=product.Price,
                review=product.Review,
                id_=product.Id
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==vm.id_);
            if (product ==null) return NotFound();

            product.Title=vm.title;
            product.Description=vm.description;
            product.Price=vm.price;
            product.Review=vm.review;

            if (vm.imageFile!=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL=fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
