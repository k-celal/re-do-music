using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using re_do_music.MVC.ViewModels;
using ReDoMusic.Domain.Entites;
using re_do_music.MVC.Extensions;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Persistance.Contexts;

namespace re_do_music.MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ReDoMusicDbContext _context;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ReDoMusicDbContext context)
        {
            _UserManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactModel);
            }

            var contactMessage = new ContactMessage
            {
                Id = Guid.NewGuid(),
                Name = contactModel.Name,
                Email = contactModel.Email,
                Message = contactModel.Message,
                CreatedAt = DateTime.UtcNow
            };

            // Entity Framework veya veri erişim yönteminize göre veriyi veritabanına kaydedin.
            _context.ContactMessages.Add(contactMessage);
            _context.SaveChanges();

            return RedirectToAction("Index", "Instrument");
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _UserManager.CreateAsync(
                new() { UserName = request.UserName, Email = request.Email, PhoneNumber = request.Phone },
                password: request.PasswordConfirm);


            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index","Instrument" );

            var hasUser = await _UserManager.FindByEmailAsync(model.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış.");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, false);

            if (signInResult.Succeeded)
            {

                return Redirect(returnUrl);
            }

            ModelState.AddModelErrorList(new List<string>() { $"Email veya şifre yanlış." });


            return View();


        }

    }
}
