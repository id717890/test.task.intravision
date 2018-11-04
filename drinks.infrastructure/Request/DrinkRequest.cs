namespace drinks.infrastructure.Request
{
    public class DrinkRequest
    {
        public class CreateDrink
        {
            public string Caption { get; set; }
            public int Cost { get; set; }
            public int Count { get; set; }
            public string Image { get; set; }
        }
    }
}
