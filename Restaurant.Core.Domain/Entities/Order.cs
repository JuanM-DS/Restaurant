namespace Restaurant.Core.Domain.Entities
{
    public class Order
    {
        public decimal Subtotal { get; set; }

        public string UserId { get; set; } = null!;

        //Navigators
        public Table Table { get; set; } = null!;

        public ICollection<Dish> SelectedDishes { get; set; } = [];

        public OrderStatus Status { get; set; } = null!;
    }
}