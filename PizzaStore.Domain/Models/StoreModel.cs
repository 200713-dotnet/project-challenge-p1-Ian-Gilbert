using System.Collections.Generic;
using PizzaStore.Domain.Abstracts;

namespace PizzaStore.Domain.Models
{
    public class StoreModel : AModel
    {
        public List<OrderModel> Orders { get; set; }
    }
}