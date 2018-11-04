using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using drinks.domain.@interface.services;
using drinks.infrastructure.Request;
using drinks.infrastructure.Response;
using Ninject;

namespace drinks.api.Controllers
{
    [RoutePrefix("api/coin")]
    public class CoinController : ApiController
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService service)
        {
            _coinService = service;
        }

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

        [HttpPost]
        [Route("GetCoins")]
        public CoinResponse.CoinsListResponse GetDrinks()
        {
            try
            {
                return new CoinResponse.CoinsListResponse
                {
                    Coins = _coinService.GetAllCoins()
                };
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("GetCoinById")]
        public CoinResponse.ParticularCoin GetCoinById(CoinRequest.FindCoinById request)
        {
            try
            {
                return new CoinResponse.ParticularCoin
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
