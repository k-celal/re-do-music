using ReDoMusic.Domain.Entites;
using ReDoMusic.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace re_do_music.MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly ReDoMusicDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(ReDoMusicDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var basket = HttpContext.Session.Get<Basket>("Basket");

            if (basket == null)
            {
                basket = new Basket
                {
                    Items = new List<BasketItem>()
                };
            }

            return View(basket);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        [HttpGet]
        public IActionResult AddToBasket(Guid productId, int quantity)
        {
            var basket = HttpContext.Session.Get<Basket>("Basket");
            var instrument = _context.Instruments.Include(x => x.Brand).FirstOrDefault(x => x.Id == productId);

            if (basket == null)
            {
                basket = new Basket
                {
                    Id = Guid.NewGuid(),
                    Items = new List<BasketItem>(),
                };

                _context.Baskets.Add(basket);
            }

            var basketItem = basket.Items.FirstOrDefault(x => x.Instrument.Id == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    Id = Guid.NewGuid(),
                    Instrument = instrument,
                    Quantity = quantity
                };

                _context.BasketItems.Add(basketItem);
                basket.Items.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += quantity;
            }

            HttpContext.Session.Set("Basket", basket);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult UpdateBasket(Guid basketId, Guid productId, int quantity)
        {
            var basket = HttpContext.Session.Get<Basket>("Basket");

            if (basket != null)
            {
                var item = basket.Items.FirstOrDefault(i => i.Instrument.Id == productId);
                if (item != null)
                {
                    item.Quantity = quantity;
                    HttpContext.Session.Set("Basket", basket);
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult RemoveFromBasket(Guid productId)
        {
            var basket = HttpContext.Session.Get<Basket>("Basket");

            if (basket != null)
            {
                var item = basket.Items.FirstOrDefault(i => i.Instrument.Id == productId);
                if (item != null)
                {
                    basket.Items.Remove(item);
                    HttpContext.Session.Set("Basket", basket);
                }
            }

            return RedirectToAction("Index");
        }

    }
}