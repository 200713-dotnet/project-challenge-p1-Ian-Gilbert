using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class CrustModel : ComponentModel
    {
        public List<MenuPizzaModel> MenuPizzas { get; set; }
        public List<PizzaModel> Pizzas { get; set; }
    }
}