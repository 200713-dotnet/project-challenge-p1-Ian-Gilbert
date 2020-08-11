using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]/{action=Home}")]
    public class UserController : Controller
    {
        // private readonly PizzaStoreDbContext _db;
        private UserViewModel userViewModel;

        public UserController(PizzaStoreDbContext dbContext)
        {
            userViewModel = new UserViewModel(dbContext);
        }

        [HttpGet]
        public IActionResult Home()
        {
            // var pop = new PopulateDb(_db);
            // pop.PopulateComponents();
            // pop.PopulateEntities();
            // pop.PopulateMenu();
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
            // super bodge job to verify login
            if (userViewModel.Login(user.Name) is null)
            {
                user.Name = null;
            }

            ModelState.Remove("UserSelected");
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
            return View(userViewModel.OrderHistory(TempData.Peek("UserLoggedIn").ToString()));
        }
    }
}