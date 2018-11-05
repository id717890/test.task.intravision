namespace drinks.api.Controllers
{
    using domain.@interface.services;
    using infrastructure.Response;
    using System;
    using System.Web.Http;
    using domain.@interface.entities;
    using infrastructure.Request;

    /// <inheritdoc />
    /// <summary>
    /// Контроллер для управления напитками
    /// </summary>
    [RoutePrefix("api/drink")]
    public class DrinkController : ApiController
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService service)
        {
            _drinkService = service;
        }

        /// <summary>
        /// Удаляет напиток из БД
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Сохраняет данные по напитку
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получает список всех напитков
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Поиск напитка по ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создает новый напиток
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
