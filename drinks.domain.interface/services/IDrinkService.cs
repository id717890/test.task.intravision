namespace drinks.domain.@interface.services
{
    using System.Collections.Generic;
    using entities;

    public interface IDrinkService
    {
        IEnumerable<Drink> GetAllDrinks();
        Drink GetDrinkById(long id);
        Drink Create(string caption, string image, int cost);
        void Update(Drink drink);
        void Delete(long id);
    }
}
