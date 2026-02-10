using RimuCloud.Domain.Entity.Models;
using RimuCloud.Domain.Interfaces.Repository;

namespace RimuCloud.Persistence.PostgreSQL.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly object _lock = new { };
        private readonly List<TodoEntity> _db = new();
        public async Task<TodoEntity> AddItem(TodoEntity item, CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                if (_db.Any(i => i.Id == item.Id))
                    throw new Exception("Item already exists");
                item.Id = Guid.NewGuid();
                item.Done = true;
                _db.Add(item);
            }

            return await Task.FromResult(item);
        }
    }
}
