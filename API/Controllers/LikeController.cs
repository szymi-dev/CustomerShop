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

        
        [HttpPost("add-to-favorites/{username}/{productId}")]
        public async Task<ActionResult> AddToFavorites(string username, int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            
            if (user == null)
            {
                return Unauthorized();
            }

            if (product.UserId == user.Id)
            {
                return BadRequest("Nie możesz polubić własnego produktu.");
            }

            if (user.LikedProducts.Contains(product))
            {
                return BadRequest("Ten produkt został już przez Ciebie polubiony.");
            }

            user.LikedProducts.Add(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<ProductDto>>> GetUserFavorites(string username)
        {
            var user = await _context.Users
            .Where(u => u.UserName == username)
            .Include(x => x.LikedProducts)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync();

            var likedProducts = user.LikedProducts.Select(p => new ProductDto
                {
                    Name = p.Name,
                    Username = p.User.UserName,
                    Price = p.Price
                    
                }).ToList();

            return likedProducts;
        }
        
        [HttpDelete("{username}/{productId}")]
        public async Task<ActionResult> DeleteFromFavorites(string username, int productId)
        {
            var user = await _context.Users
                .Where(u => u.UserName == username)
                .Include(x => x.LikedProducts)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();

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