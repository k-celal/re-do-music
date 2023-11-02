using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Domain.Entites;
using ReDoMusic.Domain.Enums;
using ReDoMusic.Persistance.Contexts;

namespace re_do_music.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ReDoMusicDbContext _context;

        public OrderController()
        {
            _context = new();
        }
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(string orderShippingAddress, Payment orderPaymentMethod, OrderStatus orderStatus)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                ShippingAddress = orderShippingAddress,
                PaymentMethod = orderPaymentMethod,
                Status = orderStatus,
                CreatedOn = DateTime.UtcNow,
                OrderDate = DateTime.UtcNow,
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult DeleteOrder(string id)
        {
            var order = _context.Orders.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            _context.Orders.Remove(order);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
