﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using re_do_music.MVC.ViewModels;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(o=>o.OrderItems).ThenInclude(o=>o.Items).ThenInclude(o=>o.Instrument).Include(o => o.Customer).ToList();
            return View(orders);
        }
        public IActionResult UserOrders()
        {
            string userId = User.Identity.Name;

            // Belirli bir kullanıcının siparişlerini getir
            var userOrders = _context.Orders
                .Include(o => o.OrderItems).ThenInclude(o => o.Items).ThenInclude(o => o.Instrument).Include(o => o.Customer)
                .Where(o => o.Customer.UserName == userId)
                .ToList();

            return View(userOrders);
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult AddOrder()
        {
            var basket = HttpContext.Session.Get<Basket>("Basket");
            BasketAndOrderViewModel basketAndOrderViewModel = new BasketAndOrderViewModel();
            if (basket == null)
            {
                basket = new Basket
                {
                    Items = new List<BasketItem>()
                };
            }
            var user = HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                // Kullanıcı oturum açmışsa, User özelliğini doldur
                // Örneğin, user özelliği AppUser tipinde ise:
                basketAndOrderViewModel.User = _context.Users.FirstOrDefault(u => u.UserName == user.Identity.Name);
            }
            basketAndOrderViewModel.Basket = basket;
            return View(basketAndOrderViewModel);
        }

        [HttpPost]
        public IActionResult AddOrder(string orderShippingAddress, Payment orderPaymentMethod)
        {
            var sessionBasket = HttpContext.Session.Get<Basket>("Basket");
            var basket = _context.Baskets.Include(x => x.Items).ThenInclude(x => x.Instrument).FirstOrDefault(x => x.Id == sessionBasket.Id);
            var user = _context.Users.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);

            var order = new Order()
            {
                Customer = user,
                Id = Guid.NewGuid(),
                ShippingAddress = orderShippingAddress,
                PaymentMethod = orderPaymentMethod,
                OrderItems= basket,
                CreatedOn = DateTime.UtcNow,
                OrderDate = DateTime.UtcNow,
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            var viewModel = new BasketAndOrderViewModel()
            {
                Basket = basket,
                PaymentMethod= orderPaymentMethod,
                ShippingAddress= orderShippingAddress,
                User = user
            };
            HttpContext.Session.Remove("Basket");
            return RedirectToAction("OrderSuccess");
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
