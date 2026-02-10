using Microsoft.AspNetCore.Identity;
using RimuCloud.Domain.Entity.Base;

namespace RimuCloud.Domain.Entity
{
    public class UserEntity : IdentityUser<Guid>, IDateTracking, IUserTracking, ISoftDelete
        { 
            // Các trường Audit
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }
            public string CreatedBy { get; set; } = "System";
            public string? UpdatedBy { get; set; }
            public bool IsDeleted { get; set; } = false;
            public DateTime? DeletedAt { get; set; }
        }
}