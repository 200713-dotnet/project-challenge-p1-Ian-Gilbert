using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class StoreRepository
    {
        private readonly PizzaStoreDbContext _db;

        public StoreRepository(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public StoreModel Login(string name)
        {
            return _db.Stores.SingleOrDefault(x => x.Name == name);
        }

        public bool CreateStore(string StoreName)
        {
            if (Login(StoreName) == null)
            {
                _db.Stores.Add(
                    new StoreModel() { Name = StoreName }
                );
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<StoreModel> ReadAllStores()
        {
            return _db.Stores.ToList();
        }

        public List<OrderModel> ReadOrders(string storeName)
        {
            var orders = _db.Orders
                .Where(x => x.StoreSubmitted.Name == storeName && x.Submitted)
                .Include(x => x.UserSubmitted)
                .Include(x => x.StoreSubmitted)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Crust)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.PizzaToppings)
                        .ThenInclude(x => x.Topping)
                .ToList();

            if (orders is null)
            {
                return null;
            }

            foreach (var order in orders)
            {
                foreach (var pizza in order.Pizzas)
                {
                    pizza.Toppings = new List<ToppingModel>();
                    foreach (var pizzaTopping in pizza.PizzaToppings)
                    {
                        pizza.Toppings.Add(pizzaTopping.Topping);
                    }
                }
            }

            return orders;
        }

        public List<OrderModel> ReadOrders(string storeName, string userName)
        {
            var orders = _db.Orders
                .Where(x => x.StoreSubmitted.Name == storeName && x.UserSubmitted.Name == userName && x.Submitted)
                .Include(x => x.UserSubmitted)
                .Include(x => x.StoreSubmitted)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Crust)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.PizzaToppings)
                        .ThenInclude(x => x.Topping)
                .ToList();

            if (orders is null)
            {
                return null;
            }

            foreach (var order in orders)
            {
                foreach (var pizza in order.Pizzas)
                {
                    pizza.Toppings = new List<ToppingModel>();
                    foreach (var pizzaTopping in pizza.PizzaToppings)
                    {
                        pizza.Toppings.Add(pizzaTopping.Topping);
                    }
                }
            }

            return orders;
        }

        public void ViewWeeklyRevenue(StoreModel store)
        {

        }

        public void ViewMonthlyRevenue(StoreModel store)
        {

        }
    }
}