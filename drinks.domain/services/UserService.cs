namespace drinks.domain.services
{
    using dal.@interface.services;
    using @interface.services;

    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository unitOfWork)
        {
            _userRepository = unitOfWork;
        }

        public bool IsAccessAllow(string secret)
        {
            return _userRepository.FindUserBySecret(secret);
        }
    }
}
