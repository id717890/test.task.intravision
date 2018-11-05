using drinks.domain.@interface.entities;
using drinks.infrastructure;
using drinks.infrastructure.Request;
using drinks.infrastructure.Response;
using drinks.web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace drinks.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ResetAll ()
        {
            Session["drinks"] = null;
            Session["coins"] = null;
            Response.SetCookie(new HttpCookie("paid") { Expires = DateTime.Now.AddDays(-1) });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BuyDrink(int cost)
        {
            int paid = GetPaid();
            // Проверяем хватает ли внесенной суммы для совершения покупки
            if (cost>paid)
            {
                TempData["errors"] = "Не достаточно денег для завершения покупки!";
                return RedirectToAction("Index", "Home");
            }
            
            if ((Session["drinks"] == null) || ((List<int>)Session["drinks"]).Count == 0) {
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
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        Session["drinks"] = null;
                        Session["coins"] = null;
                        Response.SetCookie(new HttpCookie("paid") { Expires = DateTime.Now.AddDays(-1) });
                        TempData["success"] = "Напиток куплен";
                    } else
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

        private int GetPaid()
        {
            try
            {
                var paid = Request.Cookies["paid"];
                if (paid?.Value != null) return Convert.ToInt32(paid.Value);
            } 
            catch
            {
                return 0;
            }
            return 0;
        }

        public ActionResult CancelPaid()
        {
            Response.SetCookie(new HttpCookie("paid") { Expires = DateTime.Now.AddDays(-1) });
            //Response.SetCookie(new HttpCookie("paid")
            //{
            //    Value = string.Empty,
            //    Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            //});
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelOrder()
        {
            Response.SetCookie(new HttpCookie("drinks")
            {
                Value = string.Empty,
                Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TakeRefund()
        {
            Response.SetCookie(new HttpCookie("refund")
            {
                Value = string.Empty,
                Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            });
            return RedirectToAction("Index", "Home");
        }


        public ActionResult RemoveDrinkFromBasket(int value)
        {
            var drinks = Session["drinks"] != null ? (List<int>)Session["drinks"] : new List<int>();
            if (drinks.Contains(value)) drinks.Remove(value);
            Session["drinks"] = drinks;
            //var drinksStr = string.Empty;
            //var drinksCookie = Request.Cookies["drinks"];
            //if (drinksCookie?.Value != null) drinksStr = drinksCookie.Value;
            //var drinks = drinksStr.Split(':').ToList();
            //if (drinks.Contains(value.ToString())) drinks.Remove(value.ToString());
            //Response.SetCookie(new HttpCookie("drinks")
            //{
            //    Value = string.Join(":", drinks),
            //    Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            //});
            return RedirectToAction("Index");
        }

        public ActionResult AddDrinkToBasket(int value)
        {
            var drinks = Session["drinks"] !=null ? (List<int>)Session["drinks"] : new List<int>();
            if (!drinks.Contains(value)) drinks.Add(value);
            Session["drinks"] = drinks;


            //var drinksStr = string.Empty;
            //var drinksCookie = Request.Cookies["drinks"];
            //if (drinksCookie?.Value != null) drinksStr = drinksCookie.Value;
            //var drinks = drinksStr.Split(':').ToList();
            //if (!drinks.Contains(value.ToString())) drinks.Add(value.ToString());
            //Response.SetCookie(new HttpCookie("drinks")
            //{
            //    Value = string.Join(":", drinks),
            //    Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            //});
            return RedirectToAction("Index");
        }


        public ActionResult PayCoin(int id, int value)
        {
            var paid = 0;
            var paidCookie = Request.Cookies["paid"];
            if (paidCookie?.Value != null) paid = Convert.ToInt32(paidCookie.Value);
            Response.SetCookie(new HttpCookie("paid")
            {
                Value = (paid + value).ToString(),
                Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            });
            //var coinsCookie = Request.Cookies["coins"];
            //if (coinsCookie?.Value != null)
            //{
            //    var coinsStr = coinsCookie.Value;
            //    var coinsPairs = coinsStr.Split(';');
            //}
            //var coinsDictionary = GetCoinsFromCookie();
            var coinsDictionary = Session["coins"] !=null ? (Dictionary<int, int>)Session["coins"] : new Dictionary<int, int>();
            if (!coinsDictionary.ContainsKey(id))
            {
                coinsDictionary.Add(id, 1);
            }
            else
            {
                coinsDictionary[id] = coinsDictionary[id] + 1;
            }
            //if (coinsDictionary.Count() > 0)
            //{
            //    var coinsStr = string.Empty;
            //    var i = 1;
            //    foreach (var item in coinsDictionary)
            //    {
            //        if (i != coinsDictionary.Count())
            //        {
            //            coinsStr += item.Key + ":" + item.Value + ";";
            //        }
            //        else
            //        {
            //            coinsStr += item.Key + ":" + item.Value;
            //        }
            //        i++;
            //    }
            //    //Response.SetCookie(new HttpCookie("coins") { Expires = DateTime.Now.AddDays(-1) });

            //    //Response.SetCookie(new HttpCookie("coins")
            //    //{
            //    //    Value = coinsStr,
            //    //    Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["cookie_expiration"]))
            //    //});
            //}
            Session["coins"] = coinsDictionary;
            return RedirectToAction("Index");
        }

        private Dictionary<int, int> GetCoinsFromCookie()
        {
            var data = new Dictionary<int, int>();
            var coinsCookie = Request.Cookies["coins"];
            var coinsStr = string.Empty;
            if (coinsCookie?.Value != null) coinsStr = coinsCookie.Value;

            var coinsSplit = coinsStr.Split(';');
            foreach (var item in coinsSplit)
            {
                var itemSplit = item.Split(':');
                try
                {
                    if (!string.IsNullOrEmpty(itemSplit[0]) && !string.IsNullOrEmpty(itemSplit[1]))
                    {
                        data.Add(Convert.ToInt32(itemSplit[0]), Convert.ToInt32(itemSplit[1]));
                    }
                }
                catch { };
            }
            return data;
        }



        public ActionResult Index()
        {
            var model = new MachineViewModel.Machine();
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

                        var paidCookie = Request.Cookies["paid"];
                        if (paidCookie?.Value != null) machine.Paid += Convert.ToInt32(paidCookie.Value);

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

                        //var drinksCookie = Request.Cookies["drinks"];
                        //if (drinksCookie?.Value != null)
                        //{
                            
                        //}
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
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}