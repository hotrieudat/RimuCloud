using RimuCloud.Domain.Interfaces.Repository;
using RimuCloud.Persistence.PostgreSQL.Repositories;

namespace RimuCloud.Persistence.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<ITodoRepository> _todo;

        public UnitOfWork()
        {
            _todo = new Lazy<ITodoRepository>(() => new TodoRepository());
        }

        public ITodoRepository Todo => _todo.Value;
    }
}