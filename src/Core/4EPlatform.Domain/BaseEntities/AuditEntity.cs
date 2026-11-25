namespace _4EPlatform.Domain.Models.BaseEntities
{
    public class AuditEntity<TKey> : BaseEntity<TKey>
    {
        public Guid CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public Guid? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
    }
}
