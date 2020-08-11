using System;
using System.Collections.Generic;
using System.Linq;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client.Models
{
    public class OrderViewModel
    {
        private readonly OrderRepository repo;

        public decimal Price { get; set; }
        public DateTime DateOrdered { get; set; }
        public UserModel User { get; set; }
        public StoreModel Store { get; set; }
        public List<PizzaModel> Pizzas { get; set; }

        public List<int> PizzaIndexes { get; set; }
        public bool IndexBool { get; set; }

        public OrderViewModel() { }

        public OrderViewModel(PizzaStoreDbContext dbContext)
        {
            repo = new OrderRepository(dbContext);
        }

        public void CreateOrder(string userName, string storeName)
        {
            repo.CreateOrder(userName, storeName);
        }

        public OrderViewModel ReadOpenOrder(string userName)
        {
            var order = repo.ReadOpenOrder(userName);

            if (order is null)
            {
                return null;
            }

            // foreach (var pizza in order.Pizzas)
            // {
            //     pizza.Toppings = new List<ToppingModel>();
            //     foreach (var pizzaTopping in pizza.PizzaToppings)
            //     {
            //         pizza.Toppings.Add(pizzaTopping.Topping);
            //     }
            // }

            return new OrderViewModel()
            {
                Price = order.CalculatePrice(),
                Pizzas = order.Pizzas,
                User = order.UserSubmitted,
                Store = order.StoreSubmitted
            };
        }

        public void AddPizza(PizzaViewModel pizzaViewModel, string userName)
        {
            var pizza = new PizzaModel();
            pizza.Name = pizzaViewModel.PizzaName;
            pizza.Crust = pizzaViewModel.Crusts.Find(x => x.Name == pizzaViewModel.Crust);
            pizza.Size = pizzaViewModel.Sizes.Find(x => x.Name == pizzaViewModel.Size);

            pizza.Toppings = new List<ToppingModel>();
            foreach (var topping in pizzaViewModel.SelectedToppings)
            {
                pizza.Toppings.Add(pizzaViewModel.Toppings.Find(t => t.Name == topping));
            }

            repo.AddPizza(pizza, userName);
        }

        public void RemovePizzas(List<int> indexes, string userName)
        {
            repo.RemovePizzas(indexes, userName);
        }

        public void PlaceOrder(string userName)
        {
            repo.SubmitOrder(userName);
        }

        public void CancelOrder(string userName)
        {
            repo.CancelOrder(userName);
        }
    }
}