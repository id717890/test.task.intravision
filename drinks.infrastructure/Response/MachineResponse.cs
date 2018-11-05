namespace drinks.infrastructure.Response
{
    using drinks.domain.@interface.entities;
    using System.Collections.Generic;

    public class MachineResponse:BaseResponse
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public IEnumerable<Coin> Coins { get; set; }
    }

    public class BuyResponse: BaseResponse
    {
        public List<KeyValuePair<Coin, int>> Refund { get; set; }
        public int RefundTotal { get; set; }
    }
}
