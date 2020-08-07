using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Factories
{
    public class StoreFactory : IFactory<StoreModel>
    {
        public StoreModel Create()
        {
            return new StoreModel();
        }
    }
}