using System.Collections.Generic;
using drinks.dal.@interface.services;
using drinks.domain.@interface.entities;
using drinks.domain.@interface.services;

namespace drinks.domain.services
{
    public class CoinService: ICoinService
    {
        private readonly IRepository<Coin> _coinRepository;
        public CoinService(IUnitOfWork unitOfWork)
        {
            _coinRepository = unitOfWork.Repository<Coin>();
        }

        public Coin GetCoinById(long id)
        {
            return _coinRepository.GetById(id);
        }

        public IEnumerable<Coin> GetAllCoins()
        {
            return _coinRepository.GetAll();
        }


        public void Update(Coin drink)
        {
            _coinRepository.Update(drink);
        }
    }
}
