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

        public ActionResult EditDrink()
        {
            return null;
        }

        public ActionResult DeleteDrink()
        {
            return null;
        }

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
                model.Image = "/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
                model.ImageFile.SaveAs(fileName);

                DrinkRequest.CreateDrink request = new DrinkRequest.CreateDrink
                {
                    Caption = model.Caption,
                    Image =  model.Image,
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
    }
}