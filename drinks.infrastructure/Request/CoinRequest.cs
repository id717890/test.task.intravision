namespace drinks.infrastructure.Request
{
    public class CoinRequest
    {
        public class FindCoinById
        {
            public long Id { get; set; }
        }

        public class EditCoin
        {
            public long Id { get; set; }
            public int Count { get; set; }
            public bool IsAllowed { get; set; }
        }
    }
}
