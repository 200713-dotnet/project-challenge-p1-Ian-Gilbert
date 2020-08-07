using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Factories
{
    public class PizzaFactory : IFactory<PizzaModel>
    {
        public PizzaModel Create()
        {
            var p = new PizzaModel();

            p.Crust = new CrustModel();
            p.Size = new SizeModel();
            p.Toppings = new List<ToppingModel>();

            return p;
        }
    }
}