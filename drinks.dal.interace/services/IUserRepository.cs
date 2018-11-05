namespace drinks.dal.@interface.services
{
    using domain.@interface.entities;

    public interface IUserRepository : IRepository<User>
    {
        bool FindUserBySecret(string secret);
    }
}
