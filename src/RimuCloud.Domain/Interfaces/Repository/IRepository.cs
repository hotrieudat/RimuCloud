using RimuCloud.Domain.Abstractions;

namespace RimuCloud.Domain.Interfaces.Repository
{
    public interface IRepository<T, TKey> where T : EntityBase<TKey>
    {
        // Truy vấn
        IQueryable<T> GetAll();
        IQueryable<T> GetAllAsNoTracking();
        Task<T?> GetByIdAsync(TKey id);
        
        // Thao tác dữ liệu
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity); // Sẽ tự hiểu là Soft hay Hard delete nhờ DbContext
        
        // Lưu thay đổi
        Task<int> SaveChangesAsync();
    }
    
}