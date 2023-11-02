using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Domain.Entites;
using ReDoMusic.Domain.Enums;
using ReDoMusic.Persistance.Contexts;

namespace re_do_music.MVC.Controllers
{

    public class InstrumentController : Controller
    {
        private readonly ReDoMusicDbContext _context;

        public InstrumentController()
        {
            _context = new();
        }

        public IActionResult Index()
        {

            var instruments = _context.Instruments.Include(x=>x.Brand).ToList();
            
            return View(instruments);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddInstrument()
        {
            var brands = _context.Brands.ToList();
            return View(brands); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddInstrument(string instrumentName, string instrumentBrand, string instrumentModel, Color instrumentColor, DateTime? instrumentProductionYear, decimal instrumentPrice)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id.ToString() == instrumentBrand);
            if (brand == null)
            {
                // Hatalı Brand seçimi
                return BadRequest("Invalid brand selection.");
            }
            var instrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = instrumentName,
                Brand= brand,
                Model = instrumentModel,
                Color = instrumentColor,
                ProductionYear = DateTime.UtcNow,
                Price = instrumentPrice,
                CreatedOn = DateTime.UtcNow
            };

            _context.Instruments.Add(instrument);
            
            _context.SaveChanges();

            return RedirectToAction("AddInstrument");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteInstrument(Guid id)
        {
            var instrument = _context.Instruments.FirstOrDefault(x => x.Id == id);

            if (instrument != null)
            {
                _context.Instruments.Remove(instrument);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}



