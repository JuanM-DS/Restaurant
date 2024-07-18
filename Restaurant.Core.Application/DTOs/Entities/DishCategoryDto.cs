namespace Restaurant.Core.Application.DTOs.Entities
{
    public class DishCategoryDto
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<DishDto> Dishes { get; set; } = [];
    }
}
