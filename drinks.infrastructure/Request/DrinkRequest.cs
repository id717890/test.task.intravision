namespace drinks.infrastructure.Request
{
    using System.Collections.Generic;
    using domain.@interface.entities;

    public class DrinkRequest
    {
        public class CreateDrink
        {
            public string Caption { get; set; }
            public int Cost { get; set; }
            public int Count { get; set; }
            public string Image { get; set; }
        }

        public class FindDrinkById
        {
            public long Id { get; set; }
        }

        public class EditDrink
        {
            public long Id { get; set; }
            public string Caption { get; set; }
            public int Cost { get; set; }
            public int Count { get; set; }
            public string Image { get; set; }
        }

        public class ImportDrinkList
        {
            public IEnumerable<Drink> Drinks { get; set; }
        }
    }
}
