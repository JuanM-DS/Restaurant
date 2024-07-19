using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.DTOs.Entities
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Subtotal { get; set; }

        public string? UserId { get; set; }

        public int TableId { get; set; }

        public int StatusId { get; set; }

        public List<DishDto> SelectedDishes { get; set; } = [];
    }
}