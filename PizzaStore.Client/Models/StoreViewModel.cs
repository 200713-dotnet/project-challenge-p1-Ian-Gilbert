using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;
using PizzaStore.Storing.Repositories;

namespace PizzaStore.Client.Models
{
    public class StoreViewModel
    {
        private readonly StoreRepository storeRepo;

        public List<OrderModel> Orders { get; set; }
        public List<StoreModel> StoreList { get; set; }

        [Required(ErrorMessage = "Login failed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must select a store")]
        public string StoreSelected { get; set; }

        public StoreViewModel() { }

        public StoreViewModel(PizzaStoreDbContext dbContext)
        {
            storeRepo = new StoreRepository(dbContext);

            StoreList = storeRepo.ReadAllStores();
        }

        public StoreModel Login(string storeName)
        {
            return storeRepo.Login(storeName);
        }

        public StoreViewModel OrderHistory(string storeName)
        {
            var storeViewModel = new StoreViewModel();
            storeViewModel.Orders = storeRepo.ReadOrders(storeName);
            return storeViewModel;
        }

        public StoreViewModel OrderHistory(string storeName, string userName)
        {
            var storeViewModel = new StoreViewModel();
            storeViewModel.Orders = storeRepo.ReadOrders(storeName, userName);
            return storeViewModel;
        }
    }
}