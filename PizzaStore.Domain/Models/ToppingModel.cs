using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class ToppingModel : ComponentModel
    {
        public List<PizzaToppingModel> PizzaToppings { get; set; }
        public List<MenuPizzaToppingModel> MenuPizzaToppings { get; set; }
    }
}