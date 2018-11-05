namespace drinks.domain.@interface.services
{
    using System.Collections.Generic;
    using entities;

    public interface ICoinService
    {
        IEnumerable<Coin> GetAllCoins();
        Coin GetCoinById(long id);
        void Update(Coin coin);
    }
}
