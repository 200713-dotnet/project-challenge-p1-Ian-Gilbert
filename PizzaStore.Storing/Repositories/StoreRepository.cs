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
                return true;
            }
            return false;
        }

        public List<UserModel> ReadAllUsers()
        {
            return _db.Users.ToList();
        }

        public List<OrderModel> ReadOrders(StoreModel store)
        {
            return _db.Orders
                .Where(x => x.StoreSubmitted.Name == store.Name)
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
        }

        public List<OrderModel> ReadOrders(StoreModel store, UserModel user)
        {
            return _db.Orders
                .Where(x => x.StoreSubmitted.Name == store.Name && x.UserSubmitted.Name == user.Name)
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
        }

        public void ViewWeeklyRevenue(StoreModel store)
        {

        }

        public void ViewMonthlyRevenue(StoreModel store)
        {

        }
    }
}