using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Domain.Entites;
using ReDoMusic.Persistance.Contexts;

namespace re_do_music.MVC.Controllers
{

    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _context;

        public BrandController()
        {
            _context = new();
        }

        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBrand(string brandName, string brandDisplayText, string brandAddress)
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brandName,
                Address = brandAddress,
                DisplayText = brandDisplayText,
                CreatedOn = DateTime.UtcNow,
            };

            _context.Brands.Add(brand);

            _context.SaveChanges();

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteBrand(string id)
        {
            var brand = _context.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            _context.Brands.Remove(brand);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
