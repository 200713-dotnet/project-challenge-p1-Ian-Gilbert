using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class UserRepository
    {
        private PizzaStoreDbContext _db;

        public UserRepository(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public UserModel Login(string name)
        {
            return _db.Users.SingleOrDefault(x => x.Name == name);
        }

        public bool CreateUser(UserModel user)
        {
            if (Login(user.Name) == null)
            {
                _db.Users.Add(user);
                return true;
            }
            return false;
        }

        public List<OrderModel> ReadOrders(UserModel user)
        {
            return _db.Orders
                .Where(x => x.UserSubmitted.Name == user.Name)
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
    }
}