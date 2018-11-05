using System.Linq;
using drinks.dal.@interface.services;
using drinks.domain.@interface.entities;

namespace drinks.dal.services
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DrinkContext context) : base(context)
        {
        }

        public bool FindUserBySecret(string secret)
        {
            return GetAll().Count(x => x.SecretKey == secret) > 0;
        }
    }
}
