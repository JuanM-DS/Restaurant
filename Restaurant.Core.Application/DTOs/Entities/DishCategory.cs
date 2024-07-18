using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Application.DTOs.Entities
{
    public class DishCategory
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<DishDto> Dishes { get; set; } = [];
    }
}
