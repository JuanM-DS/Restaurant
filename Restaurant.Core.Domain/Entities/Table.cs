using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Table : BaseEntity
    {
        public int Guests { get; set; }

        public string Description { get; set; } = null!;

        public int StatusId { get; set; }

        //Navigators
        public TableStatus Status { get; set; } = null!;

        public ICollection<Order> Ordes { get; set; } = [];
    }
}