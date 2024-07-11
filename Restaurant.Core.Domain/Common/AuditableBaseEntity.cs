namespace Restaurant.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public string CreateBy { get; set; } = null!;

        public DateTime CreateTime { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTime { get; set; }
    }
}
