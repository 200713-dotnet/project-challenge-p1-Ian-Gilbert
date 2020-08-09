using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Client.Models
{
    public class UserViewModel
    {
        public List<OrderViewModel> Orders { get; set; }

        [VerifyUser]
        public string Name { get; set; }
    }

    public class VerifyUserAttribute : ValidationAttribute
    {
        public string GetErrorMessage()
        {
            return "Login failed";
        }

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