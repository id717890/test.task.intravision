namespace drinks.domain.services
{
    using dal.@interface.services;
    using @interface.entities;
    using @interface.services;
    using System.Collections.Generic;

    public class DrinkService: IDrinkService
    {
        private IRepository<Drink> _drinkRepository;
        public DrinkService(IUnitOfWork unitOfWork)
        {
            _drinkRepository = unitOfWork.Repository<Drink>();
        }

        public IEnumerable<Drink> GetAllDrinks()
        {
            return _drinkRepository.GetAll();
        }

        public Drink GetDrinkById(long id)
        {
            return _drinkRepository.GetById(id);
        }

        public Drink Create(string caption, string image, int cost, int count)
        {
            if (caption == null) return null;
            var drink = new Drink
            {
                Caption = caption,
                Image = image,
                Cost = cost,
                Count = count
            };
            _drinkRepository.Create(drink);

            return drink;
        }

        public void Update(Drink drink)
        {
            _drinkRepository.Update(drink);
        }

        public void Delete(long id)
        {
            var file = GetDrinkById(id);
            if (file == null) return;
            _drinkRepository.Delete(file);
        }
    }
}
