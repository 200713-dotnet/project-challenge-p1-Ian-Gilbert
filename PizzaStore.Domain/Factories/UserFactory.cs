using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Factories
{
    public class UserFactory : IFactory<UserModel>
    {
        public UserModel Create()
        {
            return new UserModel();
        }
    }
}