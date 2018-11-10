namespace drinks.web.Controllers
{
    using infrastructure;
    using infrastructure.Request;
    using infrastructure.Response;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Mvc;


    public class HomeController : Controller
    {
        /// <summary>
        /// Сбрасывает корзину, монеты в автомате, сдачу
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetAll()
        {
            Session.Remove("drinks");
            Session.Remove("coins");
            Session.Remove("paid");
            Session.Remove("refund");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Совершает покупку наптка(ов)
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public ActionResult BuyDrink(int cost)
        {
            var paid = (int?)Session["paid"] ?? 0;
            // Проверяем хватает ли внесенной суммы для совершения покупки
            if (cost > paid)
            {
                TempData["errors"] = "Не достаточно денег для завершения покупки!";
                return RedirectToAction("Index", "Home");
            }

            //Проверяем наличие напитков в корзине
            if ((Session["drinks"] == null) || ((List<int>)Session["drinks"]).Count == 0)
            {
                TempData["errors"] = "Корзина пуста!";
                return RedirectToAction("Index", "Home");
            }

            BuyResponse response = new BuyResponse();
            try
            {
                BuyRequest request = new BuyRequest
                {
                    Drinks = (List<int>)Session["drinks"],
                    Coins = (Dictionary<int, int>)Session["coins"],
                    TotalCost = cost
                };
                HttpResponseMessage result = HttpService.PostAsync("api/machine/Buy", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsAsync<BuyResponse>().Result;

                    //Если АПИ вернул успех, очищаем корзину, монеты в автомате, выдаем сдачу и сообщение
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        Session.Remove("drinks");
                        Session.Remove("coins");
                        Session.Remove("paid");
                        Session.Remove("refund");
                        var message = "Напиток куплен. ";
                        if (response.Refund != null && response.Refund.Count > 0)
                        {
                            message += "СДАЧА = ";
                            var refund = 0;
                            foreach (var refundItem in response.Refund)
                            {
                                refund += refundItem.Key.Value * refundItem.Value;
                                message += refundItem.Key.Caption + " - " + refundItem.Value + " шт.; ";
                            }
                            Session["refund"] = refund;
                        }
                        TempData["success"] = message;
                    }
                    else
                    {
                        TempData["errors"] = response.Message;
                    }
                    return RedirectToAction("Index", "Home");
                }
                response.ErrorCode = 1;
                response.Message = "Ошибка получения данных";
            }
            catch (Exception ex)
            {
                response.ErrorCode = 2;
                response.Message = ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Отменяет внесенные монеты
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelPaid()
        {
            Session.Remove("paid");
            Session.Remove("coins");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Отменяет выбранные напитки
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelOrder()
        {
            Session.Remove("drinks");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Забираем и обнуляем сдачу
        /// </summary>
        /// <returns></returns>
        public ActionResult TakeRefund()
        {
            Session.Remove("refund");
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Удаляем из корзины конкретный напиток
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult RemoveDrinkFromBasket(int value)
        {
            var drinks = Session["drinks"] != null ? (List<int>)Session["drinks"] : new List<int>();
            if (drinks.Contains(value)) drinks.Remove(value);
            Session["drinks"] = drinks;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Добавляет в корзину напиток
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult AddDrinkToBasket(int value)
        {
            var drinks = Session["drinks"] != null ? (List<int>)Session["drinks"] : new List<int>();
            if (!drinks.Contains(value)) drinks.Add(value);
            Session["drinks"] = drinks;
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Добавляет в автомат монету
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult PayCoin(int id, int value)
        {
            var paid = (int?)Session["paid"] ?? 0;
            Session["paid"] = paid + value;
            var coinsDictionary = Session["coins"] != null ? (Dictionary<int, int>)Session["coins"] : new Dictionary<int, int>();
            if (!coinsDictionary.ContainsKey(id))
            {
                coinsDictionary.Add(id, 1);
            }
            else
            {
                coinsDictionary[id] = coinsDictionary[id] + 1;
            }
            Session["coins"] = coinsDictionary;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Главная страница автомата
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            MachineResponse response = new MachineResponse();
            try
            {
                HttpResponseMessage result = HttpService.PostAsync("api/machine/GetMachine", new DefaultRequest()).Result;
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsAsync<MachineResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        MachineViewModel.Machine machine = new MachineViewModel.Machine
                        {
                            Drinks = response.Drinks.ToList(),
                            Coins = response.Coins
                        };

                        //Внесенные монеты
                        machine.Paid = (int?)Session["paid"] ?? 0;
                        //Сдача если она есть
                        machine.Refund = (int?)Session["refund"] ?? 0;


                        var drinks = Session["drinks"] != null ? (List<int>)Session["drinks"] : new List<int>();
                        machine.Basket = drinks;
                        foreach (var drink in machine.Basket)
                        {
                            try
                            {
                                var find = machine.Drinks.SingleOrDefault(x => x.Id == Convert.ToInt64(drink));
                                if (find != null) machine.TotalCost += find.Cost;
                            }
                            catch { };
                        }
                        return View(machine);
                    }
                }
                response.ErrorCode = 1;
                response.Message = "Ошибка получения данных";
            }
            catch (Exception ex)
            {
                response.ErrorCode = 2;
                response.Message = ex.Message;
            }
            return View("Index");
        }

    }
}