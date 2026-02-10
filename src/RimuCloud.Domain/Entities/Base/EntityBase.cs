namespace RimuCloud.Domain.Entity.Base
{
    public abstract class EntityBase<TKey> 
    {
        public TKey Id { get; set; }
    }
}   