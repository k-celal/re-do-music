using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using re_do_music.MVC.Areas.Admin.Models;
using ReDoMusic.Domain.Entites;

namespace re_do_music.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();
            var userViewModelList = new List<UserViewModel>();

            foreach (var user in userList)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName
                };

                if (userRoles != null && userRoles.Any())
                {
                    userViewModel.Roles = userRoles.ToArray();
                }
                else
                {
                    userViewModel.Roles = new string[] { "No roles" }; // Eğer roller boşsa varsayılan bir değer atanabilir
                }

                userViewModelList.Add(userViewModel);
            }

            return View(userViewModelList);
        }
    }
}
