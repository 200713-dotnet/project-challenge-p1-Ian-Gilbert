using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class OrderViewModel
    {
        public List<UserModel> Users { get; set; }
        public List<StoreModel> Stores { get; set; }
        public List<PizzaModel> PresetPizzas { get; set; }





        public List<PizzaViewModel> Pizzas { get; set; }
        public string UserName { get; set; }
        public string StoreName { get; set; }
    }
}