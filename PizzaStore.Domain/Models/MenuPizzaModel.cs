using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class MenuPizzaModel : AModel
    {
        public CrustModel Crust { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}