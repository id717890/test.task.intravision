namespace drinks.domain.@interface.services
{
    public interface IUserService
    {
        bool IsAccessAllow(string secret);
    }
}
