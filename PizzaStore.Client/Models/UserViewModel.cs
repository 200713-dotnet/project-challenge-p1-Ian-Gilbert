using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client.Models
{
    public class UserViewModel
    {
        private readonly UserRepository userRepo;

        public List<OrderModel> Orders { get; set; }
        public List<UserModel> UserList { get; set; }

        [Required(ErrorMessage = "Login failed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must select a store")]
        public string UserSelected { get; set; }

        public UserViewModel() { }

        public UserViewModel(PizzaStoreDbContext dbContext)
        {
            userRepo = new UserRepository(dbContext);

            UserList = userRepo.ReadAllUsers();
        }

        public UserModel Login(string name)
        {
            return userRepo.Login(name);
        }

        public UserModel CreateUser(string name)
        {
            if (userRepo.CreateUser(name))
            {
                return Login(name);
            }
            return null;
        }

        public UserViewModel OrderHistory(string userName)
        {
            var userViewModel = new UserViewModel();
            userViewModel.Orders = userRepo.ReadOrders(userName);
            return userViewModel;
        }
    }
}