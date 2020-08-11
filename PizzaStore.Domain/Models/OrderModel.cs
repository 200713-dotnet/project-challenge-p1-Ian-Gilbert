using System;
using System.Collections.Generic;
using System.Linq;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserSubmittedId { get; set; }
        public int StoreSubmittedId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Submitted { get; set; }

        public List<PizzaModel> Pizzas { get; set; }
        public UserModel UserSubmitted { get; set; }
        public StoreModel StoreSubmitted { get; set; }

        public OrderModel()
        {
            Pizzas = new List<PizzaModel>();
        }

        public decimal CalculatePrice()
        {
            return Pizzas.Sum(p => p.CalculatePrice());
        }
    }
}