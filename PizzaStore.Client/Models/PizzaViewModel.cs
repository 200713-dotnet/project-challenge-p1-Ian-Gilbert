using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaStore.Domain.Factories;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
    public class PizzaViewModel
    {
        // out to the client
        public List<MenuPizzaModel> Presets { get; set; }
        public List<CrustModel> Crusts { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }


        // in from the client

        [Required(ErrorMessage = "Must select a pizza base")]
        public string PizzaName { get; set; }

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
            Presets = new List<MenuPizzaModel>()
            {
                new MenuPizzaModel() { Name = "Cheese", Crust = new CrustModel() { Name = "Thin" }, Toppings = new List<ToppingModel>() { new ToppingModel() { Name = "Sauce" }, new ToppingModel() { Name = "Cheese" } } },
                new MenuPizzaModel() { Name = "Pepperoni", Crust = new CrustModel() { Name = "Thin" }, Toppings = new List<ToppingModel>() { new ToppingModel() { Name = "Sauce" }, new ToppingModel() { Name = "Cheese" }, new ToppingModel() { Name = "Pepperoni" } } },
                new MenuPizzaModel() { Name = "Hawaiian", Crust = new CrustModel() { Name = "Thin" }, Toppings = new List<ToppingModel>() { new ToppingModel() { Name = "Sauce" }, new ToppingModel() { Name = "Cheese" }, new ToppingModel() { Name = "Ham" }, new ToppingModel() { Name = "Pineapple" } } },
                new MenuPizzaModel() { Name = "Custom" }
            };

            // var pf = new PizzaFactory();
            Crusts = new List<CrustModel>()
            {
                new CrustModel() { Name = "Thin" },
                new CrustModel() { Name = "Thick" },
                new CrustModel() { Name = "Garlic" },
                new CrustModel() { Name = "Garlic Stuffed" }
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
                new ToppingModel() { Name = "Sausage" },
                new ToppingModel() { Name = "Olives" },
                new ToppingModel() { Name = "Mushrooms" },
                new ToppingModel() { Name = "Ham" },
                new ToppingModel() { Name = "Pineapple" },
                new ToppingModel() { Name = "Mozzarella" },
                new ToppingModel() { Name = "Basil" }
            };
        }
    }
}