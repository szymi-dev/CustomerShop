using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        public IGenericRepository<Product> _productRepo;
        public StoreContext _context;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo, StoreContext context, IMapper mapper)
        {
            _productRepo = productRepo;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto[]>> GetProducts([FromQuery]ProductParams productParams)
        {
            var spec = new ProductsSpecification(productParams);

            var products = await _productRepo.ListAsync(spec);

            var productsToReturn = _mapper.Map<ProductDto[]>(products);

            return Ok(productsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsSpecification(id);

            return await _productRepo.GetEntityWithSpec(spec);
        }

        [HttpPost("add-product/{username}")]
        public async Task<ActionResult<ProductDto>> AddProduct(string username, ProductDto productDto)
        {

            User user= await _context.Users
                .Where(x=>x.UserName==username)
                .Include(x => x.Products)
                .SingleOrDefaultAsync();

            
            if(user!= null)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    UserId = user.Id
                };

                user.Products.Add(product);
                await _context.SaveChangesAsync();

                var productToReturn = _mapper.Map<Product, ProductDto>(product);
                return Ok(productToReturn);
            }
            
            return Ok();
        }
        [HttpDelete("delete-product/{username}/{productId}")]
        public async Task<ActionResult> DeleteProduct(string username, int productId)
        {
            User u = await _context.Users
                .Where(u => u.UserName == username)
                .Include(p => p.Products)
                .FirstOrDefaultAsync();


            var product = u.Products.FirstOrDefault(p => p.Id == productId);

            //if(productId == null) return BadRequest("There is no product with such Id");
            if(product == null) return NotFound();

            u.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }
    
        [HttpGet("GetProducts/{username}")]
        public async Task<ActionResult<ProductDto[]>> GetUserProductsByUsername(string username)
        {
            User u = await _context.Users
                .Where(x => x.UserName  == username)
                .Include(p => p.Products)
                .FirstOrDefaultAsync();

            if(u != null)
            {
                var productDtos = u.Products.Select(x => new ProductDto
                {
                    Name = x.Name,
                    Username = x.User.UserName,
                    Price = x.Price
                })
                .ToArray();

                return productDtos;
            }

            return Ok();
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }
    }
}