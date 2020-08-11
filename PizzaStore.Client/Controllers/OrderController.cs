using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]/[action]")]
    public class OrderController : Controller
    {
        // private readonly PizzaStoreDbContext _db;
        private OrderViewModel orderViewModel;
        private PizzaViewModel pizzaViewModel;

        public OrderController(PizzaStoreDbContext dbContext)
        {
            // _db = dbContext;
            orderViewModel = new OrderViewModel(dbContext);
            pizzaViewModel = new PizzaViewModel(dbContext);
        }

        public IActionResult Home()
        {
            // user is not logged in
            if (TempData.Peek("UserLoggedIn") is null)
            {
                return Redirect("/User");
            }

            var cart = orderViewModel.ReadOpenOrder(TempData.Peek("UserLoggedIn").ToString());

            // no order in progress, so create a new one
            if (cart is null)
            {
                return View("StoreSelector", new StoreViewModel());
            }

            return View(cart); // see options for current order
        }

        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(StoreViewModel store)
        {
            ModelState.Remove("Name"); // we're not logging in the store, different validation
            if (ModelState.IsValid)
            {
                orderViewModel.CreateOrder(TempData.Peek("UserLoggedIn").ToString(), store.StoreSelected);
                return View("AddPizza", pizzaViewModel);
            }

            return View("StoreSelector", store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPizza(PizzaViewModel pizza)
        {
            // add back in pizza options
            pizza.Presets = pizzaViewModel.Presets;
            pizza.Sizes = pizzaViewModel.Sizes;
            pizza.Crusts = pizzaViewModel.Crusts;
            pizza.Toppings = pizzaViewModel.Toppings;

            // remove properties that may not be set by the user
            ModelState.Remove("Crust");
            ModelState.Remove("SelectedToppings");
            if (ModelState.IsValid)
            {
                if (pizza.PizzaName == "Custom") // if custom, then we add crust and toppings
                {
                    return View("CustomizePizza", pizza);
                }

                // add in preset options
                pizza.Crust = pizzaViewModel.Presets.Find(x => x.Name == pizza.PizzaName).Crust.Name;
                pizza.SelectedToppings = pizzaViewModel.Presets.Find(x => x.Name == pizza.PizzaName).Toppings.Select(x => x.Name).ToList();

                orderViewModel.AddPizza(pizza, TempData.Peek("UserLoggedIn").ToString());
                return Redirect("/Order/Home");
            }
            return View("AddPizza", pizza); // form was not filled out correctly
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomizePizza(PizzaViewModel pizza)
        {
            // add back in pizza options
            pizza.Presets = pizzaViewModel.Presets;
            pizza.Sizes = pizzaViewModel.Sizes;
            pizza.Crusts = pizzaViewModel.Crusts;
            pizza.Toppings = pizzaViewModel.Toppings;

            if (ModelState.IsValid)
            {
                orderViewModel.AddPizza(pizza, TempData.Peek("UserLoggedIn").ToString());
                return Redirect("/Order/Home");
            }
            return View(pizza);
        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public IActionResult AddPizza()
        {
            return View(pizzaViewModel);
        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public IActionResult RemovePizzas()
        {
            var cart = orderViewModel.ReadOpenOrder(TempData.Peek("UserLoggedIn").ToString());
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemovePizzas(OrderViewModel cart)
        {
            if (cart.PizzaIndexes is { })
            {
                orderViewModel.RemovePizzas(cart.PizzaIndexes, TempData.Peek("UserLoggedIn").ToString());
            }
            return Redirect("/Order/Home");
        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            orderViewModel.CancelOrder(TempData.Peek("UserLoggedIn").ToString());
            return Redirect("/User");
        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder()
        {
            orderViewModel.PlaceOrder(TempData.Peek("UserLoggedIn").ToString());
            return Redirect("/");
        }
    }
}