using drinks.domain.@interface.services;
using drinks.infrastructure.Response;
using Ninject;
using System;
using System.Web.Http;
using drinks.domain.@interface.entities;
using drinks.infrastructure.Request;

namespace drinks.api.Controllers
{
    [RoutePrefix("api/drink")]
    public class DrinkController : ApiController
    {
        //[Inject]
        //public IDrinkService _drinkService;

        private IDrinkService _drinkService;

        public DrinkController(IDrinkService service)
        {
            try
            {
                _drinkService = service;
            }
            catch
            {

            }
        }

        [HttpPost]
        [Route("RemoveDrink")]
        public DefaultResponse RemoveDrink(DrinkRequest.FindDrinkById request)
        {
            try
            {
                _drinkService.Delete(request.Id);
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
        [Route("SaveDrink")]
        public DefaultResponse SaveDrink(DrinkRequest.EditDrink request)
        {
            try
            {
                _drinkService.Update(new Drink
                {
                    Id = request.Id,
                    Caption = request.Caption,
                    Cost = request.Cost,
                    Count = request.Count,
                    Image = request.Image
                });

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
        [Route("GetDrinks")]
        public DrinkListResponse GetDrinks()
        {
            try
            {
                return new DrinkListResponse
                {
                    Drinks = _drinkService.GetAllDrinks()
                };
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("GetDrinkById")]
        public ParticularDrink GetDrinkById(DrinkRequest.FindDrinkById request)
        {
            try
            {
                return new ParticularDrink
                {
                    Drink = _drinkService.GetDrinkById(request.Id)
                };
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("CreateDrink")]
        public DefaultResponse CreateDrink(DrinkRequest.CreateDrink request)
        {
            try
            {
                if (_drinkService.Create(request.Caption, request.Image, request.Cost, request.Count) != null)
                    return new DefaultResponse
                    {
                        Message = string.Empty,
                        ErrorCode = 0
                    };

                return new DefaultResponse
                {
                    Message = "Ошибка при создании напитка",
                    ErrorCode = 1
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
    }
}
