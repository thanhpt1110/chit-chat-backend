using ChitChat.Domain.Common;

namespace ChitChat.Domain.Entities
{
    public abstract class BaseAuditedEntity : BaseEntity, IAuditedEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
