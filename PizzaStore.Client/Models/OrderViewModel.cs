using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class OrderViewModel
    {
        public decimal Price { get; set; }
        public DateTime DateOrdered { get; set; }
        public UserModel User { get; set; }
        public StoreModel Store { get; set; }
        public List<PizzaViewModel> Pizzas { get; set; }
    }
}