using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(string basketId);
        Task<Basket> UpdateBasket(Basket basket);
        Task<bool> DeleteBasket(string basketId);
    }
}