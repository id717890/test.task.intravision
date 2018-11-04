namespace drinks.dal.@interface.services
{
    using drinks.domain.@interface.entities;

    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : Entity;
    }
}
