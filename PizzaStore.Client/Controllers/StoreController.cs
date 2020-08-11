using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    [Route("/[controller]/{action=Home}")]
    public class StoreController : Controller
    {
        // private readonly PizzaStoreDbContext _db;
        private StoreViewModel storeViewModel;
        private UserViewModel userViewModel;

        public StoreController(PizzaStoreDbContext dbContext)
        {
            storeViewModel = new StoreViewModel(dbContext);
            userViewModel = new UserViewModel(dbContext);
        }

        [HttpGet]
        public IActionResult Home()
        {
            if (TempData.Peek("StoreLoggedIn") == null)
            {
                return View("Login", new StoreViewModel());
            }
            return View(new StoreViewModel() { Name = TempData.Peek("StoreLoggedIn").ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(StoreViewModel store)
        {
            // super bodge job to verify login
            if (storeViewModel.Login(store.Name) is null)
            {
                store.Name = null;
            }

            ModelState.Remove("StoreSelected");
            if (ModelState.IsValid)
            {
                TempData["StoreLoggedIn"] = store.Name;
                TempData.Keep("StoreLoggedIn");
                return Redirect("/Store");
            }
            return View();
        }

        [HttpGet]
        public IActionResult NewStore()
        {
            return View("CreateStore", new StoreViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStore(StoreViewModel store)
        {
            ModelState.Remove("StoreSelected");
            if (ModelState.IsValid)
            {
                var newStore = storeViewModel.CreateStore(store.Name);

                if (newStore != null)
                {
                    TempData["StoreLoggedIn"] = store.Name;
                    TempData.Keep("StoreLoggedIn");
                    return Redirect("/Store");
                }
            }
            return View(store);
        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult OrderHistory()
        {
            return View(storeViewModel.OrderHistory(TempData.Peek("StoreLoggedIn").ToString()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderHistory(UserViewModel user)
        {
            ModelState.Remove("Name");
            if (ModelState.IsValid)
            {
                return View(storeViewModel.OrderHistory(TempData.Peek("StoreLoggedIn").ToString(), user.UserSelected));
            }
            return View("UserSelector", userViewModel);
        }

        [HttpGet]
        public IActionResult OrderHistoryByUser()
        {
            return View("UserSelector", userViewModel);
        }
    }
}