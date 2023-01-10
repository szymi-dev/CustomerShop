using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;

namespace API.Entities
{
    public class ProductModel
    {
        public User User { get; set; }
        public ProductDto productDto { get; set; }
    }
}