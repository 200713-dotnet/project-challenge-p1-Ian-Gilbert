namespace PizzaStore.Domain.Models
{
    public class PizzaToppingModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }
        public PizzaModel Pizza { get; set; }
        public ToppingModel Topping { get; set; }
    }
}