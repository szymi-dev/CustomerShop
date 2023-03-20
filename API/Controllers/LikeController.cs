using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class LikeController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly StoreContext _context;

        public LikeController(IUserRepository userRepository, IProductRepository productRepository, StoreContext context)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _context = context;
        }

        [HttpPost("add-to-favorites/{productId}")]
        public async Task<ActionResult> AddToFavorites(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if(product == null)
            {
                return NotFound();
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsername(username);

            if(user == null)
            {
                return NotFound();
            }
            
            if (product.UserId == user.Id)
            {
                return BadRequest("You can't like your own product!");
            }

            if(user.LikedProducts.Contains(product))
            {
                return BadRequest("You have already liked this product!");
            }

            user.LikedProducts.Add(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-favorites")]
        public async Task<ActionResult<List<ProductDto>>> GetUserFavorites()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsername(username);

            var likedProducts = user.LikedProducts.Select(p => new ProductDto
                {
                    Name = p.Name,
                    Username = p.User.UserName,
                    Price = p.Price
                    
                }).ToList();

            return likedProducts;
        }
        
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteFromFavorites(int productId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsername(username);

            var product = user.LikedProducts.FirstOrDefault(p => p.Id == productId);

            if(product == null)
            {
                return NotFound();
            }

            user.LikedProducts.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}