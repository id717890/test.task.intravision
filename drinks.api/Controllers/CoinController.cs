namespace drinks.api.Controllers
{
    using System;
    using System.Web.Http;
    using domain.@interface.services;
    using infrastructure.Request;
    using infrastructure.Response;

    /// <summary>
    /// Контроллер для управления монетами
    /// </summary>
    [RoutePrefix("api/coin")]
    public class CoinController : ApiController
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService service)
        {
            _coinService = service;
        }

        /// <summary>
        /// Сохраняет данные по монете
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveCoin")]
        public DefaultResponse SaveCoin(CoinRequest.EditCoin request)
        {
            try
            {
                var coin = _coinService.GetCoinById(request.Id);
                if (coin != null)
                {
                    coin.Count = request.Count;
                    coin.IsAllowed = request.IsAllowed;
                }

                _coinService.Update(coin);

                return new DefaultResponse
                {
                    Message = string.Empty,
                    ErrorCode = 0
                };
            }
            catch (Exception e)
            {
                return new DefaultResponse
                {
                    Message = e.Message,
                    ErrorCode = 2
                };
            }
        }

        /// <summary>
        /// Получает список всех монет
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCoins")]
        public CoinsListResponse GetDrinks()
        {
            try
            {
                return new CoinsListResponse
                {
                    Coins = _coinService.GetAllCoins()
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Поиск монеты по ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCoinById")]
        public ParticularCoin GetCoinById(CoinRequest.FindCoinById request)
        {
            try
            {
                return new ParticularCoin
                {
                    Coin = _coinService.GetCoinById(request.Id)
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
