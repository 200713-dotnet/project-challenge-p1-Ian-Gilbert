using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Abstracts
{
    public abstract class ComponentModel : AModel
    {
        public decimal Price { get; set; }
    }
}