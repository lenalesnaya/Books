namespace App.Common.Repositories.Abstractions
{
    public interface IRepository<Entity> : IReadonlyRepository<Entity>
        where Entity : class
    {
        void Add(Entity entity);

        void Update(Entity entity);

        void Remove(Entity entity);
    }
}