using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Factories;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class PizzaViewModel
    {
        // out to the client
        public List<CrustModel> Crusts { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }

        // in from the client
        [Required(ErrorMessage = "Must select a crust")]
        public string Crust { get; set; }
        [Required(ErrorMessage = "Must select a size")]
        public string Size { get; set; }
        [Required(ErrorMessage = "Must select between 2 and 5 toppings")]
        [MinLength(2, ErrorMessage = "Must select between 2 and 5 toppings")]
        [MaxLength(5, ErrorMessage = "Must select between 2 and 5 toppings")]
        public List<string> SelectedToppings { get; set; }
        public bool SelectedTopping { get; set; }

        public PizzaViewModel()
        {
            // var pf = new PizzaFactory();
            Crusts = new List<CrustModel>()
            {
                new CrustModel() { Name = "Thin" },
                new CrustModel() { Name = "Chicago" }
            };
            Sizes = new List<SizeModel>()
            {
                new SizeModel() { Name = "Small" },
                new SizeModel() { Name = "Medium" },
                new SizeModel() { Name = "Large" }
            };
            Toppings = new List<ToppingModel>()
            {
                new ToppingModel() { Name = "Sauce" },
                new ToppingModel() { Name = "Cheese" },
                new ToppingModel() { Name = "Pepperoni" },
                new ToppingModel() { Name = "Mushrooms" },
                new ToppingModel() { Name = "Ham" },
                new ToppingModel() { Name = "Pineapple" }
            };
        }
    }
}