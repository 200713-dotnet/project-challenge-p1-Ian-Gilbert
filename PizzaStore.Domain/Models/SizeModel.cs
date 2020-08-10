using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class SizeModel : ComponentModel
    {
        public List<PizzaModel> Pizzas { get; set; }
    }
}