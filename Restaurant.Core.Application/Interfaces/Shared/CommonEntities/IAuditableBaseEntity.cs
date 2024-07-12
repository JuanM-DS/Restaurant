namespace Restaurant.Core.Application.Interfaces.Shared.CommonEntities
{
    public interface IAuditableBaseEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTime { get; set; }
    }
}
