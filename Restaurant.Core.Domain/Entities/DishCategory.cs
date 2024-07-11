using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class DishCategory : BaseEntity
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<Dish> Dishes { get; set; } = [];
    }
}
