using drinks.domain.@interface.entities;
using System.Collections.Generic;

namespace drinks.infrastructure.Response
{
    public class DrinkListResponse: BaseResponse
    {
        public IEnumerable<Drink> Drinks { get; set; }
    }

    public class ParticularDrink : BaseResponse
    {
        public Drink Drink { get; set; }
    }
}
