namespace drinks.domain.@interface.services
{
    using System.Collections.Generic;
    using entities;

    public interface ICoinService
    {
        IEnumerable<Coin> GetAllCoins();
        Coin GetCoinById(long id);
        //Drink Create(string caption, string image, int cost, int count);
        void Update(Coin coin);
        //void Delete(long id);
    }
}
