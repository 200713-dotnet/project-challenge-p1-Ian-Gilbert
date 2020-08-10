using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class OrderRepository
    {
        private PizzaStoreDbContext _db;

        public OrderRepository(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public void CreateOrder(UserModel user, StoreModel store)
        {
            var order = new OrderModel();
            order.UserSubmitted = _db.Users.SingleOrDefault(x => x.Name == user.Name);
            order.StoreSubmitted = _db.Stores.SingleOrDefault(x => x.Name == store.Name);

            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public OrderModel ReadOpenOrder(UserModel user)
        {
            return _db.Orders
                .Include(x => x.UserSubmitted)
                .Include(x => x.StoreSubmitted)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Crust)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.PizzaToppings)
                        .ThenInclude(x => x.Topping)
                .SingleOrDefault(x => x.UserSubmitted.Name == user.Name && x.Submitted == false);
        }

        public void AddPizza(PizzaModel pizza, UserModel user)
        {
            var order = ReadOpenOrder(user);
            order.Pizzas.Add(pizza);

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void RemovePizzas(List<int> indexes, UserModel user)
        {
            var order = ReadOpenOrder(user);
            var pizzas = new List<PizzaModel>();

            foreach (var i in indexes)
            {
                pizzas.Add(order.Pizzas[i]);
            }

            foreach (var pizza in pizzas)
            {
                order.Pizzas.Remove(pizza);
            }

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void SubmitOrder(UserModel user)
        {
            var order = ReadOpenOrder(user);
            order.PurchaseDate = DateTime.UtcNow;
            order.Submitted = true;

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void CancelOrder(UserModel user)
        {
            _db.Orders.Remove(
                _db.Orders.SingleOrDefault(x => x.UserSubmitted.Name == user.Name && !x.Submitted)
            );
        }

        public List<CrustModel> ReadCrusts()
        {
            return _db.Crusts.ToList();
        }

        public List<SizeModel> ReadSizes()
        {
            return _db.Sizes.ToList();
        }

        public List<ToppingModel> ReadToppings()
        {
            return _db.Toppings.ToList();
        }

        public List<MenuPizzaModel> ReadPrests()
        {
            return _db.MenuPizzas
                .Include(x => x.Crust)
                .Include(x => x.MenuPizzaToppings)
                    .ThenInclude(x => x.Topping)
                .ToList();
        }

        public List<StoreModel> ReadAllStores()
        {
            return _db.Stores.ToList();
        }
    }
}