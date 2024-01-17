using finalfreelancing.Data;

using finalfreelancing.Migrations;
using finalfreelancing.Models;
using finalfreelancing.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.AccessControl;

namespace finalfreelancing.Controllers
{
    public class AccountController : Controller
    {



        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _db;
        private IConfiguration _config;
        //CommonHelper _helper;




        public AccountController(
              UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
               RoleManager<IdentityRole> roleManager,
            AppDbContext db

,
            IConfiguration config)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            //_helper = new CommonHelper(_config);
            _config = config;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult CreateRole(Role role)
        //{
        //    if (role == null) { return NotFound(); }
        //    if (ModelState.IsValid)
        //    {
        //        _db.Roles.Add(role);
        //        _db.SaveChanges();
        //        return RedirectToAction("RolesList");
        //    }
        //    return View(role);
        //}

        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }


        [HttpPost]

        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role); 
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                ModelState.AddModelError("", "Not Created");
                return View(model);
            }
            return View(model);
        }


        //public IActionResult RolesList()
        //{
        //    return View(_db.Roles);
        //}
        public IActionResult Register()
        {
            //ViewBag.roles = new SelectList(_db.Roles, "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email= model.Email,
                    UserName= model.Email,
                    PhoneNumber=model.Mobile
                };

                var result = await _userManager.CreateAsync(user, model.Password!);


              if (result.Succeeded)
              {
                return RedirectToAction("Login","Account");
              }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);

            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
                }
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        //private bool SignInMethod(string userName, string password)
        //{
        //    bool flag = false;
        //    string query = $"select * from [UserTable] where UserName='{userName} and Password
        //        var userDetails = _helper.GetUserByUserName(query);
        //    string roleQuery = $"select * from [Role] Where Id='{userDetails.RoleId}'";
        //    var roles = _helper.GetEntityById(roleQuery);
        //    RoleViewModel vm = new RoleViewModel();
        //    vm.Id roles.Id;
        //    vm.Name roles.Name;
        //    if (userDetails.UserName != null)
        //    {
        //        flag = true;
        //        if (vm.Name == "Admin")
        //        {
        //            HttpContext.Session.SetString("Role", "Admin");
        //            HttpContext.Session.SetString("UserName", userDetails.UserName);
        //        }
        //        else
        //        {

        //            HttpContext.Session.SetString("Role", "user");
        //            HttpContext.Session.SetString("UserName", userDetails.UserName);
        //        }

        //                else
        //        {

        //            ViewBag.Error
        //            = "UserName & Password wrong";
        //        }
        //        return flag;





        //    }
        //}

    }
}



