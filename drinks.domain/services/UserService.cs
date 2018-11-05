using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using drinks.dal.@interface.services;
using drinks.domain.@interface.entities;
using drinks.domain.@interface.services;

namespace drinks.domain.services
{
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
