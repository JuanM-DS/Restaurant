namespace Restaurant.Core.Application.DTOs.Entities
{
    public class TableDto
    {
        public int Guests { get; set; }

        public string Description { get; set; } = null!;

        public int StatusId { get; set; }
    }
}