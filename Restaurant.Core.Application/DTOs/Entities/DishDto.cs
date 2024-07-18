namespace Restaurant.Core.Application.DTOs.Entities
{
    public class DishDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Portions { get; set; }

        public int CategoryId { get; set; }
    }
}
