namespace drinks.web.Models
{
    using drinks.domain.@interface.entities;
    using System.Collections.Generic;

    public class MachineViewModel
    {
        public class Machine
        {
            public IEnumerable<Coin> Coins { get; set; }
            public List<Drink> Drinks { get; set; }
            public int Paid { get; set; }
            public int Refund { get; set; }
            public int TotalCost { get; set; }
            public List<int> Basket { get; set; }
        }
    }
}