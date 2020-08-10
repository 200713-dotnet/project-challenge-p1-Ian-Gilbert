using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class UserViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public List<UserModel> UserList { get; set; }

        [Required(ErrorMessage = "Login failed")]
        [VerifyUser]
        public string Name { get; set; }
    }

    internal class VerifyUserAttribute : ValidationAttribute
    {
        private string GetErrorMessage() => "Login failed";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((string)value is null) // or user is in database
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}