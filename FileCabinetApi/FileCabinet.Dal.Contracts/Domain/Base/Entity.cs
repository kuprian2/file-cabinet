namespace FileCabinet.Dal.Contracts.Domain.Base
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
