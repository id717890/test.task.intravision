namespace drinks.domain.services
{
    using System.Collections.Generic;
    using dal.@interface.services;
    using @interface.entities;
    using @interface.services;

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
