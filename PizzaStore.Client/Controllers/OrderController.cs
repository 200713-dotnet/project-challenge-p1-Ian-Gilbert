using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]")]
    public class OrderController : Controller
    {
        private readonly PizzaStoreDbContext _db;

        public OrderController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            return View("Order", new PizzaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(PizzaViewModel pizzaViewModel)
        {
            if (ModelState.IsValid)
            {
                // repository.Create(pizzaViewModel);
                return View();
            }

            return View("Order", pizzaViewModel);
        }
    }
}