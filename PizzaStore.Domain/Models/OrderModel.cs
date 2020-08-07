using System;
using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class OrderModel : AModel
    {
        public List<PizzaModel> Pizzas { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public UserModel UserSubmitted { get; set; }
        public StoreModel StoreSubmitted { get; set; }
    }
}