using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class UserRepository
    {
        private readonly PizzaStoreDbContext _db;

        public UserRepository(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public UserModel Login(string name)
        {
            return _db.Users.SingleOrDefault(x => x.Name == name);
        }

        public bool CreateUser(string UserName)
        {
            if (Login(UserName) == null)
            {
                _db.Users.Add(
                    new UserModel() { Name = UserName }
                );
                return true;
            }
            return false;
        }

        public List<UserModel> ReadAllUsers()
        {
            return _db.Users.ToList();
        }

        public List<OrderModel> ReadOrders(string userName)
        {
            var orders = _db.Orders
                .Where(x => x.UserSubmitted.Name == userName && x.Submitted)
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
    }
}