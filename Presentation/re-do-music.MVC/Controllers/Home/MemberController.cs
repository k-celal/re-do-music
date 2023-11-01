using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using ReDoMusic.Domain.Entites;

namespace re_do_music.MVC.Controllers.Home
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
           
        }
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Instrument");
            
        }
        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {
            string message = string.Empty;
            ViewBag.message = "Bu sayfayı görmeye yetkiniz yoktur. Yetki almak için yöneticiniz ile konuşunuz.";
            return View();
        }
    }
}
