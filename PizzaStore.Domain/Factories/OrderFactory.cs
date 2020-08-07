using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Factories
{
    public class OrderFactory : IFactory<OrderModel>
    {
        public OrderModel Create()
        {
            return new OrderModel();
        }
    }
}