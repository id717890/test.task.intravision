namespace drinks.infrastructure.Request
{
    using System.Collections.Generic;

    public class BuyRequest
    {
        public int TotalCost { get; set; }
        public Dictionary<int, int> Coins { get; set; }
        public List<int> Drinks { get; set; }
    }
}
