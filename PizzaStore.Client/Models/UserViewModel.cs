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

        public List<OrderViewModel> Orders { get; set; }
        public List<UserModel> UserList { get; set; }

        [Required(ErrorMessage = "Login failed")]
        public string Name { get; set; }

        public UserViewModel() { }

        public UserViewModel(PizzaStoreDbContext dbContext)
        {
            userRepo = new UserRepository(dbContext);
        }

        public UserModel Login(string name)
        {
            return userRepo.Login(name);
        }
    }
}