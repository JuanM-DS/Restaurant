using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<Dish> Dishes { get; set; } = [];
    }
}