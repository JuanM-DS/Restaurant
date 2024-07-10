using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class TableStatus : BaseEntity
    {
        public string Name { get; set; } = null!;

        //Navigators
        public ICollection<Table> Tables { get; set; } = [];
    }
}