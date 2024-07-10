namespace Restaurant.Core.Domain.Common
{
    public abstract class BaseEntity : AuditableBaseEntity
    {
        public int Id { get; set; }
    }
}
