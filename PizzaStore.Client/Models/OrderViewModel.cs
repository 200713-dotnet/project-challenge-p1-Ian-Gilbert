using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class OrderViewModel
    {
        public List<PizzaModel> Pizzas { get; set; }
    }
}