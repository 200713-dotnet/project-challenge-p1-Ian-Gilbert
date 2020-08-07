using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Client.Models
{
    public class UserViewModel
    {
        public List<OrderViewModel> Orders { get; set; }

        [Required(ErrorMessage = "Login failed")]
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
            if ((string)value is null) // should be a database call
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}