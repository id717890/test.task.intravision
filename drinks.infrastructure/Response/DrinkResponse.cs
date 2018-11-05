namespace drinks.infrastructure.Response
{
    using domain.@interface.entities;
    using System.Collections.Generic;

    public class DrinkListResponse: BaseResponse
    {
        public IEnumerable<Drink> Drinks { get; set; }
    }

    public class ParticularDrink : BaseResponse
    {
        public Drink Drink { get; set; }
    }
}
