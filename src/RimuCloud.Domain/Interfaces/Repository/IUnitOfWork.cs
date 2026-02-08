namespace RimuCloud.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        ITodoRepository Todo { get; }
    }
}
