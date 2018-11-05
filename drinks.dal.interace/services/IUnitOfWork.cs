namespace drinks.dal.@interface.services
{
    using domain.@interface.entities;

    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : Entity;
    }
}
