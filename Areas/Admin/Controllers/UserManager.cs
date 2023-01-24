using ExamBilet2.Areas.ViewModels;
using ExamBilet2.DbContextFile;
using ExamBilet2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamBilet2.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class UserManager : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserManager(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "UserName or Password is invalid");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is invalid");
                return View(loginVM);
            }

            return RedirectToAction("index", "dashboard");
        }














  /*      public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser()
            {
                FullName = "Admin",
                UserName = "Admin",
            };
            var result = await _userManager.CreateAsync(admin, "Admin2023");
            return Ok(result);
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");

            var result = await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);

            return Ok(result);
        }

        public async Task<IActionResult> SetRole()
        {
            AppUser user =await _userManager.FindByNameAsync("Admin");
            
            var result = await _userManager.AddToRoleAsync(user, "Admin");

            return Ok(result);
        }*/
    }
}
