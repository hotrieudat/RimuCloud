namespace RimuCloud.Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoRepository Todo { get; }

        // Các Repository cụ thể
        // IRepository<Product, int> Products { get; }
        // IRepository<Category, int> Categories { get; }

        // Lưu thay đổi
        Task<int> SaveChangesAsync();

        // Hỗ trợ Transaction thủ công nếu cần xử lý phức tạp
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
