namespace Restaurant.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTime { get; set; }
    }
}
