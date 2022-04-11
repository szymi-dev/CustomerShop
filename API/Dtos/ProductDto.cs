using API.Entities;

namespace API.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Descripiton { get; set; }
        public string PictureUrl { get; set; }
    }
}