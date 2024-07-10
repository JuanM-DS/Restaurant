using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Portions { get; set; }

        public int CategoryId { get; set; }

        //Navigators
        public ICollection<Ingredient> Ingredients { get; set; } = [];

        public ICollection<Order> Ordes { get; set; } = [];

        public DishCategory Category { get; set; } = null!;
    }
}
