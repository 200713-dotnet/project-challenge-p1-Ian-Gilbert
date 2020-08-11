using System.Collections.Generic;
using PizzaStore.Domain.Models;
using Xunit;

namespace PizzaStore.Testing.Tests
{
    public class MenuPizzaModelTests
    {
        [Fact]
        public void Test_CalculatePrice()
        {
            var sut = new MenuPizzaModel();
            sut.Crust = new CrustModel() { Price = 5 };
            sut.Toppings = new List<ToppingModel>()
            {
                new ToppingModel() { Price = 0.25m },
                new ToppingModel() { Price = 0.5m }
            };

            decimal price = 5.75m;

            Assert.True(sut.CalculatePrice() == price);
        }
    }
}