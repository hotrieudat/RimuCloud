namespace RimuCloud.Domain.Abstractions
{
    public abstract class EntityBase<TKey> 
    {
        public TKey Id { get; set; }
    }
}   