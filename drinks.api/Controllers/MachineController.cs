namespace drinks.api.Controllers
{
    using domain.@interface.entities;
    using domain.@interface.services;
    using infrastructure.Request;
    using infrastructure.Response;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    /// <inheritdoc />
    /// <summary>
    /// Контроллер управления автоматом с соками
    /// </summary>
    [RoutePrefix("api/machine")]
    public class MachineController : ApiController
    {
        private readonly ICoinService _coinService;
        private readonly IDrinkService _drinkService;

        public MachineController(ICoinService coinService, IDrinkService drinkService)
        {
            _coinService = coinService;
            _drinkService = drinkService;
        }

        /// <summary>
        /// Совершает покупку по выбранному заказу
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("Buy")]
        public BuyResponse Buy(BuyRequest request)
        {
            var response = new BuyResponse();
            try
            {
                // Подсчитываем общее кол-во монет с учетом вновь прибывших
                var coinsBuffer = _coinService.GetAllCoins().OrderByDescending(x => x.Value);
                var totalCoins = 0;
                foreach (var coin in coinsBuffer)
                {
                    foreach (var coinOrder in request.Coins)
                    {
                        if (coin.Id == coinOrder.Key)
                        {
                            totalCoins += coin.Value * coinOrder.Value;
                            coin.Count += coinOrder.Value;
                        }

                    }
                }

                foreach (var drinkId in request.Drinks)
                {
                    var drink = _drinkService.GetDrinkById(drinkId);
                    if (drink != null)
                    {
                        drink.Count += -1;
                        _drinkService.Update(drink);
                    }
                }
                var refund = totalCoins - request.TotalCost;
                if (refund != 0) response.Refund = CalculateRefund(refund, coinsBuffer);
                if (response.Refund != null && response.Refund.Count > 0)
                {
                    foreach (var itemDictionary in response.Refund)
                    {
                        itemDictionary.Key.Count += -itemDictionary.Value;
                        _coinService.Update(itemDictionary.Key);
                    }
                }
            }
            catch (Exception e)
            {
                response.ErrorCode = 1;
                response.Message = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Процедура расчета сдачи в монетах
        /// </summary>
        /// <param name="refund"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        private List<KeyValuePair<Coin, int>> CalculateRefund(int refund, IEnumerable<Coin> coins)
        {
            var result = new List<KeyValuePair<Coin, int>>();
            var sum = 0;
            coins = coins.Where(x => x.Count > 0).OrderByDescending(x => x.Value);

            while (sum != refund)
            {
                foreach (var coin in coins)
                {
                    sum = 0;
                    foreach (var item in result)
                    {
                        sum += item.Key.Value * item.Value;
                    }

                    var refundBuf = refund - sum;

                    var intPart = refundBuf / coin.Value;
                    var remPart = refundBuf % coin.Value;

                    if ((remPart == 0) && (intPart > coin.Count))
                    {
                        for (var steps = intPart; steps >= coin.Count && steps != 0; steps += -1)
                        {
                            if ((steps <= coin.Count) && (steps > 0)) result.Add(new KeyValuePair<Coin, int>(coin, steps));
                        }
                    }
                    else
                    if ((remPart == 0) && (intPart <= coin.Count))
                    {
                        result.Add(new KeyValuePair<Coin, int>(coin, intPart));
                        break;
                    }
                    else
                    if ((remPart != 0) && (intPart > 0) && (intPart > coin.Count))
                    {
                        for (var steps = intPart; steps >= coin.Count && steps != 0; steps += -1)
                        {
                            if ((steps <= coin.Count) && (steps > 0)) result.Add(new KeyValuePair<Coin, int>(coin, steps));
                        }
                    }
                    else
                    if ((remPart != 0) && (intPart > 0) && (intPart <= coin.Count))
                    {
                        result.Add(new KeyValuePair<Coin, int>(coin, intPart));
                    }
                }
                sum = result.Sum(item => item.Key.Value * item.Value);
                if (sum != refund) throw new Exception("Автомат не может выдать сдачу");
            }
            return result;
        }

        /// <summary>
        /// Получает модель для отображения автомата
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetMachine")]
        public MachineResponse GetMachine()
        {
            try
            {
                return new MachineResponse
                {
                    Drinks = _drinkService.GetAllDrinks(),
                    Coins = _coinService.GetAllCoins()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
