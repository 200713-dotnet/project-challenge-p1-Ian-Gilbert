using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]/{action=Home}")]
    public class UserController : Controller
    {
        private readonly PizzaStoreDbContext _db;
        // private UserViewModel _user { get; set; }

        public UserController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        public IActionResult Home()
        {
            if (TempData.Peek("UserLoggedIn") == null)
            {
                return View("Login", new UserViewModel());
            }
            return View(new UserViewModel() { Name = TempData.Peek("UserLoggedIn").ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                TempData["UserLoggedIn"] = user.Name;
                TempData.Keep("UserLoggedIn");
                return Redirect("/User");
            }
            return View();
        }

        // [HttpPost]
        public IActionResult Logout()
        {
            TempData.Clear();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}