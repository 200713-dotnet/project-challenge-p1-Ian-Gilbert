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
        private readonly PizzaStoreDbContext _db;
        public OrderViewModel Cart { get; set; }

        public OrderController(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Home()
        {
            if (TempData.Peek("UserLoggedIn") == null) // user is not logged in
            {
                return Redirect("/User");
            }

            if (Cart == null) // no order in progress, so create a new one
            {
                return View("StoreSelector", new StoreViewModel());
            }

            return View(Cart); // see options for current order
        }

        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(StoreViewModel store)
        {
            ModelState.Remove("Name"); // we're not logging in the store, different validation
            if (ModelState.IsValid)
            {
                Cart = new OrderViewModel()
                {
                    User = new UserModel() { Name = TempData.Peek("UserLoggedIn").ToString() },
                    Store = new StoreModel() { Name = store.StoreSelected }
                };
                return View("NewPizza", new PizzaViewModel());
            }

            return View("StoreSelector", store);
        }

        [ValidateAntiForgeryToken]
        public IActionResult AddPizza(PizzaViewModel pizza)
        {
            // remove properties that may not be set by the user
            ModelState.Remove("Crust");
            ModelState.Remove("SelectedToppings");
            if (ModelState.IsValid)
            {
                if (pizza.PizzaName == "Custom") // if custom, then we add crust and toppings
                {
                    return View("CustomizePizza", pizza);
                }
                pizza.Crust = pizza.Presets.Find(x => x.Name == pizza.PizzaName).Crust.Name;
                pizza.SelectedToppings = pizza.Presets.Find(x => x.Name == pizza.PizzaName).Toppings.Select(x => x.Name).ToList();
                // Cart.Pizzas.Add(pizza);
                return Redirect("/Order/Home");
            }

            return View("NewPizza", pizza); // form was not filled out correctly

            // if (ModelState.IsValid) // if a custom pizza is finished
            // {
            //     // Cart.Pizzas.Add(pizza);
            //     return Redirect("/Order/Home");
            // }
            // else
            // {
            //     // remove properties that may not be set by the user and check again
            //     ModelState.Remove("Crust");
            //     ModelState.Remove("SelectedToppings");
            //     if (ModelState.IsValid)
            //     {
            //         if (pizza.PizzaName == "Custom") // if custom, then we add crust and toppings
            //         {
            //             return View("CustomizePizza", pizza);
            //         }
            //         else // speciatly pizza - add preset crust and toppings
            //         {
            //             pizza.Crust = pizza.Presets.Find(x => x.Name == pizza.PizzaName).Crust.Name;
            //             pizza.SelectedToppings = pizza.Presets.Find(x => x.Name == pizza.PizzaName).Toppings.Select(x => x.Name).ToList();
            //             // Cart.Pizzas.Add(pizza);
            //             return Redirect("/Order/Home");
            //         }
            //     }
            // }
            // return View("NewPizza", pizza); // form was not filled out correctly
        }

        public IActionResult CustomizePizza(PizzaViewModel pizza)
        {
            if (ModelState.IsValid)
            {
                // Cart.Pizzas.Add(pizza);
                return Redirect("/Order/Home");
            }
            return View(pizza);
        }

        [ValidateAntiForgeryToken]
        public IActionResult NewPizza()
        {
            return View(new PizzaViewModel());
        }

        [ValidateAntiForgeryToken]
        public IActionResult RemovePizza(PizzaViewModel pizza)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            return Redirect("/User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(PizzaViewModel pizzaViewModel)
        {
            if (ModelState.IsValid)
            {
                // repository.Create(pizzaViewModel);
                return View("Home", Cart);
            }

            return View("Order", pizzaViewModel);
        }
    }
}