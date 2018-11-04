using drinks.domain.@interface.services;
using drinks.infrastructure;
using drinks.infrastructure.Request;
using drinks.infrastructure.Response;
using drinks.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace drinks.web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region HTTP_POST - изменение монеты
        [HttpPost]
        public ActionResult EditCoin(CoinViewModel.ParticularCoinModel model)
        {
            //Проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                CoinRequest.EditCoin request = new CoinRequest.EditCoin
                {
                    Id = model.Id,
                    Count = model.Count,
                    IsAllowed = model.IsAllowed
                };
                HttpResponseMessage result = HttpService.PostAsync("api/coin/SaveCoin", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<DefaultResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        TempData["success"] = "Данные по монете успешно изменены";
                    }
                }
                else
                {
                    TempData["error"] = "Ошибка сохранения данных";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("Coins", "Admin");
        }
        #endregion

        #region HTTP_GET - страница редактирования монеты
        [HttpGet]
        public ActionResult EditCoin(int id)
        {
            try
            {
                CoinRequest.FindCoinById request = new CoinRequest.FindCoinById
                {
                    Id = id
                };
                HttpResponseMessage result = HttpService.PostAsync("api/coin/GetCoinById", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<CoinResponse.ParticularCoin>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        return View(new CoinViewModel.ParticularCoinModel
                        {
                            Id = response.Coin.Id,
                            Caption = response.Coin.Caption,
                            Count = response.Coin.Count,
                            IsAllowed= response.Coin.IsAllowed
                        });
                    }
                }
            }
            catch
            {
                return View();
            }
            return View();

        }
        #endregion

        #region Страница управления монетами
        public ActionResult Coins()
        {
            CoinResponse.CoinsListResponse response = new CoinResponse.CoinsListResponse();
            try
            {
                HttpResponseMessage result = HttpService.PostAsync("api/coin/GetCoins", new DefaultRequest()).Result;
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsAsync<CoinResponse.CoinsListResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        CoinViewModel.CoinList coins = new CoinViewModel.CoinList
                        {
                            Coins = response.Coins
                        };
                        return View(coins);
                    }
                }
                response.ErrorCode = 10;
                response.Message = "Ошибка получения данных";
            }
            catch (Exception ex)
            {
                response.ErrorCode = 2;
                response.Message = ex.Message;
            }
            return View(); ;
        }
        #endregion 



        #region НАПИТКИ
        #region HTTP_GET - Удаление напитка
        [HttpGet]
        public ActionResult RemoveDrink(long id, string caption)
        {
            try
            {
                DrinkRequest.FindDrinkById request = new DrinkRequest.FindDrinkById
                {
                    Id = id
                };
                HttpResponseMessage result = HttpService.PostAsync("api/drink/RemoveDrink", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<DefaultResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        TempData["success"] = "Напиток '" + caption + "' успешно удален";
                    }
                }
                else
                {
                    TempData["error"] = "Ошибка удаления данных";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("Drinks", "Admin");
        }
        #endregion

        #region HTTP_POST - изменение напитка
        [HttpPost]
        public ActionResult EditDrink(DrinkViewModel.ParticularDrinkModel model)
        {

            //Проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string ext = Path.GetExtension(model.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyymmssfff") + ext;
                    model.Image = fileName;
                    fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
                    model.ImageFile.SaveAs(fileName);
                }

                DrinkRequest.EditDrink request = new DrinkRequest.EditDrink
                {
                    Id = model.Id,
                    Caption = model.Caption,
                    Image = model.Image,
                    Count = model.Count,
                    Cost = model.Cost
                };
                HttpResponseMessage result = HttpService.PostAsync("api/drink/SaveDrink", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<DefaultResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        TempData["success"] = "Напиток успешно изменен";
                    }
                }
                else
                {
                    TempData["error"] = "Ошибка сохранения данных";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("Drinks", "Admin");
        }
        #endregion

        #region HTTP_GET - страница редактирования напитка
        [HttpGet]
        public ActionResult EditDrink(int id)
        {
            try
            {
                DrinkRequest.FindDrinkById request = new DrinkRequest.FindDrinkById
                {
                    Id = id
                };
                HttpResponseMessage result = HttpService.PostAsync("api/drink/GetDrinkById", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<ParticularDrink>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        return View(new DrinkViewModel.ParticularDrinkModel
                        {
                            Id = response.Drink.Id,
                            Caption = response.Drink.Caption,
                            Image = response.Drink.Image,
                            Count = response.Drink.Count,
                            Cost = response.Drink.Cost
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return View();
            }
            return View();

        }
        #endregion

        #region Создание напитка
        [HttpPost]
        public ActionResult CreateDrink(DrinkViewModel.NewDrink model)
        {
            //Проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string ext = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yyyymmssfff") + ext;
                model.Image = fileName;
                fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
                model.ImageFile.SaveAs(fileName);

                DrinkRequest.CreateDrink request = new DrinkRequest.CreateDrink
                {
                    Caption = model.Caption,
                    Image = model.Image,
                    Count = model.Count,
                    Cost = model.Cost
                };
                HttpResponseMessage result = HttpService.PostAsync("api/drink/CreateDrink", request).Result;
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsAsync<DefaultResponse>().Result;
                    if (response.ErrorCode == 0 && string.IsNullOrEmpty(response.Message))
                    {
                        TempData["success"] = "Напиток успешно создан";
                    }
                }
                else
                {
                    TempData["error"] = "Ошибка получения данных";
                }
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("Drinks", "Admin");
        }

        [HttpGet]
        public ActionResult CreateDrink()
        {
            return View();
        }
        #endregion

        #region Список всех напитков
        public ActionResult Drinks()
        {
            DrinkListResponse drinkListResponse = new DrinkListResponse();
            try
            {
                HttpResponseMessage response = HttpService.PostAsync("api/drink/GetDrinks", new DefaultRequest()).Result;
                if (response.IsSuccessStatusCode)
                {
                    drinkListResponse = response.Content.ReadAsAsync<DrinkListResponse>().Result;
                    if (drinkListResponse.ErrorCode == 0 && string.IsNullOrEmpty(drinkListResponse.Message))
                    {
                        DrinkViewModel.DrinkList drinks = new DrinkViewModel.DrinkList
                        {
                            Drinks = drinkListResponse.Drinks
                        };
                        return View(drinks);
                    }
                }
                drinkListResponse.ErrorCode = 10;
                drinkListResponse.Message = "Ошибка получения данных";
            }
            catch (Exception ex)
            {
                drinkListResponse.ErrorCode = 2;
                drinkListResponse.Message = ex.Message;
            }
            return View(); ;
        }
        #endregion 
        #endregion
    }
}