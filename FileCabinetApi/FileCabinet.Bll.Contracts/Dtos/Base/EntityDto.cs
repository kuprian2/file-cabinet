namespace FileCabinet.Bll.Contracts.Dtos.Base
{
    public abstract class EntityDto<TKey>
    {
        public TKey Id { get; set; }
    }
}
