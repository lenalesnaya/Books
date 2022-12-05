namespace Books.ServerApp.DomainModel
{
    public abstract class BaseEntity<IdType>
    {
        public IdType Id { get; set; }
    }
}