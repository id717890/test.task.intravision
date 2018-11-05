namespace drinks.infrastructure.Response
{
    using System.Collections.Generic;
    using domain.@interface.entities;

    public class CoinsListResponse: BaseResponse
    {
        public IEnumerable<Coin> Coins { get; set; }
    }

    public class ParticularCoin : BaseResponse
    {
        public Coin Coin { get; set; }
    }
}
