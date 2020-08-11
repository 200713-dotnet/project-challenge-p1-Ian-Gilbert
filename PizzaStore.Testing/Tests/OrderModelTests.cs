using System.Collections.Generic;
using PizzaStore.Domain.Models;
using Xunit;

namespace PizzaStore.Testing.Tests
{
    public class OrderModelTests
    {
        [Fact]
        public void Test_CalculatePrice()
        {
            var sut = new OrderModel();

            sut.Pizzas = new List<PizzaModel>();
            for (int i = 0; i < 10; i++)
            {
                sut.Pizzas.Add(
                    new PizzaModel()
                    {
                        Crust = new CrustModel() { Price = 5 },
                        Size = new SizeModel() { Price = 7.5m },
                        Toppings = new List<ToppingModel>()
                        {
                            new ToppingModel() { Price = 0.25m },
                            new ToppingModel() { Price = 0.5m }
                        }
                    }
                );
            }

            decimal price = 132.50m;

            Assert.True(sut.CalculatePrice() == price);
        }
    }
}