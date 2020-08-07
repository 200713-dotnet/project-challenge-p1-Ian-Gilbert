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
        private UserModel _user;

        public UserController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            if (_user is null)
            {
                return View("Login", new UserViewModel());
            }

            return View(new UserViewModel() { Name = _user.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _user = new UserModel() { Name = "Ian" };
                return View("Home", user);
            }
            return View();

        }

        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}