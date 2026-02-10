using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RimuCloud.Domain.Entity.Base;
using RimuCloud.Domain.Entity;

namespace RimuCloud.Persistence.Postgres
{
    public class AppDbContext : DbContext
    {
        private readonly string _currentUserId;

        // Giả sử bạn lấy UserId từ một ICurrentUserService (thông qua HttpContextAccessor)
        public AppDbContext(DbContextOptions<AppDbContext> options, string currentUserId = "System")
            : base(options)
        {
            _currentUserId = currentUserId;
        }

        public DbSet<Entry> Entries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            // Tự động quét và áp dụng Global Query Filter cho các thực thể ISoftDelete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    // Gọi method helper để tạo filter: e => !e.IsDeleted
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                    var falseConstant = Expression.Constant(false);
                    var body = Expression.Equal(property, falseConstant);
                    var lambda = Expression.Lambda(body, parameter);
    
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }
    
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }
    
        private void UpdateAuditFields()
        {
            var now = DateTime.UtcNow;
    
            foreach (var entry in ChangeTracker.Entries())
            {
                // 1. Xử lý Date Tracking & User Tracking
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is IDateTracking dt)
                    {
                        dt.CreatedAt = now;
                    }
                    if (entry.Entity is IUserTracking ut)
                    {
                        ut.CreatedBy = _currentUserId;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IDateTracking dt)
                    {
                        dt.UpdatedAt = now;
                    }
                    if (entry.Entity is IUserTracking ut)
                    {
                        ut.UpdatedBy = _currentUserId;
                    }
                }
    
                // 2. Xử lý Soft Delete
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete sd)
                {
                    // Thay đổi trạng thái từ Deleted sang Modified để Update thay vì Delete
                    entry.State = EntityState.Modified;
                    sd.IsDeleted = true;
                    sd.DeletedAt = now;
                }
            }
        }

    }
}
