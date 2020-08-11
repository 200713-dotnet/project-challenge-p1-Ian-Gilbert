using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing.Repositories
{
    public class OrderRepository
    {
        private readonly PizzaStoreDbContext _db;

        public OrderRepository(PizzaStoreDbContext dbContext)
        {
            _db = dbContext;
        }

        public void CreateOrder(string userName, string storeName)
        {
            var order = new OrderModel();
            order.UserSubmitted = _db.Users.SingleOrDefault(x => x.Name == userName);
            order.StoreSubmitted = _db.Stores.SingleOrDefault(x => x.Name == storeName);
            order.Price = 0;

            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public OrderModel ReadOpenOrder(string name)
        {
            var order = _db.Orders
                .Include(x => x.UserSubmitted)
                .Include(x => x.StoreSubmitted)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Crust)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.Size)
                .Include(x => x.Pizzas)
                    .ThenInclude(x => x.PizzaToppings)
                        .ThenInclude(x => x.Topping)
                .SingleOrDefault(x => x.UserSubmitted.Name == name && x.Submitted == false);

            if (order is null)
            {
                return null;
            }

            foreach (var pizza in order.Pizzas)
            {
                pizza.Toppings = new List<ToppingModel>();
                foreach (var pizzaTopping in pizza.PizzaToppings)
                {
                    pizza.Toppings.Add(pizzaTopping.Topping);
                }
            }

            return order;
        }

        public void AddPizza(PizzaModel pizza, string userName)
        {
            var order = ReadOpenOrder(userName);

            pizza.PizzaToppings = new List<PizzaToppingModel>();
            foreach (var topping in pizza.Toppings)
            {
                pizza.PizzaToppings.Add(new PizzaToppingModel()
                {
                    Topping = topping
                });
            }

            pizza.Price = pizza.CalculatePrice();

            order.Pizzas.Add(pizza);

            order.Price = order.CalculatePrice();

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void RemovePizzas(List<int> indexes, string userName)
        {
            var order = ReadOpenOrder(userName);

            foreach (var i in indexes)
            {
                // _db.Pizzas.Remove(order.Pizzas[i]);
                var id = order.Pizzas[i].Id;
                _db.PizzaToppings.RemoveRange(
                    _db.PizzaToppings.Where(x => x.PizzaId == id)
                );
                _db.Pizzas.Remove(_db.Pizzas.SingleOrDefault(x => x.Id == id));
            }

            _db.SaveChanges();
        }

        public void SubmitOrder(string userName)
        {
            var order = ReadOpenOrder(userName);
            order.PurchaseDate = DateTime.UtcNow;
            order.Submitted = true;
            order.Price = order.CalculatePrice();

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void CancelOrder(string userName)
        {
            var order = ReadOpenOrder(userName);

            foreach (var id in order.Pizzas.Select(x => x.Id))
            {
                _db.PizzaToppings.RemoveRange(
                    _db.PizzaToppings.Where(x => x.PizzaId == id)
                );
                _db.Pizzas.Remove(
                    _db.Pizzas.SingleOrDefault(x => x.Id == id)
                );
            }

            _db.Orders.Remove(
                _db.Orders.SingleOrDefault(x => x.UserSubmitted.Name == userName && !x.Submitted)
            );

            _db.SaveChanges();
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