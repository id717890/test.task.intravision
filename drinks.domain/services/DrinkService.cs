using drinks.dal.@interface.services;
using drinks.domain.@interface.entities;
using drinks.domain.@interface.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks.domain.services
{
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
