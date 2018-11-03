namespace drinks.domain.@interface.entities
{
    public class Coin: Entity
    {
        public string Caption { get; set; }
        public int Value { get; set; }
        public int Count { get; set; }
        public bool IsAllowed { get; set; }
    }
}
