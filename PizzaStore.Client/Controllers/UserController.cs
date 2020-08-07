using Microsoft.AspNetCore.Mvc;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]")]
    public class UserController : Controller
    {
        private readonly PizzaStoreDbContext _db;

        public UserController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}