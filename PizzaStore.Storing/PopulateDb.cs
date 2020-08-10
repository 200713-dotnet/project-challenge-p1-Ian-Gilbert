using System.Collections.Generic;
using System.Linq;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing
{
    public class PopulateDb
    {
        private PizzaStoreDbContext _db;

        public PopulateDb(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public void PopulateComponents()
        {
            _db.Crusts.Add(new CrustModel() { Name = "Thin", Price = 5 });
            _db.Crusts.Add(new CrustModel() { Name = "Thick", Price = 7 });
            _db.Crusts.Add(new CrustModel() { Name = "Garlic", Price = 7 });
            _db.Crusts.Add(new CrustModel() { Name = "Garlic Stuffed", Price = 10 });

            _db.Sizes.Add(new SizeModel() { Name = "Small", Price = 5 });
            _db.Sizes.Add(new SizeModel() { Name = "Medium", Price = 7 });
            _db.Sizes.Add(new SizeModel() { Name = "Large", Price = 10 });

            _db.Toppings.Add(new ToppingModel() { Name = "Sauce", Price = 0.25m });
            _db.Toppings.Add(new ToppingModel() { Name = "Cheese", Price = 0.25m });
            _db.Toppings.Add(new ToppingModel() { Name = "Pepperoni", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Sausage", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Ham", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Pineapple", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Olives", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Mushrooms", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Mozzarella", Price = 0.5m });
            _db.Toppings.Add(new ToppingModel() { Name = "Basil", Price = 0.25m });

            _db.SaveChanges();
        }

        public void PopulateEntities()
        {
            _db.Users.Add(new UserModel() { Name = "Ian" });
            _db.Users.Add(new UserModel() { Name = "Fred" });

            _db.Stores.Add(new StoreModel() { Name = "Store1" });
            _db.Stores.Add(new StoreModel() { Name = "Store2" });

            _db.SaveChanges();
        }

        public void PopulateMenu()
        {
            var cheese = new MenuPizzaModel();
            cheese.Name = "Cheese";
            cheese.Crust = _db.Crusts.FirstOrDefault(x => x.Name == "Thin");
            cheese.MenuPizzaToppings = new List<MenuPizzaToppingModel>()
            {
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Sauce") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Cheese") }
            };
            _db.MenuPizzas.Add(cheese);

            var pepperoni = new MenuPizzaModel();
            pepperoni.Name = "Pepperoni";
            pepperoni.Crust = _db.Crusts.FirstOrDefault(x => x.Name == "Thin");
            pepperoni.MenuPizzaToppings = new List<MenuPizzaToppingModel>()
            {
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Sauce") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Cheese") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Pepperoni") }
            };
            _db.MenuPizzas.Add(pepperoni);

            var hawaiian = new MenuPizzaModel();
            hawaiian.Name = "Hawaiian";
            hawaiian.Crust = _db.Crusts.FirstOrDefault(x => x.Name == "Thin");
            hawaiian.MenuPizzaToppings = new List<MenuPizzaToppingModel>()
            {
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Sauce") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Cheese") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Ham") },
                new MenuPizzaToppingModel() { Topping = _db.Toppings.FirstOrDefault(x => x.Name == "Pineapple") }
            };
            _db.MenuPizzas.Add(hawaiian);

            var custom = new MenuPizzaModel();
            custom.Name = "Custom";
            _db.MenuPizzas.Add(custom);

            _db.SaveChanges();
        }
    }
}