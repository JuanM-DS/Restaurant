using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal Subtotal { get; set; }

        public string UserId { get; set; } = null!;

        public int TableId { get; set; }

        public int StatusId { get; set; }

        //Navigators
        public Table Table { get; set; } = null!;

        public ICollection<Dish> SelectedDishes { get; set; } = [];

        public OrderStatus Status { get; set; } = null!;
    }
}