using RimuCloud.Domain.Entities.Models;

namespace RimuCloud.Domain.Interfaces.Repository
{
    public interface ITodoRepository
    {
        Task<TodoEntity> AddItem(TodoEntity item, CancellationToken cancellationToken);
    }
}
