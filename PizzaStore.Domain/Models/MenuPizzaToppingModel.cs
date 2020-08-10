namespace PizzaStore.Domain.Models
{
    public class MenuPizzaToppingModel
    {
        public int Id { get; set; }
        public int MenuPizzaId { get; set; }
        public int ToppingId { get; set; }
        public MenuPizzaModel MenuPizza { get; set; }
        public ToppingModel Topping { get; set; }
    }
}