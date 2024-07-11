using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<Order> Orders { get; set; } = [];
    }
}