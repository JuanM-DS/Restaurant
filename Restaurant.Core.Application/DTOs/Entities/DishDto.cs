namespace Restaurant.Core.Application.DTOs.Entities
{
    public class DishDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Portions { get; set; }

        public int CategoryId { get; set; }

        public List<IngredientDto> Ingredients { get; set; } = [];
    }
}
