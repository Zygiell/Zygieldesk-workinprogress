using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Domain.Common
{
    public class AuditableEntity
    {
        public virtual User? CreatedBy { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual User? LastModifiedBy { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}