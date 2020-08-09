using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]")]
    public class OrderController : Controller
    {
        private readonly PizzaStoreDbContext _db;
        private OrderViewModel _cart { get; set; }

        public OrderController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            System.Console.WriteLine(TempData.Peek("UserLoggedIn").ToString());
            if (_cart is null)
            {
                return View("Order", new PizzaViewModel());
            }
            return View(_cart);
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