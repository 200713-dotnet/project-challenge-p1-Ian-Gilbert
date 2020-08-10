using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class PizzaModel : AModel
    {
        public int CrustId { get; set; }
        public int SizeId { get; set; }
        public int OrderId { get; set; }
        public CrustModel Crust { get; set; }
        public SizeModel Size { get; set; }
        public List<ToppingModel> Toppings { get; set; }
        public List<PizzaToppingModel> PizzaToppings { get; set; }
        public OrderModel Order { get; set; }
        public decimal Price { get; set; }
    }
}