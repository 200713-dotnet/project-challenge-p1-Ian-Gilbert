using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class StoreViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public List<StoreModel> StoreList { get; set; }

        [Required(ErrorMessage = "Login failed")]
        [VerifyStore]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must select a store")]
        public string StoreSelected { get; set; }

        public StoreViewModel()
        {
            StoreList = new List<StoreModel>()
            {
                new StoreModel() { Name = "Store1" },
                new StoreModel() { Name = "Store2" },
                new StoreModel() { Name = "Store3" },
                new StoreModel() { Name = "Store4" },
                new StoreModel() { Name = "Store5" }
            };
        }
    }

    internal class VerifyStoreAttribute : ValidationAttribute
    {
        private string GetErrorMessage() => "Login failed";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((string)value is null) // or store is in database
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}