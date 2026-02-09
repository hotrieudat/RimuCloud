namespace RimuCloud.Domain.Abstractions
{
    public abstract class FullEntity<TKey> : AuditableEntity<TKey>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}