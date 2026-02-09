using Microsoft.EntityFrameworkCore;
using RimuCloud.Domain.Abstractions;
using RimuCloud.Domain.Interfaces.Repository;
using RimuCloud.Persistence.Postgres;

namespace RimuCloud.Persistence.PostgreSQL.Repository
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : EntityBase<TKey>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
    
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
    
        // Lấy tất cả (Đã được Global Filter lọc IsDeleted tự động)
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }
    
        // Lấy tất cả nhưng không theo dõi (tối ưu hiệu năng cho Read-only)
        public virtual IQueryable<T> GetAllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }
    
        public virtual async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
    
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
    
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    
        public virtual void Delete(T entity)
        {
            // Ở đây ta chỉ cần gọi Remove. 
            // Nhờ logic trong DbContext.SaveChangesAsync, 
            // nếu T có ISoftDelete, nó sẽ tự chuyển thành Update IsDeleted = true.
            _dbSet.Remove(entity);
        }
    
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        // Method bổ sung: Truy vấn bao gồm cả các bản ghi đã xóa mềm
        public IQueryable<T> GetAllWithDeleted()
        {
            return _dbSet.IgnoreQueryFilters();
        }
    }
}