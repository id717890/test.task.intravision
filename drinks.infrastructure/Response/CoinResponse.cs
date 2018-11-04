using System.Collections.Generic;
using drinks.domain.@interface.entities;

namespace drinks.infrastructure.Response
{
    public class CoinResponse
    {
        public class CoinsListResponse: BaseResponse
        {
            public IEnumerable<Coin> Coins { get; set; }
        }

        public class ParticularCoin : BaseResponse
        {
            public Coin Coin { get; set; }
        }
    }
}
