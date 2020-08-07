using Microsoft.AspNetCore.Mvc;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]")]
    public class StoreController : Controller
    {
        private readonly PizzaStoreDbContext _db;

        public StoreController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}