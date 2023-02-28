using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasketById(string basketId)
        {
            var basket = await _basketRepo.GetBasket(basketId);

            return Ok(basket ?? new Basket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(Basket basket)
        {
            var  basketToUpdate = await _basketRepo.UpdateBasket(basket);

            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string basketId)
        {
             await _basketRepo.DeleteBasket(basketId);
        }
    }
}