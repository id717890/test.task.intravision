using drinks.domain.@interface.services;
using drinks.infrastructure.Response;
using Ninject;
using System;
using System.Web.Http;
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
