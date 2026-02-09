using RimuCloud.Domain.Interfaces.Repository;
using RimuCloud.Persistence.Postgres;
using RimuCloud.Persistence.PostgreSQL.Repository;

namespace RimuCloud.Persistence.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Sử dụng Lazy để trì hoãn việc khởi tạo
        // private readonly Lazy<IRepository<Product, int>> _productRepository;
        // private readonly Lazy<IRepository<Category, int>> _categoryRepository;
        private readonly Lazy<ITodoRepository> _todo;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            // Khởi tạo các Repository cụ thể
            // _productRepository = new Lazy<IRepository<Product, int>>(() => new Repository<Product, int>(_context));
            // _categoryRepository = new Lazy<IRepository<Category, int>>(() => new Repository<Category, int>(_context));
            _todo = new Lazy<ITodoRepository>(() => new TodoRepository());
        }
        // Truy cập thông qua thuộc tính Value của Lazy
        // public IRepository<Product, int> Products => _productRepository.Value;
        // public IRepository<Category, int> Categories => _categoryRepository.Value;
        public ITodoRepository Todo => _todo.Value;


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
    
        public async Task CommitAsync() => await _context.Database.CommitTransactionAsync();

        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}