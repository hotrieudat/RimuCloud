namespace RimuCloud.Domain.Entity.Base
{
    public abstract class AuditableEntity<TKey> : EntityBase<TKey>, IDateTracking, IUserTracking
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }

    public interface IDateTracking
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }

    public interface IUserTracking
    {
        string CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
    }
}