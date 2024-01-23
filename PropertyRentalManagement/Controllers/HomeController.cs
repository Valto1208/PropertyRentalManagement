using Microsoft.AspNetCore.Mvc;
using PropertyRentalManagement.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PropertyRentalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            using (FinalProjectDbContext context = new FinalProjectDbContext())
            {
                var userV = context.Users.FirstOrDefault(user => user.Email== model.Email && user.Password == model.Password);

                if (userV != null)
                {
                    Response.Cookies.Append("UserId", userV.UserId.ToString(), options);
                    Response.Cookies.Append("UserType", userV.UserType.ToString(), options);
                    if (userV.UserType == 1)
                    {
                    return RedirectToAction("OwnerView", "Home");
                        
                    }
                   else if (userV.UserType == 2)
                    {
                        return RedirectToAction("ManagerView", "Home");
                    }
                   else if (userV.UserType == 3)
                    {
                        return RedirectToAction("TenantView", "Appartments");
                    }
                }
                ModelState.AddModelError("", "Invalid email or password!");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (FinalProjectDbContext context = new
            FinalProjectDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ManagerView()
        {
            return View();
        }

        public IActionResult OwnerView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
